using LibUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace WiktionaryTranslator {

    public class FullTranslation {

        private string original;

        private List<string> latinInflections;

        public List<Translation> Translations => translations;
        private List<Translation> translations;

        public HashSet<string> Links => links;
        private HashSet<string> links;

        private FullTranslation(string original, List<Translation> translations) {
            this.original = original;
            this.translations = translations;
            latinInflections = new List<string>();
            links = new HashSet<string>();
        }

        public FullTranslation(string original) {
            this.original = original;
            translations = new List<Translation>();
            latinInflections = new List<string>();
            links = new HashSet<string>();
        }

        public static string ToString(string word, List<Translation> translations) {
            FullTranslation f = new FullTranslation(word, translations);
            return f.ToString();
        }

        public override string ToString() {
            string s = "";
            //List<string> inflectedForms = new List<string>();
            //inflectedForms.Add(original);
            //inflectedForms.AddRange(latinInflections);
            //s += StringUtil.ToCSV(inflectedForms.ToArray()) + " {" + Environment.NewLine;
            s += original + " {" + Environment.NewLine;
            if (Translations.Count == 0) throw new ArgumentException();
            foreach (Translation tr in Translations) {
                s += "\t" + tr.grammaticalForm + " { ";
                foreach (string line in tr.translation.Split('\n')) {
                    s += line + Environment.NewLine;
                }
                s = s.Trim();
                s += " }" + Environment.NewLine;
            }
            s += "}" + Environment.NewLine;
            return s;
        }

        private static bool NodePredicate(XmlNode node) {
            bool insideContent = false;

            string tag = node.Name;
            XmlAttributeCollection attributes = node.Attributes;
            if (tag.Equals("a")) {
                if (attributes.Contains("class", "external autonumber")) {
                    insideContent = false;
                } else {
                    insideContent = true;
                }
            }
            if (tag.Equals("b")) insideContent = true;
            if (tag.Equals("i")) insideContent = true;
            if (tag.Equals("u")) insideContent = true;
            if (tag.Equals("span")) {
                if (attributes.Contains("class", "HQToggle")) {
                    insideContent = false;
                } else {
                    insideContent = true;
                }
            }
            if (tag.Equals("text")) insideContent = true;
            if (tag.Equals("ruby")) insideContent = true;
            if (tag.Equals("mark")) insideContent = true;
            if (tag.Equals("strong")) insideContent = true;
            if (tag.Equals("abbr")) insideContent = true;
            if (tag.Equals("ol")) insideContent = true;
            if (tag.Equals("html")) insideContent = true;
            if (tag.Equals("body")) insideContent = true;
            if (tag.Equals("sup") && attributes.Count == 0) insideContent = true;
            if (tag.Equals("sub") && attributes.Count == 0) insideContent = true;
            return insideContent;
        }

        public void AddLatinInflection(string s) {
            latinInflections.Add(s);
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

            s = s.CleanXml(NodePredicate);
            s = Regex.Replace(s, "[\\n]{2,}", "\n");
            s = Regex.Replace(s, "[ ]{2,}", " ");
            s = s.Trim();

            if (string.IsNullOrWhiteSpace(s)) {
                //  Console.WriteLine("*** WARNING *** - Ignoring whitespace string");
                return;
            }
            translations.Add(new Translation(s, grammaticalForm));
        }
    }

    public class Translation {

        public string translation;
        public string grammaticalForm;

        public Translation(string translation, string grammaticalForm) {
            this.translation = translation;
            this.grammaticalForm = grammaticalForm;
        }

        public override string ToString() {
            return grammaticalForm + " { " + translation + " }";
        }

    }
}
