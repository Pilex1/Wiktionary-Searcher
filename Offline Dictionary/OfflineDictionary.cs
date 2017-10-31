using LibUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WiktionaryTranslator;

namespace Offline_Dictionary {
    public class OfflineDictionary {

        private string language;

        public OfflineDictionary(string language) {
            this.language = language;
        }

        public void DownloadFromWiktionary() {
            OfflineFromWiktionary dict = new OfflineFromWiktionary(language);
            dict.Translate();
        }

        public void DownloadFromFile(string file) {
            OfflineFromFile dict = new OfflineFromFile(file, language);
            dict.Translate();
        }


        private static string ToStringTranslationAll(string original, FullTranslation fullTranslation) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(original + " {");
            if (fullTranslation == null) {
                Console.WriteLine("ERROR: " + original);
                sb.AppendLine("\t*** ERROR ***");
            } else if (fullTranslation.Translations.Count == 0) {
                Console.WriteLine("EMPTY TRANSLATION: " + original);
                sb.AppendLine("\t*** EMPTY TRANSLATION ***");
            } else {
                foreach (Translation tr in fullTranslation.Translations) {
                    if (string.IsNullOrWhiteSpace(tr.translation)) {
                        sb.AppendLine("\t" + tr.grammaticalForm + " { *** EMPTY TRANSLATION *** }");
                    } else {
                        sb.AppendLine("\t" + tr.grammaticalForm + " { " + tr.translation + " }");
                    }
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        private class OfflineFromWiktionary {

            private string language;
            private List<Tuple<string, List<string>>> allWords;
            private Stopwatch timeLoading;
            private Stopwatch timeTranslating;

            private string dir;
            private string dirDecoded;
            private string dirTranslation;

            private int processedItems;
            private int totalItems;

            internal OfflineFromWiktionary(string language) {
                this.language = language;

                dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Plexico/Wiktionary Searcher/" + language + "/";
                dirDecoded = "decoded/";
                dirTranslation = "translation/";
            }

            private string Decode(string s) => WebUtility.UrlDecode(s).Replace("_", " ");

            private string GetTime(TimeSpan span) => span.ToString("d\\.hh\\:mm\\:ss");
            private string GetTime(Stopwatch watch) => GetTime(watch.Elapsed);

            private string ProgressTime() {
                long curTime = timeTranslating.ElapsedMilliseconds;
                float timeEachItem = (float)curTime / processedItems;
                int predictedTime = (int)(timeEachItem * totalItems);
                TimeSpan predictedTimeSpan = new TimeSpan(0, 0, 0, 0, predictedTime);
                return processedItems + " / " + totalItems + "     " + GetTime(timeTranslating) + " / " + GetTime(predictedTimeSpan);
            }

            private static int CountWords(List<Tuple<string, List<string>>> words) {
                int count = 0;
                foreach (Tuple<string, List<string>> t in words) {
                    foreach (string s in t.Item2) {
                        if (!string.IsNullOrWhiteSpace(s)) {
                            count++;
                        }
                    }
                }
                return count;
            }

            private List<Tuple<string, List<string>>> RemoveAlreadyTranslated(List<Tuple<string, List<string>>> list) {
                List<Tuple<string, List<string>>> newWords = new List<Tuple<string, List<string>>>();
                foreach (Tuple<string, List<string>> t in list) {
                    string section = t.Item1;
                    string file = dir + dirTranslation + section + "_translation.txt";
                    if (File.Exists(file)) {
                        HashSet<string> hashSet = new HashSet<string>(t.Item2);
                        string[] alreadyTranslated = FileUtil.ReadFileAsArray(file);
                        int removeCount = 0;
                        foreach (string line in alreadyTranslated) {
                            Match match = Regex.Match(line, "^([^{]+?) {");
                            if (match.Success) {
                                string word = match.Groups[1].Captures[0].ToString();
                                hashSet.Remove(word);
                                removeCount++;
                            }
                        }
                        newWords.Add(new Tuple<string, List<string>>(section, new List<string>(hashSet)));
                        Console.WriteLine("Skipping " + removeCount + " words in " + section);
                    } else {
                        newWords.Add(t);
                    }
                }
                return newWords;
            }

            // goes to the lemma section in Wiktionary for the given language
            // loads all the words there
            private void LoadAll() {
                // each tuple stores the grammatical form, and a list of all the words
                // under that grammatical form
                // represents all the words in the language
                allWords = new List<Tuple<string, List<string>>>();

                string s = WebUtil.DownloadHtml(string.Format("https://en.wiktionary.org/wiki/Category:{0}_lemmas", language));
                List<string> links = new List<string>();
                MatchCollection collection = Regex.Matches(s, "<a href=\"(/wiki/Category:[^<>]+?)\"[^<>]*?>Category:[^<>]+?</a>");
                foreach (Match match in collection) {
                    // e.g. /wiki/Category:English_conjunctions
                    string link = match.Groups[1].Captures[0].ToString();
                    Match m = Regex.Match(link, "/wiki/Category:([^<>]+)");
                    string category = m.Groups[1].Captures[0].ToString();
                    if (File.Exists(dir + dirDecoded + category + "_decoded.txt")) {
                        // we've already downloaded this category before so we can load it from file
                        Console.WriteLine("Loading from file: " + link);
                        Tuple<string, List<string>> t = new Tuple<string, List<string>>(category, new List<string>());
                        using (StreamReader reader = new StreamReader(dir + dirDecoded + category + "_decoded.txt")) {
                            string s2 = null;
                            while ((s2 = reader.ReadLine()) != null) {
                                t.Item2.Add(s2);
                            }
                        }
                        allWords.Add(t);
                    } else {
                        string url = "https://en.wiktionary.org" + link;
                        Console.WriteLine("Adding: " + url);
                        links.Add(url);
                    }
                }



                // loop through all the sub-pages 
                // e.g. Category:English adjectives, Category:English nouns, etc.
                foreach (string link in links) {

                    Match m2 = Regex.Match(link, "[\\s\\S]+?Category:([\\s\\S]+)");
                    Tuple<string, List<string>> tuple = new Tuple<string, List<string>>(m2.Groups[1].Captures[0].ToString(), new List<string>());

                    s = WebUtil.DownloadHtml(link);
                    // parse the website and keep going to the "Next page" if available
                    while (true) {
                        // find the "Pages in category [e.g. Latin verbs]" section
                        MatchCollection collection3 = Regex.Matches(s, "<h2>[^<>]+?</h2>");
                        List<int> indicies = new List<int>();
                        foreach (Match m in collection3) {
                            indicies.Add(m.Groups[0].Captures[0].Index);
                        }
                        string[] sections = s.Split(indicies.ToArray());
                        string section = null;
                        foreach (string sec in sections) {
                            Match m = Regex.Match(sec, "^<h2>Pages in category [^<>]+?</h2>");
                            if (m.Success) {
                                section = sec;
                                break;
                            }
                        }
                        if (section == null) {
                            break;
                        }

                        // then extract all the links in that section
                        MatchCollection collection4 = Regex.Matches(section, "<li><a href=\"/wiki/([^<>]+?)\"[^<>]*?>[^<>]*?</a></li>");
                        foreach (Match m in collection4) {
                            string word = m.Groups[1].Captures[0].ToString();
                            if (!word.StartsWith("Category:") && !word.StartsWith("Appendix:") && !word.StartsWith("Talk:") && !word.StartsWith("Citations:") && !word.StartsWith("User_talk:")) {
                                if (word.Contains(":")) {
                                    Console.WriteLine("WARNING " + word);
                                    // throw new ArgumentException();
                                }
                                //Console.WriteLine(word);
                                tuple.Item2.Add(Decode(word));
                            }
                        }

                        // go to the next page
                        MatchCollection collection2 = Regex.Matches(s, "<a href=\"(/w/index.php\\?title=Category:[^<>]+?&amp;pagefrom=[^<>]+?\")[^<>]*?>next page</a>");
                        if (collection2.Count == 0) break;

                        string newLink = "https://en.wiktionary.org" + collection2[0].Groups[1].Captures[0].ToString().Replace("amp;", "");
                        s = WebUtil.DownloadHtml(newLink);
                        Console.WriteLine(GetTime(timeLoading) + " Accessing page: " + newLink);
                    }

                    allWords.Add(tuple);

                    // write the decoded data i.e. after HTML decoding
                    StreamWriter writerDecoded = new StreamWriter(dir + dirDecoded + tuple.Item1 + "_decoded.txt");
                    foreach (string w in tuple.Item2) {
                        writerDecoded.WriteLine(Decode(w));
                    }
                    writerDecoded.Dispose();
                }

                // make a file with all the words in it
                string[] decodedFiles = Directory.GetFiles(dir + dirDecoded);
                HashSet<string> decodedWords = new HashSet<string>();
                foreach (string file in decodedFiles) {
                    foreach (string word in FileUtil.ReadFileAsArray(file)) {
                        decodedWords.Add(word);
                    }
                }
                StringBuilder sb3 = new StringBuilder();
                foreach (string s2 in decodedWords) {
                    sb3.AppendLine(s2);
                }
                StreamWriter writerFullDecoded = new StreamWriter(dir + language + "_full_decoded.txt");
                writerFullDecoded.WriteLine(sb3.ToString());
                writerFullDecoded.Dispose();
                Console.WriteLine();
            }

            private void TranslateAll() {
                // we now have all the words loaded and categorised based on grammatical form
                // now to look up their translations
                Translator translator = new Translator(language);

                // remove sections that have already been translated
                allWords = RemoveAlreadyTranslated(allWords);

                totalItems = CountWords(allWords);
                processedItems = 0;
                foreach (Tuple<string, List<string>> t in allWords) {

                    string section = t.Item1;
                    List<string> wordList = t.Item2;

                    StreamWriter translationWriter = new StreamWriter(dir + dirTranslation + section + "_translation.txt", true);
                    translationWriter.AutoFlush = true;
                    for (int i = 0; i < wordList.Count; i++) {
                        string curWord = wordList[i];
                        if (string.IsNullOrWhiteSpace(curWord)) continue;
                        Console.WriteLine(ProgressTime() + " " + section + " " + curWord);
                        FullTranslation fullTranslation = translator.Translate(curWord);
                        string translatedText = ToStringTranslationAll(curWord, fullTranslation);
                        translationWriter.WriteLine(translatedText);
                        processedItems++;
                    }

                    translationWriter.Dispose();
                }
                // delete any empty files
                FileUtil.DeleteFilesInDirectoryIfEmpty(dir + dirTranslation, SearchOption.TopDirectoryOnly);

                // when everything is done, compile another file with all the words in it
                Console.WriteLine();
                Console.WriteLine("Compiling files");
                Console.WriteLine();
                string[] allFiles = Directory.GetFiles(dir + dirTranslation);
                List<string> files = new List<string>();
                foreach (string s2 in allFiles) {
                    if (s2.EndsWith("_translation.txt")) {
                        files.Add(s2);
                        Console.WriteLine(s2);
                    }
                }
                Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
                foreach (string f in files) {
                    using (StreamReader reader = new StreamReader(f)) {
                        string word = null;
                        List<string> translations = new List<string>();

                        string currentLine = null;
                        while ((currentLine = reader.ReadLine()) != null) {
                            Match m1 = Regex.Match(currentLine, "^([^{]+?) {$");
                            if (m1.Success) {
                                word = m1.Groups[1].Captures[0].ToString();
                                continue;
                            }

                            Match m2 = Regex.Match(currentLine, "\\w+ \\{ .+? \\}$");
                            if (m2.Success) {
                                translations.Add(currentLine);
                                continue;
                            }

                            Match m3 = Regex.Match(currentLine, "^$");
                            if (m3.Success) {
                                if (word != null) {
                                    dictionary[word] = translations;
                                    word = null;
                                    translations = new List<string>();
                                    continue;
                                }
                            }
                        }
                    }
                }

                // convert the dictionary into a string
                StringBuilder sb2 = new StringBuilder();
                foreach (string word in dictionary.Keys) {
                    sb2.AppendLine(word + " {");
                    foreach (string line in dictionary[word]) {
                        sb2.AppendLine(line);
                    }
                    sb2.AppendLine("}");
                    sb2.AppendLine();
                }

                // writing to file
                StreamWriter writer = new StreamWriter(dir + language + "_full_translations.txt");
                writer.WriteLine(sb2.ToString());
                writer.Dispose();
            }

            internal void Translate() {
                Directory.CreateDirectory(dir + dirDecoded);
                Directory.CreateDirectory(dir + dirTranslation);

                timeLoading = new Stopwatch();
                timeLoading.Start();
                LoadAll();
                timeLoading.Stop();

                timeTranslating = new Stopwatch();
                timeTranslating.Start();
                TranslateAll();
                timeTranslating.Stop();

                Console.WriteLine();
                Console.WriteLine("Finished downloading " + language + " in " + GetTime(timeLoading.Elapsed.Add(timeTranslating.Elapsed)));
                Console.WriteLine();

                timeTranslating.Stop();
            }

        }

        private class OfflineFromFile {

            private string file;
            private string language;

            internal OfflineFromFile(string file, string language) {
                this.file = file;
                this.language = language;
            }

            internal void Translate() {
                string outputFile = file.Insert(file.LastIndexOf('.'), "_out");

                string s1 = null;
                using (StreamReader reader = new StreamReader(file)) {
                    s1 = reader.ReadToEnd();
                }

                s1 = s1.ToLower();

                // remove line numbers
                s1 = Regex.Replace(s1, "[ ]{2,}?\\d+", "");

                // remove unnecessary spacing
                s1 = Regex.Replace(s1, "[\\s]{2,}", " ");

                // remove punctuation
                s1 = Regex.Replace(s1, "[^\\w\\s]", "");

                s1 = s1.Trim();

                HashSet<string> set = new HashSet<string>(s1.Split(' '));
                List<string> words = new List<string>(set);

                // translating and writing to file
                Stopwatch watch = new Stopwatch();
                watch.Start();

                Translator t = new Translator(language);

                StringBuilder sb = new StringBuilder();
                foreach (string s in words) {
                    int l = 50;
                    StringBuilder msg = new StringBuilder(s);
                    while (msg.Length < l) {
                        msg.Append(' ');
                    }
                    Console.Write("\r");
                    Console.Write(msg.ToString());

                    try {
                        sb.AppendLine(ToStringTranslationAll(s, t.Translate(s)));
                        sb.AppendLine();
                        Console.Write("Success                             ");
                        Console.Write(watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff"));
                    } catch (Exception) {
                        Console.Write("Failed");
                        for (int i = 0; i < l; i++) {
                            Console.Write(' ');
                        }
                        Console.WriteLine();
                    }
                }

                StreamWriter writer = new StreamWriter(outputFile, false);
                writer.Write(sb.ToString());
                writer.Dispose();
            }

        }

    }
}
