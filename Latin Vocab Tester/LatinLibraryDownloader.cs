using LibUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Latin_Vocab_Tester {
    public class LatinLibraryDownloader {

        internal static string Dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Plexico/Latin Library Downloader/";
        private static string InternalDir = "Downloads/";

        // author name, link
        private List<Tuple<string, string>> GetLinksToAuthors() {
            string homePage = WebUtil.DownloadHtml("http://www.thelatinlibrary.com/");
            MatchCollection allLinks = Regex.Matches(homePage, "<a href=\"([\\s\\S]+?)\">([\\s\\S]+?)</a>");
            List<Tuple<string, string>> authorLinks = new List<Tuple<string, string>>();
            foreach (Match match in allLinks) {
                string link = match.Groups[1].Captures[0].ToString();
                string author = match.Groups[2].Captures[0].ToString();
                if (link == "credit.html" || link == "about.html" || link == "technical.html" || link == "indices.html" || link == "epubs.html" || link == "ius.html" || link == "misc.html" || link == "christian.html" || link == "medieval.html" || link == "neo.html") continue;
                authorLinks.Add(new Tuple<string, string>(author, "http://www.thelatinlibrary.com/" + link));
            }
            return authorLinks;
        }

        /// <summary>
        /// Tuple - heading, page name, link
        /// </summary>
        private List<Tuple<string, string, string>> GetLinksToPages(string authorUrl) {
            string authorPage = WebUtil.DownloadHtml(authorUrl);
            authorPage = authorPage.ConvertHtmlToXml();
            authorPage = Regex.Replace(authorPage, "<head>[\\s\\S]+?</head>", "");
            authorPage = Regex.Replace(authorPage, "<div class=\"footer\">[\\s\\S]+?</div>", "");
            List<Tuple<string, string>> sections = authorPage.SplitSections((node) => {
                if (node.Name != "h2") return false;
                XmlAttributeCollection attributeCollection = node.Attributes;
                if (attributeCollection == null) return false;
                foreach (XmlAttribute attribute in attributeCollection) {
                    if (attribute.Name == "class" && attribute.Value == "work") {
                        return true;
                    }
                }
                return false;
            });

            List<Tuple<string, string, string>> pageLinks = new List<Tuple<string, string, string>>();
            foreach (Tuple<string, string> section in sections) {
                string heading = section.Item1;
                string content = section.Item2;
                MatchCollection allLinks = Regex.Matches(content, "<a href=\"([\\s\\S]+?)\">([\\s\\S]+?)</a>");
                foreach (Match match in allLinks) {
                    string link = match.Groups[1].Captures[0].ToString();
                    string pageName = match.Groups[2].Captures[0].ToString().Trim();
                    pageLinks.Add(new Tuple<string, string, string>(heading, pageName, "http://www.thelatinlibrary.com/" + link));
                }
            }
            return pageLinks;
        }

        private void DownloadPage(string author, string heading, string page, string link) {
            string content = new TextExtractor(link).Extract();
            StreamWriter writer = new StreamWriter(Dir + InternalDir + author + "/" + heading + "/" + page + ".txt");
            writer.Write(content);
            writer.Dispose();
        }

        public void Download() {
            List<Tuple<string, string>> authorLinks = GetLinksToAuthors();
            foreach (Tuple<string, string> tuple in authorLinks) {
                string author = tuple.Item1;
                string authorLink = tuple.Item2;
                List<Tuple<string, string, string>> pageLinks = GetLinksToPages(authorLink);
                foreach (Tuple<string, string, string> tuple2 in pageLinks) {
                    string heading = tuple2.Item1;
                    string page = tuple2.Item2;
                    string pageLink = tuple2.Item3;
                    Console.WriteLine(author + " - " + heading + " - " + page);
                    Directory.CreateDirectory(Dir + InternalDir + author + "/" + heading);
                    DownloadPage(author, heading, page, pageLink);
                }
            }
        }
    }
}
