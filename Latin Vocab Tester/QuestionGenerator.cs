using LibUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WiktionaryTranslator;

namespace Latin_Vocab_Tester {

    public class WordInfo {
        internal List<string> definitions;
        internal int score;
        internal WordInfo(List<string> definitions, int score) {
            this.definitions = definitions;
            this.score = score;
        }
        public string DefinitionsString() {
            string s = "";
            foreach (string d in definitions) {
                s += d + Environment.NewLine;
            }
            return s;
        }
    }

    public class QuestionGenerator {


        // maps a word to its score (number of times answered correctly - 2 * number of times answered incorrectly)
        // and its definitions (a list of strings)
        private SortedDictionary<string, WordInfo> dict = new SortedDictionary<string, WordInfo>();

        // maps a score to a list of words
        // e.g. dictScore[1] returns a list where all words have a score of exactly 1
        private Dictionary<int, HashSet<string>> dictScore = new Dictionary<int, HashSet<string>>();

        private string innerDirectory;
        internal const string saveFile = "save.txt";
        private Random rand = new Random();

        public QuestionGenerator(string innerDirectory) {
            this.innerDirectory = innerDirectory;
            ReadSaveFile();
            UpdateFile();
        }

        public void UpdateTranslation(string word, List<string> translation) {
            if (!dict.ContainsKey(word)) {
                if (translation.Count > 0) {
                    dict[word] = new WordInfo(translation, 0);
                }
            } else {
                if (translation.Count == 0) {
                    dict.Remove(word);
                } else {
                    dict[word].definitions = translation;
                }
            }
            UpdateFile();
        }

        public WordInfo GetInfo(string word) {
            if (!dict.ContainsKey(word)) return null;
            return dict[word];
        }

        private string GetWordExcluding(HashSet<string> excluding) {
            string result = null;
            List<string> words = new List<string>(dict.Keys);
            while (true) {
                int index = rand.Next(words.Count);
                result = words[index];
                if (!excluding.Contains(result)) break;
            }
            return result;
        }

        public QASet Generate(int answerCount) {

            // pick a random element from the lowest scores
            int lowestScore = CalculateLowestScore();
            HashSet<string> lowest = dictScore[lowestScore];
            string wordToBeTranslated = CollectionUtil<string>.RandomElement(lowest, rand);

            // finds a random index to put the correct answer
            string[] answers = new string[answerCount];
            int indexOfCorrectAnswer = (int)(rand.NextDouble() * answerCount);
            answers[indexOfCorrectAnswer] = CollectionUtil<string>.RandomElement(dict[wordToBeTranslated].definitions, rand);

            // populates the remaining spots with incorrect answers
            // these are chosen so that no answers from the same word appear twice
            HashSet<string> excluding = new HashSet<string>();
            excluding.Add(wordToBeTranslated);
            for (int i = 0; i < answerCount; i++) {
                if (i == indexOfCorrectAnswer) continue;
                string word = GetWordExcluding(excluding);
                answers[i] = CollectionUtil<string>.RandomElement(dict[word].definitions, rand);
            }

            return new QASet(this, wordToBeTranslated, answers, indexOfCorrectAnswer);
        }

        /// <summary>
        /// Calculates the lowest score with at least 10 entries
        /// </summary>
        private int CalculateLowestScore() {
            int lowest = int.MaxValue;
            foreach (int score in dictScore.Keys) {
                if (score < lowest && dictScore[score].Count >= 10) {
                    lowest = score;
                }
            }
            return lowest;
        }

        private void UpdateScore(string word, int newScore) {

            int currentScore = dict[word].score;
            if (dictScore.ContainsKey(currentScore)) {
                dictScore[currentScore].Remove(word);
            }

            if (dictScore.ContainsKey(newScore)) {
                dictScore[newScore].Add(word);
            } else {
                dictScore[newScore] = new HashSet<string>(new string[] { word });
            }
            dict[word].score = newScore;
        }

        private void ReadSaveFile() {
            var arr = FileUtil.ReadFileAsArray(innerDirectory + saveFile, Environment.NewLine + Environment.NewLine);
            foreach (var val in arr) {

                /*
                 
                For each entry:

                    Case 1:
                    pars {
                        8
	                    Noun { part, piece }
	                    Noun { party (politics) }
                    }

                    Case 2 (score not present - assume 0):
                    pars {
                        Noun { part, piece }
	                    Noun { party (politics) }
                    }

                    Case 3 (translation not present - find a translation and initialise score to 0):
                    pars

                 */

                string[] lines = val.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string word = null;
                int score = 0;
                List<string> definitions = new List<string>();
                if (lines.Length == 1) {
                    // Case 3 - find a translation
                    Translator translator = new Translator("Latin");
                    word = lines[0].Trim();
                    Console.WriteLine("Translating: " + word);
                    FullTranslation translations = translator.Translate(word);
                    if (translations == null) {
                        throw new ArgumentException("Unable to translate: " + word);
                    }
                    foreach (Translation t in translations.Translations) {
                        definitions.Add(t.ToString());
                    }
                } else {
                    for (int i = 0; i < lines.Length; i++) {
                        string l = lines[i];
                        l = l.Replace("{", " { ");
                        l = l.Replace("}", " }");
                        l = l.Replace("  ", " ");
                        if (i == 0) {
                            word = Regex.Match(l, "(.+){").Groups[1].Captures[0].ToString().Trim();
                        } else if (i == 1) {
                            // Case 2
                            if (!int.TryParse(l, out score)) {
                                // Case 1
                                definitions.Add(l.Trim());
                            }
                        } else if (i == lines.Length - 1) {
                            // do nothing
                        } else {
                            // Case 1
                            definitions.Add(l.Trim());
                        }
                    }
                }
                if (dict.ContainsKey(word)) {
                    throw new ArgumentException(word);
                }
                foreach (string s in definitions) {
                    if (s == "{" || s == "}") throw new Exception();
                }
                dict.Add(word, new WordInfo(definitions, score));
                UpdateScore(word, score);
            }
        }

        private void UpdateFile() {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, WordInfo> kvp in dict) {
                sb.AppendLine(kvp.Key + " {");
                sb.AppendLine("\t" + kvp.Value.score);
                foreach (string s in kvp.Value.definitions) {
                    sb.AppendLine("\t" + s);
                }
                sb.AppendLine("}");
                sb.AppendLine();
            }
            FileUtil.WriteFile(innerDirectory + saveFile, sb.ToString().Trim());
        }

        // notifies us that the given word has been answered correctly
        // used to update the incorrect words file
        internal void MarkCorrect(string s) {
            UpdateScore(s, dict[s].score + 1);
            UpdateFile();
        }

        // notifies us that the given word has been answered incorrectly
        // used to update the incorrect words file
        internal void MarkIncorrect(string s) {
            UpdateScore(s, dict[s].score - 2);
            UpdateFile();
        }
    }
}
