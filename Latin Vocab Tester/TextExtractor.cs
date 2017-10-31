using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibUtil;
using System.Text.RegularExpressions;

namespace Latin_Vocab_Tester {
    public class TextExtractor {

        public enum ExtractOptions {
            Full, Raw
        }

        private string url;

        public TextExtractor(string url) {
            this.url = url;
        }

        public string Extract() {
            string raw = WebUtil.DownloadHtml(url);
            if (raw == null) return "";
            // string xml = raw.ConvertHtmlToXml();
            raw = Regex.Replace(raw, "<head>[\\s\\S]*</head>", "");
            raw = Regex.Replace(raw, "<p class=pagehead>[\\s\\S]*?</p>", "");
            raw = Regex.Replace(raw, "<P class=border>[\\s\\S]*?</P>", "");
            raw = raw.ConvertHtmlToXml();
            string extracted = raw.CleanXml((node) => {
                bool keepInsideContent = false;
                if (node.Name.Equals("p") && node.Attributes.Count == 0) keepInsideContent = true;
                if (node.Name.Equals("html")) keepInsideContent = true;
                if (node.Name.Equals("body")) keepInsideContent = true;
                if (node.Name.Equals("div")) keepInsideContent = true;
                if (node.Name.Equals("i")) keepInsideContent = true;
                if (node.Name.Equals("b")) keepInsideContent = true;
                return keepInsideContent;
            });
            extracted = extracted.RemoveDoubleSpaces().RemoveDoubleNewlines();
            extracted = Regex.Replace(extracted, "\\[.*?\\]", "");
            extracted = Regex.Replace(extracted, "\n ", "\n");
            extracted = extracted.Trim();
            return extracted;
        }
    }
}
