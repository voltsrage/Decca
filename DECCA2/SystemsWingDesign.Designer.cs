namespace DECCA2
{
    partial class BaseWindow
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
            this.pb_Min = new System.Windows.Forms.PictureBox();
            this.pb_Max = new System.Windows.Forms.PictureBox();
            this.pb_Exit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Exit)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Min
            // 
            this.pb_Min.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pb_Min.Image = global::DECCA2.Properties.Resources.minus___1514_;
            this.pb_Min.Location = new System.Drawing.Point(797, 12);
            this.pb_Min.Name = "pb_Min";
            this.pb_Min.Size = new System.Drawing.Size(25, 25);
            this.pb_Min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Min.TabIndex = 0;
            this.pb_Min.TabStop = false;
            this.pb_Min.Click += new System.EventHandler(this.pb_Min_Click);
            // 
            // pb_Max
            // 
            this.pb_Max.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pb_Max.Image = global::DECCA2.Properties.Resources.multitasking___1440_;
            this.pb_Max.Location = new System.Drawing.Point(838, 12);
            this.pb_Max.Name = "pb_Max";
            this.pb_Max.Size = new System.Drawing.Size(25, 25);
            this.pb_Max.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Max.TabIndex = 0;
            this.pb_Max.TabStop = false;
            this.pb_Max.Click += new System.EventHandler(this.pb_Max_Click);
            // 
            // pb_Exit
            // 
            this.pb_Exit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pb_Exit.Image = global::DECCA2.Properties.Resources.close___1511_;
            this.pb_Exit.Location = new System.Drawing.Point(879, 12);
            this.pb_Exit.Name = "pb_Exit";
            this.pb_Exit.Size = new System.Drawing.Size(25, 25);
            this.pb_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_Exit.TabIndex = 0;
            this.pb_Exit.TabStop = false;
            // 
            // BaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(916, 560);
            this.Controls.Add(this.pb_Max);
            this.Controls.Add(this.pb_Min);
            this.Controls.Add(this.pb_Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseWindow";
            this.Text = "SystemsWingDesign";
            this.Load += new System.EventHandler(this.BaseWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Exit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PictureBox pb_Exit;
        protected System.Windows.Forms.PictureBox pb_Max;
        protected System.Windows.Forms.PictureBox pb_Min;
    }
}