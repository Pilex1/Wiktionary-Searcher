using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibUtil;

namespace Latin_Vocab_Tester {
    public partial class Form1 : Form {

        private QuestionGenerator generator;
        private QASet curSet;

        public Form1(QuestionGenerator generator) {
            InitializeComponent();
            this.generator = generator;
            NextQuestion();
        }

        private void NextQuestion() {
            curSet = generator.Generate(4);
            lblQuestion.Text = curSet.wordToBeTranslated;
            lblPossibleAnswers.Text = "";
            lblResult.Text = "";
            txtbxAnswer.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            string text = txtbxSearchWord.Text;
            WordInfo info = generator.GetInfo(text);
            tableTranslations.Controls.Clear();

            if (info != null) {
                foreach (string s in info.definitions) {
                    TextBox txtbx = new TextBox();
                    txtbx.Dock = DockStyle.Fill;
                    txtbx.Text = s;
                    tableTranslations.Controls.Add(txtbx);
                }
            }

        }

        private void AddString(List<string> l, string s) {
            s = s.Replace("{", " { ");
            s = s.Replace("}", " } ");
            s = s.Trim();
            s = s.RemoveUnnecessarySpacing();
            l.Add(s);
        }

        private void confirmButton_Click(object sender, EventArgs e) {
            List<string> l = new List<string>();
            foreach (var d in tableTranslations.Controls) {
                TextBox t = (TextBox)d;
                if (t.Text != "") {
                    AddString(l, t.Text);
                }
            }
            if (txtbxNewTranslation.Text != "") {
                AddString(l, txtbxNewTranslation.Text);
            }
            generator.UpdateTranslation(txtbxSearchWord.Text, l);

            txtbxNewTranslation.Clear();
            textBox1_TextChanged(null, null);
        }

        private void txtbxNewTranslation_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                confirmButton_Click(null, null);
            }
        }

        private void txtbxAnswer_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                if (lblPossibleAnswers.Text == "") {
                    foreach (string ans in curSet.possibleAnswers) {
                        lblPossibleAnswers.Text += Environment.NewLine + ans;
                    }
                } else if (lblResult.Text == "") {
                    string s = txtbxAnswer.Text;
                    int i = 0;
                    if (int.TryParse(s, out i)) {
                        if (i == curSet.indexOfCorrectAnswer + 1) {
                            OnCorrect();
                        } else {
                            OnIncorrect();
                        }
                    } else {
                        OnIncorrect();
                    }
                } else {
                    NextQuestion();
                }
            }
        }

        private void OnCorrect() {
            lblResult.Text = "Correct";
            lblResult.ForeColor = Color.DarkGreen;
            generator.MarkCorrect(lblQuestion.Text);
        }

        private void OnIncorrect() {
            lblResult.Text = "Incorrect - correct answer is:\n" + curSet.possibleAnswers[curSet.indexOfCorrectAnswer];
            lblResult.ForeColor = Color.DarkRed;
            generator.MarkIncorrect(lblQuestion.Text);
        }
    }
}
