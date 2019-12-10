namespace DECCA2
{
    partial class DeccaWebSearch
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
            this.MinBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.keywordstxt = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.BackBtn = new System.Windows.Forms.Button();
            this.ForwardBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.converttxt = new System.Windows.Forms.TextBox();
            this.convertitems = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CopyScreenBtn = new System.Windows.Forms.Button();
            this.webbrowser1 = new System.Windows.Forms.WebBrowser();
            this.LinkClickBtn = new System.Windows.Forms.Button();
            this.ResultsCbx = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // MinBtn
            // 
            this.MinBtn.BackColor = System.Drawing.Color.Black;
            this.MinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinBtn.ForeColor = System.Drawing.Color.Teal;
            this.MinBtn.Location = new System.Drawing.Point(946, 12);
            this.MinBtn.Name = "MinBtn";
            this.MinBtn.Size = new System.Drawing.Size(30, 30);
            this.MinBtn.TabIndex = 0;
            this.MinBtn.Text = "--";
            this.MinBtn.UseVisualStyleBackColor = false;
            // 
            // CloseBtn
            // 
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.ForeColor = System.Drawing.Color.Teal;
            this.CloseBtn.Location = new System.Drawing.Point(982, 12);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(30, 30);
            this.CloseBtn.TabIndex = 0;
            this.CloseBtn.Text = "X";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Decca Web Search";
            // 
            // keywordstxt
            // 
            this.keywordstxt.BackColor = System.Drawing.Color.Black;
            this.keywordstxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keywordstxt.ForeColor = System.Drawing.Color.Teal;
            this.keywordstxt.Location = new System.Drawing.Point(145, 68);
            this.keywordstxt.Multiline = true;
            this.keywordstxt.Name = "keywordstxt";
            this.keywordstxt.Size = new System.Drawing.Size(773, 30);
            this.keywordstxt.TabIndex = 2;
            // 
            // SearchBtn
            // 
            this.SearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchBtn.ForeColor = System.Drawing.Color.Teal;
            this.SearchBtn.Location = new System.Drawing.Point(931, 68);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(81, 30);
            this.SearchBtn.TabIndex = 3;
            this.SearchBtn.Text = "Search";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBtn.ForeColor = System.Drawing.Color.Teal;
            this.BackBtn.Location = new System.Drawing.Point(12, 68);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(45, 30);
            this.BackBtn.TabIndex = 3;
            this.BackBtn.Text = "<==";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // ForwardBtn
            // 
            this.ForwardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForwardBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForwardBtn.ForeColor = System.Drawing.Color.Teal;
            this.ForwardBtn.Location = new System.Drawing.Point(76, 68);
            this.ForwardBtn.Name = "ForwardBtn";
            this.ForwardBtn.Size = new System.Drawing.Size(45, 30);
            this.ForwardBtn.TabIndex = 3;
            this.ForwardBtn.Text = "==>";
            this.ForwardBtn.UseVisualStyleBackColor = true;
            this.ForwardBtn.Click += new System.EventHandler(this.ForwardBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartBtn.ForeColor = System.Drawing.Color.Teal;
            this.StartBtn.Location = new System.Drawing.Point(12, 489);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(81, 30);
            this.StartBtn.TabIndex = 5;
            this.StartBtn.Text = "Start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PauseBtn.ForeColor = System.Drawing.Color.Teal;
            this.PauseBtn.Location = new System.Drawing.Point(129, 489);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(81, 30);
            this.PauseBtn.TabIndex = 6;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopBtn.ForeColor = System.Drawing.Color.Teal;
            this.StopBtn.Location = new System.Drawing.Point(246, 489);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(81, 30);
            this.StopBtn.TabIndex = 7;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = true;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // converttxt
            // 
            this.converttxt.BackColor = System.Drawing.Color.Black;
            this.converttxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.converttxt.ForeColor = System.Drawing.Color.Teal;
            this.converttxt.Location = new System.Drawing.Point(12, 554);
            this.converttxt.Multiline = true;
            this.converttxt.Name = "converttxt";
            this.converttxt.Size = new System.Drawing.Size(1000, 98);
            this.converttxt.TabIndex = 8;
            // 
            // convertitems
            // 
            this.convertitems.BackColor = System.Drawing.Color.Black;
            this.convertitems.ForeColor = System.Drawing.Color.Teal;
            this.convertitems.FormattingEnabled = true;
            this.convertitems.Location = new System.Drawing.Point(12, 683);
            this.convertitems.Name = "convertitems";
            this.convertitems.Size = new System.Drawing.Size(1000, 69);
            this.convertitems.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(9, 538);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Converted Text:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(9, 667);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Converted Items:";
            // 
            // CopyScreenBtn
            // 
            this.CopyScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyScreenBtn.ForeColor = System.Drawing.Color.Teal;
            this.CopyScreenBtn.Location = new System.Drawing.Point(931, 489);
            this.CopyScreenBtn.Name = "CopyScreenBtn";
            this.CopyScreenBtn.Size = new System.Drawing.Size(81, 30);
            this.CopyScreenBtn.TabIndex = 7;
            this.CopyScreenBtn.Text = "Copy Screen";
            this.CopyScreenBtn.UseVisualStyleBackColor = true;
            this.CopyScreenBtn.Click += new System.EventHandler(this.CopyScreenBtn_Click);
            // 
            // webbrowser1
            // 
            this.webbrowser1.Location = new System.Drawing.Point(11, 110);
            this.webbrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webbrowser1.Name = "webbrowser1";
            this.webbrowser1.Size = new System.Drawing.Size(1000, 360);
            this.webbrowser1.TabIndex = 11;
            // 
            // LinkClickBtn
            // 
            this.LinkClickBtn.Location = new System.Drawing.Point(674, 18);
            this.LinkClickBtn.Name = "LinkClickBtn";
            this.LinkClickBtn.Size = new System.Drawing.Size(75, 23);
            this.LinkClickBtn.TabIndex = 12;
            this.LinkClickBtn.Text = "Click Link";
            this.LinkClickBtn.UseVisualStyleBackColor = true;
            this.LinkClickBtn.Click += new System.EventHandler(this.LinkClickBtn_Click);
            // 
            // ResultsCbx
            // 
            this.ResultsCbx.FormattingEnabled = true;
            this.ResultsCbx.Location = new System.Drawing.Point(507, 27);
            this.ResultsCbx.Name = "ResultsCbx";
            this.ResultsCbx.Size = new System.Drawing.Size(121, 21);
            this.ResultsCbx.TabIndex = 13;
            // 
            // DeccaWebSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 764);
            this.Controls.Add(this.ResultsCbx);
            this.Controls.Add(this.LinkClickBtn);
            this.Controls.Add(this.webbrowser1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.convertitems);
            this.Controls.Add(this.converttxt);
            this.Controls.Add(this.CopyScreenBtn);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ForwardBtn);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.keywordstxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.MinBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DeccaWebSearch";
            this.Text = "DeccaWebSearch";
            this.Load += new System.EventHandler(this.DeccaWebSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button MinBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keywordstxt;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Button ForwardBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.TextBox converttxt;
        private System.Windows.Forms.ListBox convertitems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CopyScreenBtn;
        private System.Windows.Forms.WebBrowser webbrowser1;
        private System.Windows.Forms.Button LinkClickBtn;
        private System.Windows.Forms.ComboBox ResultsCbx;
    }
}