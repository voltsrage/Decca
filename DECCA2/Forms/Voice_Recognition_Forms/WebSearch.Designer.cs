namespace DECCA2
{
    partial class WebSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.txt_Search = new System.Windows.Forms.TextBox();
            this.txt_Back = new System.Windows.Forms.Button();
            this.txt_Forward = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(27, 94);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(743, 299);
            this.webBrowser1.TabIndex = 0;
            // 
            // txt_Search
            // 
            this.txt_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Search.Location = new System.Drawing.Point(162, 37);
            this.txt_Search.Multiline = true;
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.Size = new System.Drawing.Size(485, 35);
            this.txt_Search.TabIndex = 1;
            // 
            // txt_Back
            // 
            this.txt_Back.Location = new System.Drawing.Point(27, 37);
            this.txt_Back.Name = "txt_Back";
            this.txt_Back.Size = new System.Drawing.Size(47, 34);
            this.txt_Back.TabIndex = 2;
            this.txt_Back.Text = "<==";
            this.txt_Back.UseVisualStyleBackColor = true;
            this.txt_Back.Click += new System.EventHandler(this.txt_Back_Click);
            // 
            // txt_Forward
            // 
            this.txt_Forward.Location = new System.Drawing.Point(94, 37);
            this.txt_Forward.Name = "txt_Forward";
            this.txt_Forward.Size = new System.Drawing.Size(47, 35);
            this.txt_Forward.TabIndex = 2;
            this.txt_Forward.Text = "==>";
            this.txt_Forward.UseVisualStyleBackColor = true;
            this.txt_Forward.Click += new System.EventHandler(this.txt_Forward_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(653, 36);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(117, 37);
            this.btn_Search.TabIndex = 2;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // WebSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txt_Forward);
            this.Controls.Add(this.txt_Back);
            this.Controls.Add(this.txt_Search);
            this.Controls.Add(this.webBrowser1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WebSearch";
            this.Text = "WebSearch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox txt_Search;
        private System.Windows.Forms.Button txt_Back;
        private System.Windows.Forms.Button txt_Forward;
        private System.Windows.Forms.Button btn_Search;
    }
}