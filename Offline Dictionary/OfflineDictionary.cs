using LibUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tests;

namespace Offline_Dictionary {
    class OfflineDictionary {

        private string outputFile;

        private string language;
        private string[] words;

        public static OfflineDictionary FromFile(string language, string file) {
            OfflineDictionary dict = new OfflineDictionary();
            dict.language = language;
            dict.outputFile = file.Insert(file.LastIndexOf('.'), "_out");

            string s = null;
            using (StreamReader reader = new StreamReader(file)) {
                s = reader.ReadToEnd();
            }

            s = s.ToLower();

            // remove line numbers
            s = Regex.Replace(s, "[ ]{2,}?\\d+", "");

            // remove unnecessary spacing
            s = Regex.Replace(s, "[\\s]{2,}", " ");

            // remove punctuation
            s = Regex.Replace(s, "[^\\w\\s]", "");

            s = s.Trim();

            HashSet<string> set = new HashSet<string>(s.Split(' '));
            dict.words = set.ToArray();

            return dict;
        }

        public static OfflineDictionary FromWiktionary(string language) {
            OfflineDictionary dict = new OfflineDictionary();
            dict.language = language;
            dict.outputFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Plexico/Latin Vocab/Wiktionary_" + language + "_out.txt";

            string s = Util.ExtractHTMLFromWebsite(string.Format("https://en.wiktionary.org/wiki/Category:{0}_lemmas", language));

            List<string> links = new List<string>();
            MatchCollection collection = Regex.Matches(s, "<a href=\"(/wiki/Category:[^<>]+?)\"[^<>]*?>Category:[^<>]+?</a>");
            foreach (Match match in collection) {
                string link = match.Groups[1].Captures[0].ToString();
                links.Add("https://en.wiktionary.org" + link);
            }

            List<string> allWords = new List<string>();

            // loop through all the sub-pages
            foreach (string link in links) {
                s = Util.ExtractHTMLFromWebsite(link);
                Console.WriteLine(link);
                while (true) {
                    // find the "Pages in category [e.g. Latin verbs]" section

                    // then extract all the links in that section


                    // go to the next page
                    MatchCollection collection2 = Regex.Matches(s, "<a href=\"(/w/index.php\\?title=Category:[^<>]+?&amp;pagefrom=[^<>]+?\")[^<>]*?>[^<>]+?</a>");
                    if (collection2.Count == 0) break;

                    string newLink = "https://en.wiktionary.org" + collection2[0].Groups[1].Captures[0].ToString().Replace("amp;", "");
                    s = Util.ExtractHTMLFromWebsite(newLink);
                    Console.WriteLine(newLink);
                }

            }

            return dict;

        }

        private OfflineDictionary() {

        }


        private void AddTranslation(StringBuilder sb, string word, FullTranslation f) {
            sb.AppendLine(word + " {");
            foreach (Translation t in f.Translations) {
                sb.AppendLine("\t" + t.g + " { " + t.s + " }");
            }
            sb.AppendLine("}");
            sb.AppendLine();
        }

        public void Translate() {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Translator t = new Translator();
            t.Language = language;

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
                    FullTranslation f = t.Translate(s);
                    AddTranslation(sb, s, f);
                    Console.Write("Success                             ");
                    Console.Write(watch.Elapsed.ToString("mm\\:ss\\.ff"));
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


    }
}
