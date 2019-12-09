namespace DECCA2
{
    partial class WingEstimateForm_form
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
            this.label13 = new System.Windows.Forms.Label();
            this.cmbAircft_Type = new System.Windows.Forms.ComboBox();
            this.cmb_cf = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_WsEnter = new System.Windows.Forms.TextBox();
            this.txt_Cl_max_TO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Hto = new System.Windows.Forms.ComboBox();
            this.txt_H_to = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Clmax_L = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Cl_max = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Sto = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_H_landing = new System.Windows.Forms.ComboBox();
            this.txt_H_landing = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_Wl_Wto = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Slanding = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.grp_Landing = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_PwIndex = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Hcr = new System.Windows.Forms.TextBox();
            this.cmb_Hcr = new System.Windows.Forms.ComboBox();
            this.grp_Cruise = new System.Windows.Forms.GroupBox();
            this.gpb_GenInfo = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Stop_prop = new System.Windows.Forms.TextBox();
            this.txt_Lgears = new System.Windows.Forms.TextBox();
            this.txt_TOflaps = new System.Windows.Forms.TextBox();
            this.txt_e = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Lflaps = new System.Windows.Forms.TextBox();
            this.txt_np = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_AR = new System.Windows.Forms.TextBox();
            this.gpb_Ceiling = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmb_Hcl = new System.Windows.Forms.ComboBox();
            this.txt_Hcl = new System.Windows.Forms.TextBox();
            this.txt_Tcl = new System.Windows.Forms.TextBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.grp_Landing.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grp_Cruise.SuspendLayout();
            this.gpb_GenInfo.SuspendLayout();
            this.gpb_Ceiling.SuspendLayout();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Aircraft Type:";
            // 
            // cmbAircft_Type
            // 
            this.cmbAircft_Type.FormattingEnabled = true;
            this.cmbAircft_Type.Location = new System.Drawing.Point(111, 25);
            this.cmbAircft_Type.Name = "cmbAircft_Type";
            this.cmbAircft_Type.Size = new System.Drawing.Size(121, 21);
            this.cmbAircft_Type.TabIndex = 0;
            this.cmbAircft_Type.SelectedIndexChanged += new System.EventHandler(this.cmbAircft_Type_SelectedIndexChanged);
            // 
            // cmb_cf
            // 
            this.cmb_cf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cf.FormattingEnabled = true;
            this.cmb_cf.Location = new System.Drawing.Point(49, 26);
            this.cmb_cf.Name = "cmb_cf";
            this.cmb_cf.Size = new System.Drawing.Size(79, 21);
            this.cmb_cf.TabIndex = 0;
            this.cmb_cf.SelectedIndexChanged += new System.EventHandler(this.cmb_cf_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "C_f:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "W_s:";
            // 
            // txt_WsEnter
            // 
            this.txt_WsEnter.Location = new System.Drawing.Point(99, 19);
            this.txt_WsEnter.Name = "txt_WsEnter";
            this.txt_WsEnter.Size = new System.Drawing.Size(80, 20);
            this.txt_WsEnter.TabIndex = 0;
            this.txt_WsEnter.TextChanged += new System.EventHandler(this.txt_WsEnter_TextChanged);
            // 
            // txt_Cl_max_TO
            // 
            this.txt_Cl_max_TO.Location = new System.Drawing.Point(99, 71);
            this.txt_Cl_max_TO.Name = "txt_Cl_max_TO";
            this.txt_Cl_max_TO.Size = new System.Drawing.Size(80, 20);
            this.txt_Cl_max_TO.TabIndex = 2;
            this.txt_Cl_max_TO.TextChanged += new System.EventHandler(this.txt_Cl_max_TO_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Clmax_TO:";
            // 
            // cmb_Hto
            // 
            this.cmb_Hto.FormattingEnabled = true;
            this.cmb_Hto.Location = new System.Drawing.Point(185, 44);
            this.cmb_Hto.Name = "cmb_Hto";
            this.cmb_Hto.Size = new System.Drawing.Size(50, 21);
            this.cmb_Hto.TabIndex = 27;
            // 
            // txt_H_to
            // 
            this.txt_H_to.Location = new System.Drawing.Point(99, 45);
            this.txt_H_to.Name = "txt_H_to";
            this.txt_H_to.Size = new System.Drawing.Size(80, 20);
            this.txt_H_to.TabIndex = 1;
            this.txt_H_to.TextChanged += new System.EventHandler(this.txt_H_to_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "H_to:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "label1";
            // 
            // txt_Clmax_L
            // 
            this.txt_Clmax_L.Location = new System.Drawing.Point(99, 45);
            this.txt_Clmax_L.Name = "txt_Clmax_L";
            this.txt_Clmax_L.Size = new System.Drawing.Size(80, 20);
            this.txt_Clmax_L.TabIndex = 1;
            this.txt_Clmax_L.TextChanged += new System.EventHandler(this.txt_Clmax_L_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Clmax_L:";
            // 
            // txt_Cl_max
            // 
            this.txt_Cl_max.Location = new System.Drawing.Point(49, 53);
            this.txt_Cl_max.Name = "txt_Cl_max";
            this.txt_Cl_max.Size = new System.Drawing.Size(79, 20);
            this.txt_Cl_max.TabIndex = 1;
            this.txt_Cl_max.TextChanged += new System.EventHandler(this.txt_Cl_max_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Clmax:";
            // 
            // txt_Sto
            // 
            this.txt_Sto.Location = new System.Drawing.Point(99, 97);
            this.txt_Sto.Name = "txt_Sto";
            this.txt_Sto.Size = new System.Drawing.Size(80, 20);
            this.txt_Sto.TabIndex = 3;
            this.txt_Sto.TextChanged += new System.EventHandler(this.txt_Sto_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "S_to:";
            // 
            // cmb_H_landing
            // 
            this.cmb_H_landing.FormattingEnabled = true;
            this.cmb_H_landing.Location = new System.Drawing.Point(185, 19);
            this.cmb_H_landing.Name = "cmb_H_landing";
            this.cmb_H_landing.Size = new System.Drawing.Size(50, 21);
            this.cmb_H_landing.TabIndex = 38;
            // 
            // txt_H_landing
            // 
            this.txt_H_landing.Location = new System.Drawing.Point(99, 19);
            this.txt_H_landing.Name = "txt_H_landing";
            this.txt_H_landing.Size = new System.Drawing.Size(80, 20);
            this.txt_H_landing.TabIndex = 0;
            this.txt_H_landing.TextChanged += new System.EventHandler(this.txt_H_landing_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "H_Landing:";
            // 
            // txt_Wl_Wto
            // 
            this.txt_Wl_Wto.Location = new System.Drawing.Point(99, 97);
            this.txt_Wl_Wto.Name = "txt_Wl_Wto";
            this.txt_Wl_Wto.Size = new System.Drawing.Size(80, 20);
            this.txt_Wl_Wto.TabIndex = 3;
            this.txt_Wl_Wto.TextChanged += new System.EventHandler(this.txt_H_landing_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Wl_Wto:";
            // 
            // txt_Slanding
            // 
            this.txt_Slanding.Location = new System.Drawing.Point(99, 71);
            this.txt_Slanding.Name = "txt_Slanding";
            this.txt_Slanding.Size = new System.Drawing.Size(80, 20);
            this.txt_Slanding.TabIndex = 2;
            this.txt_Slanding.TextChanged += new System.EventHandler(this.txt_H_landing_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "S_landing:";
            // 
            // grp_Landing
            // 
            this.grp_Landing.Controls.Add(this.txt_Wl_Wto);
            this.grp_Landing.Controls.Add(this.label11);
            this.grp_Landing.Controls.Add(this.txt_Slanding);
            this.grp_Landing.Controls.Add(this.label10);
            this.grp_Landing.Controls.Add(this.cmb_H_landing);
            this.grp_Landing.Controls.Add(this.txt_H_landing);
            this.grp_Landing.Controls.Add(this.label12);
            this.grp_Landing.Controls.Add(this.txt_Clmax_L);
            this.grp_Landing.Controls.Add(this.label3);
            this.grp_Landing.Location = new System.Drawing.Point(23, 183);
            this.grp_Landing.Name = "grp_Landing";
            this.grp_Landing.Size = new System.Drawing.Size(257, 126);
            this.grp_Landing.TabIndex = 2;
            this.grp_Landing.TabStop = false;
            this.grp_Landing.Text = "Landing Information";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Sto);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmb_Hto);
            this.groupBox1.Controls.Add(this.txt_H_to);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_Cl_max_TO);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_WsEnter);
            this.groupBox1.Location = new System.Drawing.Point(23, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Takeoff Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "I_p:";
            // 
            // txt_PwIndex
            // 
            this.txt_PwIndex.Location = new System.Drawing.Point(99, 16);
            this.txt_PwIndex.Name = "txt_PwIndex";
            this.txt_PwIndex.Size = new System.Drawing.Size(80, 20);
            this.txt_PwIndex.TabIndex = 0;
            this.txt_PwIndex.TextChanged += new System.EventHandler(this.txt_PwIndex_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "H_cr:";
            // 
            // txt_Hcr
            // 
            this.txt_Hcr.Location = new System.Drawing.Point(99, 42);
            this.txt_Hcr.Name = "txt_Hcr";
            this.txt_Hcr.Size = new System.Drawing.Size(80, 20);
            this.txt_Hcr.TabIndex = 1;
            this.txt_Hcr.TextChanged += new System.EventHandler(this.txt_PwIndex_TextChanged);
            // 
            // cmb_Hcr
            // 
            this.cmb_Hcr.FormattingEnabled = true;
            this.cmb_Hcr.Location = new System.Drawing.Point(185, 42);
            this.cmb_Hcr.Name = "cmb_Hcr";
            this.cmb_Hcr.Size = new System.Drawing.Size(50, 21);
            this.cmb_Hcr.TabIndex = 38;
            // 
            // grp_Cruise
            // 
            this.grp_Cruise.Controls.Add(this.txt_Hcr);
            this.grp_Cruise.Controls.Add(this.txt_PwIndex);
            this.grp_Cruise.Controls.Add(this.label6);
            this.grp_Cruise.Controls.Add(this.label5);
            this.grp_Cruise.Controls.Add(this.cmb_Hcr);
            this.grp_Cruise.Location = new System.Drawing.Point(23, 315);
            this.grp_Cruise.Name = "grp_Cruise";
            this.grp_Cruise.Size = new System.Drawing.Size(257, 84);
            this.grp_Cruise.TabIndex = 3;
            this.grp_Cruise.TabStop = false;
            this.grp_Cruise.Text = "Cruise Information";
            // 
            // gpb_GenInfo
            // 
            this.gpb_GenInfo.Controls.Add(this.label21);
            this.gpb_GenInfo.Controls.Add(this.label20);
            this.gpb_GenInfo.Controls.Add(this.label18);
            this.gpb_GenInfo.Controls.Add(this.label16);
            this.gpb_GenInfo.Controls.Add(this.txt_Stop_prop);
            this.gpb_GenInfo.Controls.Add(this.txt_Lgears);
            this.gpb_GenInfo.Controls.Add(this.txt_TOflaps);
            this.gpb_GenInfo.Controls.Add(this.txt_e);
            this.gpb_GenInfo.Controls.Add(this.label19);
            this.gpb_GenInfo.Controls.Add(this.label17);
            this.gpb_GenInfo.Controls.Add(this.txt_Lflaps);
            this.gpb_GenInfo.Controls.Add(this.txt_np);
            this.gpb_GenInfo.Controls.Add(this.label15);
            this.gpb_GenInfo.Controls.Add(this.txt_AR);
            this.gpb_GenInfo.Controls.Add(this.txt_Cl_max);
            this.gpb_GenInfo.Controls.Add(this.label4);
            this.gpb_GenInfo.Controls.Add(this.label1);
            this.gpb_GenInfo.Controls.Add(this.cmb_cf);
            this.gpb_GenInfo.Controls.Add(this.label14);
            this.gpb_GenInfo.Location = new System.Drawing.Point(286, 25);
            this.gpb_GenInfo.Name = "gpb_GenInfo";
            this.gpb_GenInfo.Size = new System.Drawing.Size(306, 210);
            this.gpb_GenInfo.TabIndex = 5;
            this.gpb_GenInfo.TabStop = false;
            this.gpb_GenInfo.Text = "General Information";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(134, 182);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(88, 13);
            this.label21.TabIndex = 35;
            this.label21.Text = "Cd_0_stop_prop:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(134, 156);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(79, 13);
            this.label20.TabIndex = 35;
            this.label20.Text = "Cd_0_L_gears:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(134, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(84, 13);
            this.label18.TabIndex = 35;
            this.label18.Text = "Cd_0_TO_flaps:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(134, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(16, 13);
            this.label16.TabIndex = 35;
            this.label16.Text = "e:";
            // 
            // txt_Stop_prop
            // 
            this.txt_Stop_prop.Location = new System.Drawing.Point(231, 180);
            this.txt_Stop_prop.Name = "txt_Stop_prop";
            this.txt_Stop_prop.Size = new System.Drawing.Size(63, 20);
            this.txt_Stop_prop.TabIndex = 8;
            this.txt_Stop_prop.Text = "0.005";
            this.txt_Stop_prop.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // txt_Lgears
            // 
            this.txt_Lgears.Location = new System.Drawing.Point(231, 154);
            this.txt_Lgears.Name = "txt_Lgears";
            this.txt_Lgears.Size = new System.Drawing.Size(63, 20);
            this.txt_Lgears.TabIndex = 7;
            this.txt_Lgears.Text = "0.02";
            this.txt_Lgears.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // txt_TOflaps
            // 
            this.txt_TOflaps.Location = new System.Drawing.Point(231, 102);
            this.txt_TOflaps.Name = "txt_TOflaps";
            this.txt_TOflaps.Size = new System.Drawing.Size(63, 20);
            this.txt_TOflaps.TabIndex = 5;
            this.txt_TOflaps.Text = "0.015";
            this.txt_TOflaps.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // txt_e
            // 
            this.txt_e.Location = new System.Drawing.Point(231, 50);
            this.txt_e.Name = "txt_e";
            this.txt_e.Size = new System.Drawing.Size(63, 20);
            this.txt_e.TabIndex = 3;
            this.txt_e.Text = "0.8";
            this.txt_e.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(134, 130);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Cd_0_L_flaps:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(134, 78);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(22, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "np:";
            // 
            // txt_Lflaps
            // 
            this.txt_Lflaps.Location = new System.Drawing.Point(231, 128);
            this.txt_Lflaps.Name = "txt_Lflaps";
            this.txt_Lflaps.Size = new System.Drawing.Size(63, 20);
            this.txt_Lflaps.TabIndex = 6;
            this.txt_Lflaps.Text = "0.06";
            this.txt_Lflaps.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // txt_np
            // 
            this.txt_np.Location = new System.Drawing.Point(231, 76);
            this.txt_np.Name = "txt_np";
            this.txt_np.Size = new System.Drawing.Size(63, 20);
            this.txt_np.TabIndex = 4;
            this.txt_np.Text = "0.8";
            this.txt_np.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(134, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "AR:";
            // 
            // txt_AR
            // 
            this.txt_AR.Location = new System.Drawing.Point(231, 24);
            this.txt_AR.Name = "txt_AR";
            this.txt_AR.Size = new System.Drawing.Size(63, 20);
            this.txt_AR.TabIndex = 2;
            this.txt_AR.Text = "8";
            this.txt_AR.TextChanged += new System.EventHandler(this.txt_AR_TextChanged);
            // 
            // gpb_Ceiling
            // 
            this.gpb_Ceiling.Controls.Add(this.label24);
            this.gpb_Ceiling.Controls.Add(this.label23);
            this.gpb_Ceiling.Controls.Add(this.textBox3);
            this.gpb_Ceiling.Controls.Add(this.label22);
            this.gpb_Ceiling.Controls.Add(this.cmb_Hcl);
            this.gpb_Ceiling.Controls.Add(this.txt_Hcl);
            this.gpb_Ceiling.Controls.Add(this.txt_Tcl);
            this.gpb_Ceiling.Location = new System.Drawing.Point(23, 405);
            this.gpb_Ceiling.Name = "gpb_Ceiling";
            this.gpb_Ceiling.Size = new System.Drawing.Size(257, 111);
            this.gpb_Ceiling.TabIndex = 4;
            this.gpb_Ceiling.TabStop = false;
            this.gpb_Ceiling.Text = "Ceiling Information";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(9, 79);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(41, 13);
            this.label24.TabIndex = 1;
            this.label24.Text = "label22";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(9, 53);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(32, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "H_cl:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(99, 76);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(80, 20);
            this.textBox3.TabIndex = 2;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "T_cl:";
            // 
            // cmb_Hcl
            // 
            this.cmb_Hcl.FormattingEnabled = true;
            this.cmb_Hcl.Location = new System.Drawing.Point(185, 49);
            this.cmb_Hcl.Name = "cmb_Hcl";
            this.cmb_Hcl.Size = new System.Drawing.Size(50, 21);
            this.cmb_Hcl.TabIndex = 38;
            // 
            // txt_Hcl
            // 
            this.txt_Hcl.Location = new System.Drawing.Point(99, 50);
            this.txt_Hcl.Name = "txt_Hcl";
            this.txt_Hcl.Size = new System.Drawing.Size(80, 20);
            this.txt_Hcl.TabIndex = 1;
            this.txt_Hcl.TextChanged += new System.EventHandler(this.txt_Tcl_TextChanged);
            // 
            // txt_Tcl
            // 
            this.txt_Tcl.Location = new System.Drawing.Point(99, 24);
            this.txt_Tcl.Name = "txt_Tcl";
            this.txt_Tcl.Size = new System.Drawing.Size(80, 20);
            this.txt_Tcl.TabIndex = 0;
            this.txt_Tcl.TextChanged += new System.EventHandler(this.txt_Tcl_TextChanged);
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(598, 25);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(748, 581);
            this.cartesianChart1.TabIndex = 50;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // WingEstimateForm_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 658);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.gpb_Ceiling);
            this.Controls.Add(this.gpb_GenInfo);
            this.Controls.Add(this.grp_Cruise);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grp_Landing);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbAircft_Type);
            this.Name = "WingEstimateForm_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Roskam Wing Estimate: Prop";
            this.Load += new System.EventHandler(this.WingEstimateProp_Load);
            this.grp_Landing.ResumeLayout(false);
            this.grp_Landing.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grp_Cruise.ResumeLayout(false);
            this.grp_Cruise.PerformLayout();
            this.gpb_GenInfo.ResumeLayout(false);
            this.gpb_GenInfo.PerformLayout();
            this.gpb_Ceiling.ResumeLayout(false);
            this.gpb_Ceiling.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbAircft_Type;
        private System.Windows.Forms.ComboBox cmb_cf;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_WsEnter;
        private System.Windows.Forms.TextBox txt_Cl_max_TO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Hto;
        private System.Windows.Forms.TextBox txt_H_to;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Clmax_L;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Cl_max;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Sto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_H_landing;
        private System.Windows.Forms.TextBox txt_H_landing;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_Wl_Wto;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Slanding;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grp_Landing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_PwIndex;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Hcr;
        private System.Windows.Forms.ComboBox cmb_Hcr;
        private System.Windows.Forms.GroupBox grp_Cruise;
        private System.Windows.Forms.GroupBox gpb_GenInfo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_Lgears;
        private System.Windows.Forms.TextBox txt_TOflaps;
        private System.Windows.Forms.TextBox txt_e;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_Lflaps;
        private System.Windows.Forms.TextBox txt_np;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_AR;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_Stop_prop;
        private System.Windows.Forms.GroupBox gpb_Ceiling;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_Tcl;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox cmb_Hcl;
        private System.Windows.Forms.TextBox txt_Hcl;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
    }
}