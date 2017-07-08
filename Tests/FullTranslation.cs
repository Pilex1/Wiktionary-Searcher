using LibUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tests {

    public class FullTranslation {
        public List<Translation> Translations => translations;
        private List<Translation> translations;

        public HashSet<string> Links => links;
        private HashSet<string> links;


        public FullTranslation() {
            translations = new List<Translation>();
            links = new HashSet<string>();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (Translation tr in Translations) {
                sb.AppendLine(tr.g + ":");
                foreach (string line in tr.s.Split('\n')) {
                    sb.AppendLine("\t" + line);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public void AddTranslation(string grammaticalForm, string s, string language, bool searchLinks) {

            // find all the links
            // if the links lead

            // find links to the same language
            if (searchLinks) {
                MatchCollection collection = Regex.Matches(s, "<a href=\"/wiki/(.+?)#(.+?)\" title=\".+?\">.+?</a>");
                foreach (Match match in collection) {
                    string link = match.Groups[1].Captures[0].ToString();
                    string lang = match.Groups[2].Captures[0].ToString();
                    if (link.StartsWith("Appendix:")) {
                    } else {
                        if (language.Equals(lang)) {
                            links.Add(link);
                        }
                    }
                }
            }

            s = s.CleanHTML();
            s = Regex.Replace(s, "[\\n]{2,}", "\n");
            s = Regex.Replace(s, "[ ]{2,}", " ");
            s = s.Trim();

            if (s.Contains("<") || s.Contains(">")) {
                Console.WriteLine("WARNING - String not parsed correctly: " + s);
            }
            if (string.IsNullOrWhiteSpace(s)) {
                Console.WriteLine("WARNING - Whitespace string");
            }

            translations.Add(new Translation(s, grammaticalForm));
        }
    }

    public class Translation {

        public string s;
        public string g;

        public Translation(string s, string g) {
            this.s = s;
            this.g = g;
        }

    }
}
