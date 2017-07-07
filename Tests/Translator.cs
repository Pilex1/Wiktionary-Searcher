using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using LibUtil;
using System.Threading.Tasks;

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

        public Translator() {
        }

        public FullTranslation Translate(string word) {
            FullTranslation fullTranslation = new FullTranslation();
            //     try {
            Translate(fullTranslation, word, false);
            //   } catch (Exception e) {
            //      fullTranslation = null;
            //  }
            return fullTranslation;
        }

        // cuts out everything except what's in the language section
        private string RefineLanguage(string s) {
            Regex rgx = new Regex("<h2><span class=\"mw-headline\" id=\"(.+?)\">");
            Match m = rgx.Match(s);

            int latinIndex = -1;
            int afterLatinIndex = -1;

            while (m.Success) {

                Group g = m.Groups[1];
                Capture c = g.Captures[0];
                //   Console.WriteLine(c);

                if (latinIndex == -1) {
                    if (c.Value.Equals(language)) {
                        latinIndex = m.Index;
                    }
                } else if (afterLatinIndex == -1) {
                    afterLatinIndex = m.Index;
                }

                m = m.NextMatch();
            }

            if (afterLatinIndex == -1) {
                afterLatinIndex = s.Length;
            }
            if (latinIndex == -1) {
                return "";
            }
            s = s.Substring(latinIndex, afterLatinIndex - latinIndex);
            return s;
        }

        private string LoadFromFile(string file) {
            using (StreamReader reader = new StreamReader(file)) {
                return reader.ReadToEnd();
            }
        }


        // split between the different grammatical forms e.g. noun, adjective, etc.
        private Section SplitSections(string s) {
            Regex rgx = new Regex("<span class=\"mw-headline\" id=\".+\">([a-zA-Z ]+?)</span>");
            List<Tuple<int, string>> sectionIndices = new List<Tuple<int, string>>();

            Match m = rgx.Match(s);
            while (m.Success) {
                sectionIndices.Add(new Tuple<int, string>(m.Index, m.Groups[1].Captures[0].ToString()));
                m = m.NextMatch();
            }
            sectionIndices.Add(new Tuple<int, string>(s.Length, "Null"));

            Section section = new Section();

            if (sectionIndices.Count > 0) {
                int start = sectionIndices[0].Item1;
                for (int i = 0; i < sectionIndices.Count - 1; i++) {
                    int end = sectionIndices[i + 1].Item1;
                    string sectionString = s.Substring(start, end - start);
                    string heading = sectionIndices[i].Item2;
                    if (section.Sections.ContainsKey(heading)) {
                        section.Sections[heading].Add(sectionString);
                    } else {
                        List<string> l = new List<string>();
                        l.Add(sectionString);
                        section.Sections.Add(heading, l);
                    }
                    start = end;
                }
            }


            return section;
        }

        private string LoadFromWiktionary(string search) {
            string url = "https://en.wiktionary.org/wiki/" + search;
            return Util.ExtractHTMLFromWebsite(url);
        }

        private void Translate(FullTranslation fullTranslations, string word, bool searchLinks) {

            string s = LoadFromWiktionary(word);
            //  string s = LoadFromFile("in.txt");

            // find the Latin section
            s = RefineLanguage(s);

            //  Console.WriteLine(s);

            // remove links
            //  Regex rgxLink = new Regex(patterns[3]);
            //s = rgxLink.Replace(s, patterns[4]);

            // divide into the different grammatical sections in case the word has multiple unrelated meanings
            Section sections = SplitSections(s);

            foreach (string heading in sections.Sections.Keys) {
                if (heading.Equals("References")) {

                } else if (heading.Equals("Pronunciation")) {

                } else if (heading.Equals("Etymology")) {

                } else if (heading.Equals("Inflection")) {

                } else if (heading.Equals("Descendants")) {

                } else if (heading.Equals("Antonyms")) {

                } else if (heading.Equals("Synonyms")) {

                } else if (heading.Equals("Translations")) {

                } else if (heading.Equals("Anagrams")) {

                } else if (heading.Equals("Declension")) {

                } else if (heading.Equals("Conjugation")) {

                } else if (heading.Equals("Alternative forms")) {

                } else if (heading.Equals("Usage notes")) {

                } else if (heading.Equals("Derived terms")) {

                } else if (heading.Equals("See also")) {

                } else if (heading.Equals("Further reading")) {

                } else if (heading.Equals("Related terms")) {

                } else if (heading.Equals("Coordinate terms")) {

                } else if (heading.Equals(language)) {

                } else {
                    //    Console.WriteLine(heading);
                    List<string> list = sections.Sections[heading];
                    foreach (string str in list) {

                        Regex rgx = new Regex("<ol>([\\s\\S]+)</ol>");
                        Match m = rgx.Match(str);

                        while (m.Success) {
                            // finds the <ol>...</ol> section
                            // represents an entire translation section
                            string c = m.Groups[1].Captures[0].ToString();
                            //Console.WriteLine(c);

                            c = c.RemoveNestedElements("<dl>", "</dl>", 1);
                            c = c.RemoveNestedElements("<ul>", "</ul>", 1);

                            // remove nested lists
                            c = c.RemoveNestedElements("<li>", "</li>", 2);

                            Regex rgxLine = new Regex("<li>([\\s\\S]+?)</li>");
                            Match mLine = rgxLine.Match(c);
                            while (mLine.Success) {

                                // finds the <li>...</li> section
                                // represents a single translation line
                                string captureLi = mLine.Groups[1].Captures[0].ToString();
                                // Console.WriteLine(captureLi);
                                //Console.WriteLine("-");


                                fullTranslations.AddTranslation(heading, captureLi, language, searchLinks);

                                mLine = mLine.NextMatch();
                            }


                            //   Group g = m.Groups[2];
                            // Capture c = g.Captures[0];

                            // Console.WriteLine(c);

                            //translations.Add(c.ToString());

                            m = m.NextMatch();
                        }
                    }
                }
            }
        }

    }

    class Section {

        // heading, actual content
        public Dictionary<string, List<string>> Sections;

        public Section() {
            Sections = new Dictionary<string, List<string>>();
        }
    }

    class WordTranslation {

        public string original;
        public List<Translation> translations;

        public WordTranslation(string original) {
            this.original = original;
            translations = new List<Translation>();
        }

        public void Add(Translation s) {
            translations.Add(s);
        }
    }

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
                sb.AppendLine(tr.g.ToString() + ":");
                string s = tr.s;
                s = Regex.Replace(s, "\n", "\n\t");
                sb.AppendLine("\t" + s);
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
            s = Regex.Replace(s, "[\\s]{2,}", " ");
            s = Regex.Replace(s, "[\\s]+?\\.", ".");


            /*
            s = Regex.Replace(s, "<a href=[\\s\\S]*?>([\\s\\S]+?)</a>", "$1");

            // replace span class with just the text inside
            s = Util.IterateRegexReplace(s, "<span[^<>]+?>([\\s\\S]*?)</span>", "$1");

            // remove superscripts
            s = Regex.Replace(s, "<sup [^<>]+?>[^<>]*?</sup>", "");
            s = Regex.Replace(s, "<sup>([^<>]+?)</sup>", "$1");



            // replace bolded words with just the word
            s = Regex.Replace(s, "<b>([\\s\\S]+?)</b>", "$1");

            // replace italicised words with just the word
            s = Regex.Replace(s, "<i[\\s\\S]*?>([\\s\\S]+?)</i>", "$1");

            s = Regex.Replace(s, "<strong class=\"[^<>]+?\">([^<>]+?)</strong>", "$1");


            // removing links involving Latin inflection
            s = Regex.Replace(s, "<i class=\"Latn mention\" lang=\"la\"[\\w\\W]*?>([\\w\\W]+?)</i>", "\"$1\"");

            // self links
            s = Regex.Replace(s, "<a class=\"mw-selflink selflink\">([\\w\\W]+?)</a>", "$1");


            // remove quotes
            s = Regex.Replace(s, "<div class=[\\s\\S]*</div>", "");
            s = Regex.Replace(s, "<dd>[\\w\\W]+?</dd>", "");
            s = Regex.Replace(s, "</dd>[\\w\\W]+?</dl>", "");
            s = Regex.Replace(s, "<ul>[\\s\\S]*</ul>", "");


            s = Regex.Replace(s, "<code>[^<>]*?</code>", "");



            // remove unnecessary new lines
            s = Regex.Replace(s, "\\n", "");

            // remove double spaces
            s = Regex.Replace(s, "[\\s]{2,}", " ");

            // misc
            s = Regex.Replace(s, "<ol></ol>", "");
            s = Regex.Replace(s, "<ol><li>", "");
            s = Regex.Replace(s, "</div>", "");*/

            s = s.Trim();

            if (string.IsNullOrWhiteSpace(s)) return;

            if (s.Contains("<") || s.Contains(">")) {
                Console.WriteLine("WARNING - String not parsed correctly: " + s);
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
