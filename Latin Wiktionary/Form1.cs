using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Latin_Wiktionary {
    public partial class Form1 : Form {

        private HashSet<Uri> urlHistory;

        public Form1() {
            InitializeComponent();
            urlHistory = new HashSet<Uri>();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            string text = textBox1.Text;
            string url = string.Format("https://en.wiktionary.org/wiki/{0}#Latin", text);
            webBrowser1.Url = new Uri(url);





        }

        private void Form1_Load(object sender, EventArgs e) {
            ActiveControl = textBox1;
            AcceptButton = buttonSearch;
            CancelButton = buttonClear;
        }

        private void buttonClear_Click(object sender, EventArgs e) {
            textBox1.Text = "";
        }

        private void buttonBack_Click(object sender, EventArgs e) {
            webBrowser1.GoBack();
        }

        private void buttonForward_Click(object sender, EventArgs e) {
            webBrowser1.GoForward();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
            //  string documentText = webBrowser1.DocumentText;
            //string latinHeader = "<span class=\"mw - headline\" id=\"Latin\">Latin</span>";

            //// chops off start up to Latin section
            //int index = documentText.IndexOf(latinHeader);
            //if (index == -1) {
            //    // there is no Latin section in this URL
            //    return;
            //}
            //documentText = documentText.Substring(index + latinHeader.Length);

            //// chops off end of Latin section
            //int index2 = documentText.IndexOf("<hr>");
            //if (index2 != -1) {
            //    documentText = documentText.Substring(0, index2);
            //}

            //Console.WriteLine(documentText);

            urlHistory.Add(e.Url);

        }

        private void WriteUrl(Uri url, StreamWriter writer) {
            string s = url.ToString();
            string prefix = "https://en.wiktionary.org/wiki/";
            string suffix = "#Latin";

            if (!s.StartsWith(prefix)) {
                return;
            }
            s = s.Substring(prefix.Length);

            if (!s.EndsWith(suffix)) {
                return;
            }
            s = s.Substring(0, s.Length - suffix.Length);

            writer.WriteLine(s);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Plexico\\Latin Wiktionary\\";
            Directory.CreateDirectory(folderPath);

            string fileName = "history.plex";

            using (StreamWriter writer = File.AppendText(folderPath + fileName)) {
                foreach (Uri url in urlHistory) {
                    WriteUrl(url, writer);
                }
            }
        }
    }

}
