namespace DECCA2
{
    partial class DeccaEmail
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
            this.txt_MessFrom = new System.Windows.Forms.TextBox();
            this.txt_MessTit = new System.Windows.Forms.TextBox();
            this.txt_MessTag = new System.Windows.Forms.TextBox();
            this.txt_MessSum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_NumEmails = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_MessFrom
            // 
            this.txt_MessFrom.Location = new System.Drawing.Point(29, 29);
            this.txt_MessFrom.Multiline = true;
            this.txt_MessFrom.Name = "txt_MessFrom";
            this.txt_MessFrom.Size = new System.Drawing.Size(285, 37);
            this.txt_MessFrom.TabIndex = 0;
            // 
            // txt_MessTit
            // 
            this.txt_MessTit.Location = new System.Drawing.Point(29, 92);
            this.txt_MessTit.Multiline = true;
            this.txt_MessTit.Name = "txt_MessTit";
            this.txt_MessTit.Size = new System.Drawing.Size(285, 37);
            this.txt_MessTit.TabIndex = 0;
            // 
            // txt_MessTag
            // 
            this.txt_MessTag.Location = new System.Drawing.Point(29, 160);
            this.txt_MessTag.Multiline = true;
            this.txt_MessTag.Name = "txt_MessTag";
            this.txt_MessTag.Size = new System.Drawing.Size(285, 37);
            this.txt_MessTag.TabIndex = 0;
            // 
            // txt_MessSum
            // 
            this.txt_MessSum.Location = new System.Drawing.Point(29, 229);
            this.txt_MessSum.Multiline = true;
            this.txt_MessSum.Name = "txt_MessSum";
            this.txt_MessSum.Size = new System.Drawing.Size(285, 209);
            this.txt_MessSum.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Message From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "&Message Title:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "&Message Summary:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "&Total Emails:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "&Message Tag Line:";
            // 
            // lbl_NumEmails
            // 
            this.lbl_NumEmails.AutoSize = true;
            this.lbl_NumEmails.Location = new System.Drawing.Point(292, 13);
            this.lbl_NumEmails.Name = "lbl_NumEmails";
            this.lbl_NumEmails.Size = new System.Drawing.Size(13, 13);
            this.lbl_NumEmails.TabIndex = 1;
            this.lbl_NumEmails.Text = "0";
            // 
            // DeccaEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_NumEmails);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_MessSum);
            this.Controls.Add(this.txt_MessTag);
            this.Controls.Add(this.txt_MessTit);
            this.Controls.Add(this.txt_MessFrom);
            this.Name = "DeccaEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decca Email Reader";
            this.Load += new System.EventHandler(this.DeccaEmail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_MessFrom;
        private System.Windows.Forms.TextBox txt_MessTit;
        private System.Windows.Forms.TextBox txt_MessTag;
        private System.Windows.Forms.TextBox txt_MessSum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_NumEmails;
    }
}