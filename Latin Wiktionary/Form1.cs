using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Tests;

namespace Latin_Wiktionary {
    public partial class Form1 : Form {

        private HashSet<string> urlHistory;

        public Form1() {
            InitializeComponent();
            urlHistory = new HashSet<string>();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ActiveControl = textBoxWord;
        }

        private void buttonBack_Click(object sender, EventArgs e) {
            webBrowser1.GoBack();
        }

        private void buttonForward_Click(object sender, EventArgs e) {
            webBrowser1.GoForward();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
            urlHistory.Add(e.Url.ToString());

        }

        private void WriteUrl(string url, StreamWriter writer) {
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
                foreach (string url in urlHistory) {
                    WriteUrl(url, writer);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e) {
            string word = textBoxWord.Text;
            string language = textBoxLanguage.Text;
            string url = string.Format("https://en.wiktionary.org/wiki/{0}#{1}", word, language);
            webBrowser1.Navigate(new Uri(url));
            Translator translator = new Translator(language);
            string parsedContent = translator.Translate(word).ToString();
            textBoxOutput.Text = parsedContent;
        }

        private void textBoxWord_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                textBoxWord.Text = "";
            }
            if (e.KeyCode == Keys.Enter) {
                buttonSearch_Click(null, null);
            }
        }

        private void textBoxLanguage_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Escape) {
                textBoxLanguage.Text = "";
            }
            if (e.KeyCode == Keys.Enter) {
                buttonSearch_Click(null, null);
            }
        }

    }

}
