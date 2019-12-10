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
    public partial class WingDetailEst : Form
    {
        List<Double> W_S_list;

        Double [] W_S_TO_L_list;

        Double[] ClmaxTO_list;

        Double[] W_S_list2;

        Double Cl_max, Clmax_TO, Clmax_L,c,d,cf,a,b,AR,e,K;

        Double Vs_flp_up, Vs_flp_down,  Sto, Hto, S_land, V_sl, S_wet, f;

        Double W_S_Clmax, W_S_ClmaxTO, W_S_ClmaxL,WpTO, Wl_Wto, Wto,Cd0;

        private void cmb_cf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetaAndb();
                CalculateWetArea();
            }
            catch
            {
                return;
            }
        }

        Double rho0 = AltitudeCalculator.Dens_Standard* 0.0019412449;

        private void LoadAircraftType()
        {
            cmbAircft_Type.Items.Add("Transport Jet");
            cmbAircft_Type.Items.Add("Business Jet");
            cmbAircft_Type.Items.Add("GA - Single Engine");
            cmbAircft_Type.Items.Add("GA - Twin Engine");
            cmbAircft_Type.Items.Add("Home-built");
        }

        private void txt_SwEnter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WingAreaListLoad();
            }
            catch
            {
                return;
            }
        }

        private void txt_Cl_max_TO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClmaxListLoad();
            }
            catch
            {
                return;
            }
        }

        private void txt_Slanding_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateW_PL();
            }
            catch
            {
                return;
            }
        }

        Double[,] W_P_TO;

        private void WingAreaListLoad()
        {
            Double Sw = Double.Parse(txt_WsEnter.Text);
            W_S_list2 = new Double[5];
            for (int i = 0; i < 5; i++)
            {
                W_S_list2[i] = Sw;
                Sw = Sw + 10;
            }

            dataGridView1.Columns["Column" + 1].HeaderText = W_S_list2[0].ToString();
            dataGridView1.Columns["Column2"].HeaderText = W_S_list2[1].ToString();
            dataGridView1.Columns["Column3"].HeaderText = W_S_list2[2].ToString();
            dataGridView1.Columns["Column4"].HeaderText = W_S_list2[3].ToString();            
            dataGridView1.Columns["Column5"].HeaderText = W_S_list2[4].ToString();
            /*
            dataGridView1.Columns["Column6"].HeaderText = W_S_list2[5].ToString();
            dataGridView1.Columns["Column7"].HeaderText = W_S_list2[6].ToString();
            dataGridView1.Columns["Column8"].HeaderText = W_S_list2[7].ToString();

            label23.Text = W_S_list2[0].ToString();
            label24.Text = W_S_list2[1].ToString();
            label25.Text = W_S_list2[2].ToString();
            label26.Text = W_S_list2[3].ToString();
            label27.Text = W_S_list2[4].ToString();
            label28.Text = W_S_list2[5].ToString();
            label29.Text = W_S_list2[6].ToString();
            label30.Text = W_S_list2[7].ToString();
            */
        }

        private void ClmaxListLoad()
        {
            Double Clmx = Double.Parse(txt_Cl_max_TO.Text);
            ClmaxTO_list = new Double[5];
            for (int i = 0; i < 5; i++)
            {
                ClmaxTO_list[i] = Clmx;
                Clmx = Clmx + 0.3;
            }

            /*
            label15.Text = CL_max[0].ToString();
            label16.Text = CL_max[1].ToString();
            label17.Text = CL_max[2].ToString();
            label18.Text = CL_max[3].ToString();
            label19.Text = CL_max[4].ToString();
            label20.Text = CL_max[5].ToString();
            label21.Text = CL_max[6].ToString();
            label22.Text = CL_max[7].ToString();
            */
        }

        private void txt_Sto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateW_P_TO();
            }
            catch
            {
                return;
            }
        }

        private void txt_Cl_max_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateW_S_Clmax();
            }
            catch
            {
                return;
            }
        }

        public WingDetailEst()
        {
            InitializeComponent();
        }

        private void LoadSkinFriction()
        {
            Double num = 0.0090;            
            for (int i = 0; i < 8; i++)
            {
                cmb_cf.Items.Add(num);
                num = num - 0.0010;
            }
        }

        private void GetaAndb()
        {
            b = 1;
            if (Double.Parse(cmb_cf.Text) == 0.0090)
            {
                a = -2.0458;                
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0080)
            {
                a = -2.0969;                
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0070)
            {
                a = -2.1549;
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0060)
            {
                a = -2.2218;
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0050)
            {
                a = -2.3010;
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0040)
            {
                a = -2.3979;
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0030)
            {
                a = -2.5229;
            }
            else if (Double.Parse(cmb_cf.Text) == 0.0020)
            {
                a = -2.6990;
            }
        }

        private void WingDetailEst_Load(object sender, EventArgs e)
        {
            LoadCombos.LoadDistanceCombos(cmb_Hto, "ft");
            LoadCombos.LoadDistanceCombos(cmb_H_landing, "ft");

            cmb_Top.Items.Add("Top23");
            cmb_Top.Items.Add("Top25");

            LoadSkinFriction();
            LoadAircraftType();
        }

        private void CalculateW_S_Clmax()
        {
            W_S_list = new List<double>();
            Vs_flp_up = Double.Parse(txt_Vs_flp_up.Text) * 1.688;
            Vs_flp_down = Double.Parse(txt_Vs_flp_down.Text) * 1.688;            

            Cl_max = Double.Parse(txt_Cl_max.Text);
            //Clmax_TO = Double.Parse(txt_Cl_max_TO.Text);
            Clmax_L = Double.Parse(txt_Clmax_L.Text);

            

            W_S_Clmax = Math.Round(Math.Pow(Vs_flp_up, 2)*0.5*rho0*Cl_max,1);
            W_S_ClmaxL = Math.Round(Math.Pow(Vs_flp_down, 2) * 0.5 * rho0 * Clmax_L,1);

            W_S_list.Add(W_S_Clmax);
            W_S_list.Add(W_S_ClmaxL);

            //label5.Text = W_S_Clmax.ToString();

            listBox1.DataSource = W_S_list;
        }

        private void CalculateW_P_TO()
        {
            dataGridView1.Rows.Clear();
            W_P_TO = new double[5,5];                        

            List<Double> xlist = new List<double>();
            Sto = Double.Parse(txt_Sto.Text);
            Hto = Double.Parse(txt_H_to.Text);
            AltitudeCalculator.CalcAtmos(txt_H_to.Text, cmb_Hto);

            Double rhorel = AltitudeCalculator.rho_h * 0.0019412449 / rho0;

            if (cmb_Top.Text == "Top23")
            {
                Double a1 = 0.009;
                Double b1 = 4.9;
                Double c1 = -Sto;

                Double inner = Math.Sqrt(Math.Pow(b1, 2) - 4 * a1 * c1);
                Double x1 = Math.Round((-b1 - inner) / (2 * a1), 1);
                Double Top23 = Math.Round((-b1 + inner) / (2 * a1), 1);

                Double ans = Top23 * AltitudeCalculator.rel_rho_h;                

                label5.Text = ans.ToString();

                for (int i = 0; i < W_S_list2.Length; i++)
                {
                    for (int j = 0; j < ClmaxTO_list.Length; j++)
                    {
                        WpTO = Top23 * rhorel * ClmaxTO_list[j] / W_S_list2[i];
                        W_P_TO[i, j] = Math.Round(WpTO, 1);
                    }
                }

                label5.Text = Top23.ToString();
            }
            else
            {
                Double Top25 = Sto / 37.5;

                for (int i = 0; i < W_S_list2.Length; i++)
                {
                    for (int j = 0; j < ClmaxTO_list.Length; j++)
                    {
                        WpTO = W_S_list2[i] / (rhorel * ClmaxTO_list[j] * Top25);
                        W_P_TO[i, j] = Math.Round(WpTO, 2);
                    }
                }
            }

            //label5.Text = W_P_TO[1, 2].ToString();

            for (int i = 0; i < W_P_TO.GetLength(0); i++)
            {
                string[] row = new string[W_P_TO.GetLength(1)];

                for (int j = 0; j < W_P_TO.GetLength(1); j++)
                {
                    row[j] = W_P_TO[j,i].ToString();
                }

                dataGridView1.Rows.Add(row);
            }

            
        }

        private void CalculateW_PL()
        {            
            S_land = Double.Parse(txt_Slanding.Text);            

            W_S_TO_L_list = new Double[4];

            Clmax_L = Double.Parse(txt_Clmax_L.Text);

            AltitudeCalculator.CalcAtmos(txt_H_landing.Text, cmb_H_landing);

            Wl_Wto = Double.Parse(txt_Wl_Wto.Text);

            if (cmb_Top.Text == "Top23")
            {
                V_sl = Math.Round(Math.Sqrt(S_land / 0.265) * 1.688, 1);
            }
            else
            {
                Double Va = Math.Sqrt(S_land / 0.3);
                V_sl = Va * 1.688 / 1.3;
            }

            for (int i = 0; i < 4; i++)
            {
                W_S_TO_L_list[i] = Math.Round(Math.Pow(V_sl,2)*Clmax_L*AltitudeCalculator.rho_h* 0.0019412449*0.5/Wl_Wto,1);
                Clmax_L = Clmax_L + 0.3; 
            }

            

            listBox1.DataSource = W_S_TO_L_list;

            label5.Text = V_sl.ToString();

        }

        List<Double> Cl_to_list;
        List<Double> L_D_list;
        List<Double> CGRP_list;
        List<Double> W_S_to_list;
        Double[] L_D_invList;

        Double CGR = 0.08333;

        private void CalculateWetArea()
        {
            if (cmbAircft_Type.Text == "GA - Single Engine")
            {
                c = 1.2362;
                d = 0.5147;
            }
            else if (cmbAircft_Type.Text == "GA - Twin Engine")
            {
                c = 0.8635;
                d = 0.5632;
            }
            else if (cmbAircft_Type.Text == "Business Jet")
            {
                c = 0.2263;
                d = 0.6977;

            }
            else if (cmbAircft_Type.Text == "Transport Jet")
            {
                c = 0.0199;
                d = 0.7531;
            }
            else if (cmbAircft_Type.Text == "Home-built")
            {
                c = 1.2362;
                d = 0.4319;
            }
            else if (cmbAircft_Type.Text == "Regional Turboprops")
            {
                c = -0.0866;
                d = 0.8099;
            }

            //var obj = new Aircraft_Weight_Est();

            //obj.TakeoffWeightCal();

            //Wto = obj.Wto;

            Double Wto = 7900;
            

            S_wet = Math.Pow(10, (c + d * Math.Log10(Wto)));

            f = Math.Pow(10, (a + b * Math.Log10(S_wet)));

            Double W_s_avg = 30;

            Double S_w_avg = Math.Round(Wto / W_s_avg,0);

            //Cd0 = f / S_w_avg;

            Cd0 = 0.0266;

            AR = 8;

            e = 0.8;

            K = 1 / (Math.PI * AR * e);

            ClmaxTO_list = new Double[3];
            Cl_to_list = new List<double>();
            L_D_list = new List<double>();
            CGRP_list = new List<double>();
            W_S_to_list = new List<double>();

            Double Cd0_TO_flaps = 0.0134;

            Double np = 0.8;

            Double W_S_Takoff = 20;

            Clmax_TO = Double.Parse(txt_Cl_max_TO.Text);

            for (int i = 0; i < ClmaxTO_list.Length; i++)
            {
                Double num1 = Clmax_TO - 0.2;
                Cl_to_list.Add(num1);

                Double L_Davg = num1 / ((Cd0 + Cd0_TO_flaps) + (Math.Pow(num1, 2) * K));
                L_D_list.Add(L_Davg);
                L_D_list.Add(1 / L_Davg);
                Double CGRP = (CGR + (1 / L_Davg)) / Math.Pow(num1, 2);
                CGRP_list.Add(CGRP);
                W_S_to_list.Add(18.97 * np / (CGRP * Math.Sqrt(W_S_Takoff)));

                Clmax_TO = Clmax_TO + 0.3;

            }

            /*
            Double Cl_rc_max = Math.Sqrt(3*Cd0*Math.PI*AR*e);

            Double Cd_rc_max = 4 * Cd0;
            
            Double Cl32_Cdmax = 1.345 * Math.Pow(AR * e, 0.75) / Math.Pow(Cd0, 0.25);

            Double RCP = Math.Sqrt(W_s_avg)/(19*Cl32_Cdmax);
            */


            //label5.Text = Cl32_Cdmax.ToString();
            listBox1.DataSource = W_S_to_list;

        }

        
    }
}
