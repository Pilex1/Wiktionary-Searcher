using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using LibUtil;
using System.Threading.Tasks;
using System.Xml;

namespace Tests {
    public class Translator {

        private string language = "English";

        public string Language {
            get { return language; }
            set {
                char[] charArr = value.ToCharArray();
                charArr[0] = char.ToUpper(charArr[0]);
                language = new string(charArr);
            }
        }

        public Translator(string language) {
            Language = language;
        }

        public FullTranslation Translate(string word) {
            FullTranslation fullTranslation = Translate(word, false);
            return fullTranslation;
        }

        private string GetLanguageFromH2(XmlNode h2) {
            if (!h2.Name.Equals("h2")) return null;
            string heading = h2.InnerXml.CleanHTML();
            heading = Regex.Replace(heading, "([\\s\\S]+)\\[edit\\]", "$1");
            if (heading == "Contents" || heading == "Navigation menu") return null;
            return heading;
        }

        // cuts out everything except what's in the language section
        private string RefineLanguage(string s) {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(s);

            XmlNodeList list = xmlDoc.GetElementsByTagName("h2");
            foreach (XmlNode node in list) {
                string heading = GetLanguageFromH2(node);
                if (heading != null && heading == language) {
                    StringBuilder body = new StringBuilder();
                    XmlNode next = node.NextSibling;
                    while (next != null && GetLanguageFromH2(next) == null) {
                        body.Append(next.InnerXml);
                        next = next.NextSibling;
                    }
                    return body.ToString();
                }
            }
            return null;
        }

        private static bool IsHeader(XmlNode node) {
            XmlAttributeCollection attributeCollection = node.Attributes;
            if (attributeCollection == null) return false;
            foreach (XmlAttribute attribute in attributeCollection) {
                if (attribute.Name == "class" && attribute.Value == "mw-headline") {
                    return true;
                }
            }
            return false;
        }

        // split between the different grammatical forms e.g. noun, adjective, etc.
        // tuple of grammatical form and actual content
        private List<Tuple<string, string>> SplitSections(string s) {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml("<text>" + s + "</text>");

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("span");
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
            List<XmlNode> refinedList = new List<XmlNode>();
            foreach (XmlNode node in nodeList) {
                bool isHeader = IsHeader(node);
                if (isHeader) {
                    StringBuilder body = new StringBuilder();
                    string heading = node.InnerText;
                    XmlNode next = node.NextSibling;
                    while (next != null && !IsHeader(next)) {
                        body.Append(next.OuterXml);
                        next = next.NextSibling;
                    }
                    list.Add(new Tuple<string, string>(heading, body.ToString()));
                }
            }
            return list;
        }

        private string LoadFromWiktionary(string search) {
            string url = "https://en.wiktionary.org/wiki/" + search;
            return Util.ExtractHTMLFromWebsite(url);
        }

        // returns null if unable to find a translation
        private FullTranslation Translate(string word, bool searchLinks) {
            FullTranslation fullTranslations = new FullTranslation();
            string s = LoadFromWiktionary(word);
            if (s == null) return null;
            s = RefineLanguage(s);
            List<Tuple<string, string>> sections = SplitSections(s);

            foreach (Tuple<string, string> tuple in sections) {
                string heading = tuple.Item1;
                string content = tuple.Item2;
                if (heading.StartsWith("References")) {

                } else if (heading.StartsWith("Pronunciation")) {

                } else if (heading.StartsWith("Etymology")) {

                } else if (heading.StartsWith("Inflection")) {

                } else if (heading.StartsWith("Descendants")) {

                } else if (heading.StartsWith("Antonyms")) {

                } else if (heading.StartsWith("Synonyms")) {

                } else if (heading.StartsWith("Translations")) {

                } else if (heading.StartsWith("Anagrams")) {

                } else if (heading.StartsWith("Declension")) {

                } else if (heading.StartsWith("Conjugation")) {

                } else if (heading.StartsWith("Alternative forms")) {

                } else if (heading.StartsWith("Usage notes")) {

                } else if (heading.StartsWith("Derived terms")) {

                } else if (heading.StartsWith("See also")) {

                } else if (heading.StartsWith("Further reading")) {

                } else if (heading.StartsWith("Related terms")) {

                } else if (heading.StartsWith("Coordinate terms")) {

                } else if (heading.StartsWith("Statistics")) {

                } else {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.LoadXml("<text>" + content + "</text>");

                    XmlNodeList list = xmlDoc.GetElementsByTagName("li");
                    foreach (XmlNode node in list) {
                        if (!node.HasParentNode("li")) {
                            string nodeContent = node.InnerXml;
                            fullTranslations.AddTranslation(heading, nodeContent, language, searchLinks);
                        }
                    }
                }
            }
            return fullTranslations;

        }

    }

}
