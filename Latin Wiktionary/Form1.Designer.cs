namespace Latin_Wiktionary {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelOutput = new System.Windows.Forms.Label();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLanguage = new System.Windows.Forms.TextBox();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.labelOutput, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonForward, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonBack, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonSearch, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBoxLanguage, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxWord, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(813, 499);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelOutput
            // 
            this.labelOutput.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelOutput, 4);
            this.labelOutput.Location = new System.Drawing.Point(3, 100);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(0, 17);
            this.labelOutput.TabIndex = 0;
            // 
            // buttonForward
            // 
            this.buttonForward.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonForward.Location = new System.Drawing.Point(3, 3);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(115, 44);
            this.buttonForward.TabIndex = 1;
            this.buttonForward.Text = "▶";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBack.Location = new System.Drawing.Point(3, 53);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(115, 44);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.Text = "◀";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // webBrowser1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.webBrowser1, 4);
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 382);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(807, 114);
            this.webBrowser1.TabIndex = 5;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSearch.Location = new System.Drawing.Point(692, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.tableLayoutPanel1.SetRowSpan(this.buttonSearch, 2);
            this.buttonSearch.Size = new System.Drawing.Size(118, 94);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(124, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Language";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Word";
            // 
            // textBoxLanguage
            // 
            this.textBoxLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLanguage.Location = new System.Drawing.Point(245, 3);
            this.textBoxLanguage.Name = "textBoxLanguage";
            this.textBoxLanguage.Size = new System.Drawing.Size(441, 22);
            this.textBoxLanguage.TabIndex = 9;
            this.textBoxLanguage.Text = "English";
            this.textBoxLanguage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLanguage_KeyDown);
            // 
            // textBoxWord
            // 
            this.textBoxWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxWord.Location = new System.Drawing.Point(245, 53);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(441, 22);
            this.textBoxWord.TabIndex = 10;
            this.textBoxWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxWord_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 499);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Latin Wiktionary - Copyright Alex Tan 2017";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLanguage;
        private System.Windows.Forms.TextBox textBoxWord;
    }
}

