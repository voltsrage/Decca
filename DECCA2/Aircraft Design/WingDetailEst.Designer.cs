namespace DECCA2
{
    partial class WingDetailEst
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Cl_max = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Cl_max_TO = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Clmax_L = new System.Windows.Forms.TextBox();
            this.grp_Clmax = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Vs_flp_up = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Vs_flp_down = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Sto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_H_to = new System.Windows.Forms.TextBox();
            this.cmb_Hto = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_WsEnter = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_Top = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Slanding = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Wl_Wto = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_H_landing = new System.Windows.Forms.TextBox();
            this.cmb_H_landing = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbAircft_Type = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmb_cf = new System.Windows.Forms.ComboBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_Clmax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clmax:";
            // 
            // txt_Cl_max
            // 
            this.txt_Cl_max.Location = new System.Drawing.Point(89, 32);
            this.txt_Cl_max.Name = "txt_Cl_max";
            this.txt_Cl_max.Size = new System.Drawing.Size(100, 20);
            this.txt_Cl_max.TabIndex = 1;
            this.txt_Cl_max.TextChanged += new System.EventHandler(this.txt_Cl_max_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Clmax_TO:";
            // 
            // txt_Cl_max_TO
            // 
            this.txt_Cl_max_TO.Location = new System.Drawing.Point(89, 58);
            this.txt_Cl_max_TO.Name = "txt_Cl_max_TO";
            this.txt_Cl_max_TO.Size = new System.Drawing.Size(100, 20);
            this.txt_Cl_max_TO.TabIndex = 1;
            this.txt_Cl_max_TO.TextChanged += new System.EventHandler(this.txt_Cl_max_TO_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Clmax_L:";
            // 
            // txt_Clmax_L
            // 
            this.txt_Clmax_L.Location = new System.Drawing.Point(89, 84);
            this.txt_Clmax_L.Name = "txt_Clmax_L";
            this.txt_Clmax_L.Size = new System.Drawing.Size(100, 20);
            this.txt_Clmax_L.TabIndex = 1;
            this.txt_Clmax_L.TextChanged += new System.EventHandler(this.txt_Cl_max_TextChanged);
            // 
            // grp_Clmax
            // 
            this.grp_Clmax.Controls.Add(this.txt_Clmax_L);
            this.grp_Clmax.Controls.Add(this.label3);
            this.grp_Clmax.Controls.Add(this.txt_Cl_max_TO);
            this.grp_Clmax.Controls.Add(this.label2);
            this.grp_Clmax.Controls.Add(this.txt_Cl_max);
            this.grp_Clmax.Controls.Add(this.label1);
            this.grp_Clmax.Location = new System.Drawing.Point(31, 43);
            this.grp_Clmax.Name = "grp_Clmax";
            this.grp_Clmax.Size = new System.Drawing.Size(222, 118);
            this.grp_Clmax.TabIndex = 2;
            this.grp_Clmax.TabStop = false;
            this.grp_Clmax.Text = "CL_max\'s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Vs_flp_up:";
            // 
            // txt_Vs_flp_up
            // 
            this.txt_Vs_flp_up.Location = new System.Drawing.Point(120, 183);
            this.txt_Vs_flp_up.Name = "txt_Vs_flp_up";
            this.txt_Vs_flp_up.Size = new System.Drawing.Size(100, 20);
            this.txt_Vs_flp_up.TabIndex = 1;
            this.txt_Vs_flp_up.TextChanged += new System.EventHandler(this.txt_Cl_max_TextChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(491, 54);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(156, 56);
            this.listBox1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(540, 448);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Vs_flp_down:";
            // 
            // txt_Vs_flp_down
            // 
            this.txt_Vs_flp_down.Location = new System.Drawing.Point(120, 209);
            this.txt_Vs_flp_down.Name = "txt_Vs_flp_down";
            this.txt_Vs_flp_down.Size = new System.Drawing.Size(100, 20);
            this.txt_Vs_flp_down.TabIndex = 1;
            this.txt_Vs_flp_down.TextChanged += new System.EventHandler(this.txt_Cl_max_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "S_to:";
            // 
            // txt_Sto
            // 
            this.txt_Sto.Location = new System.Drawing.Point(120, 250);
            this.txt_Sto.Name = "txt_Sto";
            this.txt_Sto.Size = new System.Drawing.Size(100, 20);
            this.txt_Sto.TabIndex = 6;
            this.txt_Sto.TextChanged += new System.EventHandler(this.txt_Sto_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 279);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "H_to:";
            // 
            // txt_H_to
            // 
            this.txt_H_to.Location = new System.Drawing.Point(120, 276);
            this.txt_H_to.Name = "txt_H_to";
            this.txt_H_to.Size = new System.Drawing.Size(100, 20);
            this.txt_H_to.TabIndex = 6;
            this.txt_H_to.TextChanged += new System.EventHandler(this.txt_Sto_TextChanged);
            // 
            // cmb_Hto
            // 
            this.cmb_Hto.FormattingEnabled = true;
            this.cmb_Hto.Location = new System.Drawing.Point(226, 276);
            this.cmb_Hto.Name = "cmb_Hto";
            this.cmb_Hto.Size = new System.Drawing.Size(91, 21);
            this.cmb_Hto.TabIndex = 7;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(491, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(210, 134);
            this.dataGridView1.TabIndex = 8;
            // 
            // txt_WsEnter
            // 
            this.txt_WsEnter.Location = new System.Drawing.Point(120, 341);
            this.txt_WsEnter.Name = "txt_WsEnter";
            this.txt_WsEnter.Size = new System.Drawing.Size(100, 20);
            this.txt_WsEnter.TabIndex = 9;
            this.txt_WsEnter.TextChanged += new System.EventHandler(this.txt_SwEnter_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 344);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "W_s:";
            // 
            // cmb_Top
            // 
            this.cmb_Top.FormattingEnabled = true;
            this.cmb_Top.Location = new System.Drawing.Point(31, 12);
            this.cmb_Top.Name = "cmb_Top";
            this.cmb_Top.Size = new System.Drawing.Size(121, 21);
            this.cmb_Top.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(40, 397);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "S_landing:";
            // 
            // txt_Slanding
            // 
            this.txt_Slanding.Location = new System.Drawing.Point(120, 394);
            this.txt_Slanding.Name = "txt_Slanding";
            this.txt_Slanding.Size = new System.Drawing.Size(100, 20);
            this.txt_Slanding.TabIndex = 13;
            this.txt_Slanding.TextChanged += new System.EventHandler(this.txt_Slanding_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 370);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Wl_Wto:";
            // 
            // txt_Wl_Wto
            // 
            this.txt_Wl_Wto.Location = new System.Drawing.Point(120, 367);
            this.txt_Wl_Wto.Name = "txt_Wl_Wto";
            this.txt_Wl_Wto.Size = new System.Drawing.Size(100, 20);
            this.txt_Wl_Wto.TabIndex = 13;
            this.txt_Wl_Wto.TextChanged += new System.EventHandler(this.txt_Slanding_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 305);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "H_Landing:";
            // 
            // txt_H_landing
            // 
            this.txt_H_landing.Location = new System.Drawing.Point(120, 302);
            this.txt_H_landing.Name = "txt_H_landing";
            this.txt_H_landing.Size = new System.Drawing.Size(100, 20);
            this.txt_H_landing.TabIndex = 6;
            this.txt_H_landing.TextChanged += new System.EventHandler(this.txt_Sto_TextChanged);
            // 
            // cmb_H_landing
            // 
            this.cmb_H_landing.FormattingEnabled = true;
            this.cmb_H_landing.Location = new System.Drawing.Point(226, 302);
            this.cmb_H_landing.Name = "cmb_H_landing";
            this.cmb_H_landing.Size = new System.Drawing.Size(91, 21);
            this.cmb_H_landing.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(186, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Aircraft Type:";
            // 
            // cmbAircft_Type
            // 
            this.cmbAircft_Type.FormattingEnabled = true;
            this.cmbAircft_Type.Location = new System.Drawing.Point(276, 12);
            this.cmbAircft_Type.Name = "cmbAircft_Type";
            this.cmbAircft_Type.Size = new System.Drawing.Size(121, 21);
            this.cmbAircft_Type.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 448);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 16;
            this.label14.Text = "C_f:";
            // 
            // cmb_cf
            // 
            this.cmb_cf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cf.FormattingEnabled = true;
            this.cmb_cf.Location = new System.Drawing.Point(120, 445);
            this.cmb_cf.Name = "cmb_cf";
            this.cmb_cf.Size = new System.Drawing.Size(100, 21);
            this.cmb_cf.TabIndex = 17;
            this.cmb_cf.SelectedIndexChanged += new System.EventHandler(this.cmb_cf_SelectedIndexChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "10";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "20";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 40;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "30";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 40;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "40";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 40;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "50";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 40;
            // 
            // WingDetailEst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 505);
            this.Controls.Add(this.cmb_cf);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbAircft_Type);
            this.Controls.Add(this.txt_Wl_Wto);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txt_Slanding);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmb_Top);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_WsEnter);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmb_H_landing);
            this.Controls.Add(this.cmb_Hto);
            this.Controls.Add(this.txt_H_landing);
            this.Controls.Add(this.txt_H_to);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_Sto);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txt_Vs_flp_down);
            this.Controls.Add(this.txt_Vs_flp_up);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grp_Clmax);
            this.Controls.Add(this.label4);
            this.Name = "WingDetailEst";
            this.Text = "WingDetailEst";
            this.Load += new System.EventHandler(this.WingDetailEst_Load);
            this.grp_Clmax.ResumeLayout(false);
            this.grp_Clmax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Cl_max;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Cl_max_TO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Clmax_L;
        private System.Windows.Forms.GroupBox grp_Clmax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Vs_flp_up;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Vs_flp_down;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Sto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_H_to;
        private System.Windows.Forms.ComboBox cmb_Hto;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_WsEnter;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_Top;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_Slanding;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Wl_Wto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_H_landing;
        private System.Windows.Forms.ComboBox cmb_H_landing;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbAircft_Type;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmb_cf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}