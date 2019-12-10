using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DECCA2
{
    public partial class SystemHorizTail : BaseWindow
    {
        Double W_to, V_c, S_w, AR, tr_w, i_w, b_w, alpha_twist,MAC_w, S_h,Cl,Cm_0, Cm_0_wf, l_L, L_f;

        Double X_ac_wf,X_cg_fuse,cg_ac_distance,X_cg,X_cg_w,Cl_h,Cl_ah_2D, Cl_ah_3D,a_h,AR_h, Cl_aw, Cm_a, cr_w;

        private void cmbAirConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetConfig();
        }

        private void cmbAircft_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetVhAndVvForAircraftType();
            label8.Text = V_h.ToString();
        }

        private void pb_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txt_Kc_TextChanged(object sender, EventArgs e)
        {
            try
            {

                CalculateLopt();
                GetHorWingParameters();
                LiftingLine();
                GetVerWingParameters();
            }
            catch
            {
                return;
            }
        }

        Double sw_angle_w, dihedral_w, Cl_a, V_h,V_v,Df_max,L_opt,K_c,Side_Eff;

        public SystemHorizTail(Double Wto, Double Vc, Double Sw, Double Ar, Double trw,
            Double iw, Double alptwist, Double MAC, Double bw, Double crw, Double MacW)
        {
            InitializeComponent();

            //Double Wto, Double Vc,Double Sw, Double Ar,
            //Double trw, Double iw, Double alptwist, Double MAC, Double bw

            
            W_to = Wto;
            V_c = Vc;
            S_w = Sw;
            AR = Ar;
            tr_w = trw;
            i_w = iw;
            alpha_twist = alptwist;
            cr_w = crw;
            b_w = bw;
            MAC_w = MacW;

            /*
            W_to = 850;
            V_c = 95*0.5144447;
            S_w = 18;
            AR = 28;
            tr_w = 0.8;
            i_w = 3;
            alpha_twist = -1.1;
            MAC_w = 0.8;
            b_w = Math.Sqrt(AR * S_w);
            */
        }

        private void SystemHorizTail_Load(object sender, EventArgs e)
        {
            LoadCombos.LoadAircraftType(cmbAircft_Type);
            LoadCombos.LoadDistanceCombos(cmb_Hcr, "ft");
            LoadConfigs();
            SetVhAndVvForAircraftType();
        }

        private void SetVhAndVvForAircraftType()
        {
            if (cmbAircft_Type.Text == "Jet Transport")
            {
                V_h = 1.0;
                V_v = 0.09;
            }
            else if (cmbAircft_Type.Text == "Business Jet")
            {
                V_h = 1.1;
                V_v = 0.09;
            }
            else if (cmbAircft_Type.Text == "Sailplane (glider)")
            {
                V_h = 0.6;
                V_v = 0.03;
            }
            else if (cmbAircft_Type.Text == "Home-built")
            {
                V_h = 0.5;
                V_v = 0.04;
            }
            else if (cmbAircft_Type.Text == "Supersonic Fighter")
            {
                V_h = 0.4;
                V_v = 0.07;
            }
            else if (cmbAircft_Type.Text == "Subsonic Military")
            {
                V_h = 0.7;
                V_v = 0.06;
            }
            else if (cmbAircft_Type.Text == "GA-single Engine")
            {
                V_h = 0.7;
                V_v = 0.04;
            }
            else if (cmbAircft_Type.Text == "GA-twin Engine")
            {
                V_h = 0.8;
                V_v = 0.07;
            }
        }

        private void SetConfig()
        {
            if (cmbAirConfig.Text == "Engine in Nose -- Aft Tail")
            {
                l_L = 0.6;
            }
            else if (cmbAirConfig.Text == "Glider -- Aft Tail")
            {
                l_L = 0.65;
            }
            else if (cmbAirConfig.Text == "Engine above Wing -- Aft Tail")
            {
                l_L = 0.55;
            }
            else if (cmbAirConfig.Text == "Engine on Aft Fuselage -- Aft Tail")
            {
                l_L = 0.45;
            }
            else if (cmbAirConfig.Text == "Engine under Wing -- Aft Tail")
            {
                l_L = 0.5;
            }
            else if (cmbAirConfig.Text == "Canard Aircraft")
            {
                l_L = 0.4;
            }
            else if (cmbAirConfig.Text == "Engine inside Fuselage")
            {
                l_L = 0.3;
            }

        }

        private void GetParameters()
        {
            //Cl_a = Double.Parse(txt_Clalpha.Text);
            sw_angle_w = Double.Parse(txt_WingSweep.Text);
            //dihedral_w = Double.Parse(txt_WingDihedral.Text);
            Df_max = Double.Parse(txt_Dfmax.Text);
            K_c = Double.Parse(txt_Kc.Text);
            Cm_0 = Double.Parse(txt_Cm0.Text);

            X_cg_fuse = Double.Parse(txt_Xcg_fuse.Text);
            X_ac_wf = Double.Parse(txt_X_ac_wf.Text);
            cg_ac_distance = Double.Parse(txt_cg_ac_dist.Text);
            Cl_ah_2D = Double.Parse(txt_Cl_ah.Text);
            Cl_aw = Double.Parse(txt_Clalpha.Text);
        }

        private void LoadConfigs()
        {
            cmbAirConfig.Text = "Engine in Nose -- Aft Tail";
            cmbAirConfig.Items.Add("Engine in Nose -- Aft Tail");
            cmbAirConfig.Items.Add("Engine above Wing -- Aft Tail");
            cmbAirConfig.Items.Add("Engine on Aft Fuselage -- Aft Tail");
            cmbAirConfig.Items.Add("Engine under Wing -- Aft Tail");
            cmbAirConfig.Items.Add("Glider -- Aft Tail");
            cmbAirConfig.Items.Add("Canard Aircraft");
            cmbAirConfig.Items.Add("Engine inside Fuselage");
        }

        private void CalculateLopt()
        {            
            GetParameters();
            
            L_opt = Math.Round(K_c * Math.Sqrt((4 * MAC_w * S_w * V_h) / (Math.PI * Df_max)),3);

            S_h = Math.Round(MAC_w * S_w * V_h / L_opt,3);

            AltitudeCalculator.CalcAtmos(txt_Hcr.Text, cmb_Hcr);

            Cl = Math.Round(2*W_to*9.81/(AltitudeCalculator.rho_h*Math.Pow(V_c,2)*S_w),3);

            Cm_0_wf = Math.Round((Cm_0 * (AR * Math.Pow(Math.Cos(sw_angle_w / 57.3), 2)) / (AR + 2 * Math.Cos(sw_angle_w / 57.3))) + 0.01 * alpha_twist,3);

            L_f = Math.Round(L_opt / l_L,3);

            X_cg = Math.Round(X_ac_wf * MAC_w - cg_ac_distance,3);

            X_cg_w = Math.Round(X_cg / MAC_w, 3);

            Cl_h = Math.Round((Cm_0_wf + Cl * (X_cg - X_ac_wf)) / V_h, 3);

            Cl_ah_3D = Math.Round(Cl_ah_2D / (1 + (Cl_ah_2D / (3.14* AR_h))), 2);

            //a_h = Math.Round((Cl_h / Cl_ah_3D)*57.3,2);

            AR_h = Math.Round(2 * AR/3,1);

            txt_Lopt.Text = L_opt.ToString();
            txt_Sh.Text = S_h.ToString();
            txt_Cl.Text = Cl.ToString();
            txt_Cm0wf.Text = Cm_0_wf.ToString();
            txt_Lf.Text = L_f.ToString();

            txt_Xcg.Text = X_cg.ToString();
            txt_X_cg_h.Text = X_cg_w.ToString();
            txt_Cl_h.Text = Cl_h.ToString();
            txt_Cla3D.Text = Cl_ah_3D.ToString();
            //txt_ah.Text = a_h.ToString();

            a_h = Double.Parse(txt_ah.Text);
        }

        List<Double> LHS, mu, z, c, theta_set, res, Cl1;
        Double[,] B;

        Double b_h, MAC_h, cr_h, alpha0_h, alpha_twist_h,tr_h, Cl_horz, e0,e_alpha;
        
        int N;

        private void GetHorWingParameters()
        {
            N = Int16.Parse(txt_Segments.Text); 

            B = new Double[N, N];
            
            tr_h = tr_w;

            b_h = Math.Round(Math.Sqrt(AR_h * S_h), 4);
            txt_WingSpanh.Text = b_h.ToString();

            MAC_h = Math.Round(S_h / b_h, 4);
            txt_MACh.Text = MAC_h.ToString();

            cr_h = Math.Round((1.5 * MAC_h * (1 + tr_h)) / (1 + tr_h + Math.Pow(tr_h, 2)), 4);
            txt_RootChordh.Text = cr_h.ToString();

            alpha0_h = 0.00001;           

            alpha_twist_h = 0.00001;
        }

        Double b_v, MAC_v, cr_v, alpha0_v, alpha_twist_v, tr_v, Cl_vert,l_v, S_v, AR_v,i_v, sweep_v, dihedral_v;

        private void GetVerWingParameters()
        {
            l_v = L_opt;

            S_v = Math.Round(MAC_w * b_w * V_v / l_v, 3);

            tr_v = Double.Parse(txt_trv.Text);

            AR_v = Double.Parse(txt_ARv.Text);

            b_v = Math.Round(Math.Sqrt(AR_v * S_v),3);

            MAC_v = Math.Round(S_v / b_v, 4);

            cr_v = Math.Round((1.5 * MAC_v * (1 + tr_v)) / (1 + tr_v + Math.Pow(tr_v , 2)), 4);

            sweep_v = Double.Parse(txt_sweep_v.Text);

            i_v = 0.00001;

            dihedral_v = 0.00001;

            txt_Sv.Text = S_v.ToString();

            txt_Lv.Text = l_v.ToString();

            txt_ARv.Text = AR_v.ToString();

            txt_MACv.Text = MAC_v.ToString();

            txt_bv.Text = b_v.ToString();

            txt_crv.Text = cr_v.ToString();
        }

        private void LiftingLine()
        {
            LHS = new List<double>();
            mu = new List<double>();
            z = new List<double>();
            c = new List<double>();
            theta_set = new List<double>();
            res = new List<double>();

            for (Double theta = Math.PI / (2 * N); theta <= Math.PI / 2; theta += Math.PI / (2 * N))
            {
                theta_set.Add(theta);
                z.Add((b_h / 2) * Math.Cos(theta));
                Double c1 = cr_h * (1 - (1 - tr_h) * Math.Cos(theta));
                c.Add(c1);
                mu.Add(Math.Round(c1 * Cl_ah_2D / (4 * b_h), 4));
            }

            int count = 0;
            for (Double alpha = a_h + alpha_twist; alpha <= a_h; alpha -= alpha_twist / (N - 1))
            {
                LHS.Add(Math.Round(mu[count] * (alpha - alpha0_h) / 57.3, 4));
                count++;
            }

            listBox1.DataSource = LHS;            

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    B[i, j] = Math.Round(Math.Sin((2 * (j + 1) - 1) * theta_set[i]) * (1 + (mu[i] * (2 * (j + 1) - 1)) / Math.Sin(theta_set[i])), 4);
                }
            }

            MatrixSolver(B, LHS);

            Double[] sum1 = new Double[N];
            Double[] sum2 = new Double[N];

            for (int i = 0; i < N; i++)
            {
                sum1[i] = 0;
                sum2[i] = 0;
                for (int j = 0; j < N; j++)
                {
                    sum1[i] = sum1[i] + (2 * j - 1) * result[j] * Math.Sin((2 * (j + 1) - 1) * theta_set[i]);
                    sum2[i] = Math.Round(sum2[i] + result[j] * Math.Sin((2 * (j + 1) - 1) * theta_set[i]), 4);
                }
            }

            listBox1.DataSource = sum2;

            Cl1 = new List<double>();

            for (int i = 0; i < N; i++)
            {
                Cl1.Add(Math.Round(4 * b_h * sum2[i] / c[i], 4));
            }

            listBox1.DataSource = Cl1;

            Cl_horz = Math.Round(Math.PI * AR_h * result[0], 3);

            txt_Cl_horz.Text = Cl_horz.ToString();

            e0 = Math.Round(2 * Cl * 57.3 / (Math.PI * AR), 3);

            e_alpha = Math.Round(2 * Cl_aw/ (Math.PI * AR), 3);

            Double a1 = Cl_aw * (X_cg - X_ac_wf);

            Double n_h = 0.98;

            Double b1 = Cl_ah_3D * n_h * (S_h / S_w) * ((L_opt / MAC_w) - X_cg) * (1 - e_alpha);

            Cm_a = Math.Round(a1 - b1,1);

            txt_e0.Text = e0.ToString();

            txt_ealpha.Text = e_alpha.ToString();

            txt_Cma.Text = Cm_a.ToString();

            Side_Eff = 0.724 + 3.06 * (S_v / S_w) / (1 + Math.Cos(sw_angle_w / 57.3)) + 0.009 * AR; 

            Double ct_w = cr_w * tr_w;

            Double KI = ct_w / 2;

            //Double AH = 

            Double b_eff = KI;

            txt_side_efficiency.Text = Side_Eff.ToString();

            Double x_cg_f = L_f * X_cg_fuse;

        }

        LinearEquationSolver linear;
        IList<Double> result;

        private void MatrixSolver(Double[,] B, List<Double> LHS)
        {
            linear = new LinearEquationSolver();

            linear.AddLinearEquation(LHS[0], B[0, 0], B[0, 1], B[0, 2], B[0, 3], B[0, 4], B[0, 5], B[0, 6], B[0, 7], B[0, 8]);
            linear.AddLinearEquation(LHS[1], B[1, 0], B[1, 1], B[1, 2], B[1, 3], B[1, 4], B[1, 5], B[1, 6], B[1, 7], B[1, 8]);
            linear.AddLinearEquation(LHS[2], B[2, 0], B[2, 1], B[2, 2], B[2, 3], B[2, 4], B[2, 5], B[2, 6], B[2, 7], B[2, 8]);
            linear.AddLinearEquation(LHS[3], B[3, 0], B[3, 1], B[3, 2], B[3, 3], B[3, 4], B[3, 5], B[3, 6], B[3, 7], B[3, 8]);
            linear.AddLinearEquation(LHS[4], B[4, 0], B[4, 1], B[4, 2], B[4, 3], B[4, 4], B[4, 5], B[4, 6], B[4, 7], B[4, 8]);
            linear.AddLinearEquation(LHS[5], B[5, 0], B[5, 1], B[5, 2], B[5, 3], B[5, 4], B[5, 5], B[5, 6], B[5, 7], B[5, 8]);
            linear.AddLinearEquation(LHS[6], B[6, 0], B[6, 1], B[6, 2], B[6, 3], B[6, 4], B[6, 5], B[6, 6], B[6, 7], B[6, 8]);
            linear.AddLinearEquation(LHS[7], B[7, 0], B[7, 1], B[7, 2], B[7, 3], B[7, 4], B[7, 5], B[7, 6], B[7, 7], B[7, 8]);
            linear.AddLinearEquation(LHS[8], B[8, 0], B[8, 1], B[8, 2], B[8, 3], B[8, 4], B[8, 5], B[8, 6], B[8, 7], B[8, 8]);


            result = linear.Solve();

        }
    }
}
