using LibUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Tests;

namespace Offline_Dictionary {
    public static class OfflineDictionary {

        public static void TranslateFromFile(string language, string file) {
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
                    sb.AppendLine(ToStringTranslationAll(s, t.Translate(s).Translations));
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

            using (StreamWriter writer = new StreamWriter(outputFile, false)) {
                writer.Write(sb.ToString());
            }
        }

        private static string ToStringTranslationAll(string original, List<Translation> fullTranslation) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(original + " {");
            foreach (Translation tr in fullTranslation) {
                sb.AppendLine("\t" + tr.g + " { " + tr.s + " }");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string Decode(string s) => WebUtility.UrlDecode(s).Replace("_", " ");

        public static void TranslateFromWiktionary(string language) {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Plexico/Wiktionary Searcher/" + language + "/";
            string dirRaw = "raw/";
            string dirDecoded = "decoded/";
            string dirTranslation = "translation/";
            Directory.CreateDirectory(dir + dirRaw);
            Directory.CreateDirectory(dir + dirDecoded);
            Directory.CreateDirectory(dir + dirTranslation);

            // list of tuples
            // each tuple stores the grammatical form, and a list of all the words
            // under that grammatical form
            // represents all the words in the language
            List<Tuple<string, List<string>>> allWords = new List<Tuple<string, List<string>>>();

            string s = Util.ExtractHTMLFromWebsite(string.Format("https://en.wiktionary.org/wiki/Category:{0}_lemmas", language));
            List<string> links = new List<string>();
            MatchCollection collection = Regex.Matches(s, "<a href=\"(/wiki/Category:[^<>]+?)\"[^<>]*?>Category:[^<>]+?</a>");
            foreach (Match match in collection) {
                // e.g. /wiki/Category:English_conjunctions
                string link = match.Groups[1].Captures[0].ToString();
                Match m = Regex.Match(link, "/wiki/Category:([^<>]+)");
                string category = m.Groups[1].Captures[0].ToString();
                if (File.Exists(dir + dirDecoded + category + "_decoded.txt") && File.Exists(dir + dirRaw + category + "_raw.txt")) {
                    // we've already downloaded this category before so we can load it from file
                    Console.WriteLine(watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " Loading from file: " + link);
                    Tuple<string, List<string>> t = new Tuple<string, List<string>>(category, new List<string>());
                    using (StreamReader reader = new StreamReader(dir + dirRaw + category + "_raw.txt")) {
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

                s = Util.ExtractHTMLFromWebsite(link);
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
                            tuple.Item2.Add(word);
                        }
                    }

                    // go to the next page
                    MatchCollection collection2 = Regex.Matches(s, "<a href=\"(/w/index.php\\?title=Category:[^<>]+?&amp;pagefrom=[^<>]+?\")[^<>]*?>next page</a>");
                    if (collection2.Count == 0) break;

                    string newLink = "https://en.wiktionary.org" + collection2[0].Groups[1].Captures[0].ToString().Replace("amp;", "");
                    s = Util.ExtractHTMLFromWebsite(newLink);
                    Console.WriteLine(watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " Accessing page: " + newLink);
                }

                allWords.Add(tuple);

                // write the raw data i.e. with the HTML encoding 
                // e.g. %26 instead of & symbol
                StringBuilder sb = new StringBuilder();
                foreach (string w in tuple.Item2) {
                    sb.AppendLine(w);
                }
                using (StreamWriter writer = new StreamWriter(dir + dirRaw + tuple.Item1 + "_raw.txt")) {
                    string s2 = sb.ToString();
                    writer.WriteLine(s2);
                }

                // write the decoded data i.e. after HTML decoding
                sb = new StringBuilder();
                foreach (string w in tuple.Item2) {
                    sb.AppendLine(Decode(w));
                }
                string s3 = sb.ToString();
                if (!string.IsNullOrWhiteSpace(s3)) {
                    using (StreamWriter writer = new StreamWriter(dir + dirDecoded + tuple.Item1 + "_decoded.txt")) {
                        writer.WriteLine(s3);
                    }
                }
            }

            // make a file with all the words in it
            string[] decodedFiles = Directory.GetFiles(dir + dirDecoded);
            HashSet<string> decodedWords = new HashSet<string>();
            foreach (string file in decodedFiles) {
                foreach (string word in Util.ReadFileAsArray(file)) {
                    decodedWords.Add(word);
                }
            }
            StringBuilder sb3 = new StringBuilder();
            foreach (string s2 in decodedWords) {
                sb3.AppendLine(s2);
            }
            using (StreamWriter writer = new StreamWriter(dir + language + "_full_decoded.txt")) {
                writer.WriteLine(sb3.ToString());
            }


            // we now have all the words loaded and categorised based on grammatical form
            // now to look up their translations
            Translator translator = new Translator(language);
            foreach (Tuple<string, List<string>> t in allWords) {

                if (File.Exists(dir + dirTranslation + t.Item1 + "_translation.txt")) {
                    // we've already translated this category before so we can skip this
                    Console.WriteLine(watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " Skipping translation: " + t.Item1);
                    continue;
                }

                StringBuilder sb = new StringBuilder();
                //string grammaticalForm = t.Item1;
                //grammaticalForm = grammaticalForm.Substring(grammaticalForm.IndexOf('_') + 1);
                //grammaticalForm = grammaticalForm.Substring(0, grammaticalForm.Length - 1);
                //grammaticalForm = grammaticalForm.CapitaliseFirstLetter();
                List<string> words = t.Item2;

                for (int i = 0; i < words.Count; i++) {
                    string word = words[i];
                    string decoded = Decode(word);
                    Console.WriteLine(watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff") + " " + (i + 1) + "/" + words.Count + " Translating: " + t.Item1 + " " + decoded);
                    FullTranslation fullTranslation = translator.Translate(word);
                    string translatedText = ToStringTranslationAll(decoded, fullTranslation.Translations);
                    if (string.IsNullOrWhiteSpace(translatedText)) {
                        Console.WriteLine("WARNING - Empty translation: " + word);
                    }
                    sb.AppendLine(translatedText);
                    sb.AppendLine();
                }

                using (StreamWriter writer = new StreamWriter(dir + dirTranslation + t.Item1 + "_translation.txt")) {
                    writer.WriteLine(sb.ToString());
                }
            }

            // when everything is done, compile another file with all the words in it
            Console.WriteLine("Compiling files");
            string[] allFiles = Directory.GetFiles(dir + dirTranslation);
            List<string> files = new List<string>();
            foreach (string s2 in allFiles) {
                if (s2.EndsWith("_translation.txt")) {
                    files.Add(s2);
                    Console.WriteLine(s2);
                } else {
                    throw new ArgumentException();
                }
            }
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            foreach (string f in files) {
                using (StreamReader reader = new StreamReader(f)) {
                    string word = null;
                    List<string> translations = new List<string>();

                    string currentLine = null;
                    while ((currentLine = reader.ReadLine()) != null) {
                        Match m1 = Regex.Match(currentLine, "(\\w+) {$");
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
            using (StreamWriter writer = new StreamWriter(dir + language + "_full_translations.txt")) {
                writer.WriteLine(sb2.ToString());
            }


            Console.WriteLine("Finished downloading " + language + " in " + watch.Elapsed.ToString("hh\\:mm\\:ss\\.ff"));
            Console.WriteLine();
            Console.WriteLine();

        }
    }
}
