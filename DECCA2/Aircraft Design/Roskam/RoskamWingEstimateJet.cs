using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    public partial class RoskamWingEstimateJet : Form
    {
        Double a, b, c, d, e, K, AR, ClmaxA, Sw, Hcr, Vcr_cl, RC, H_abs, e_clean, Temp_cr,P_h,a_h,Vcr,q_cr;

        Double Hto, rhoh, Swet, f, C_d0, T_W_to, L_D, Sto, TWTO, Wl_Wto, S_land, Clmax_L, V_sl,T_W_cl;

        Double Pressrel, Press_Ratio , Temp_Ratio;        

        Double rho0 = AltitudeCalculator.Dens_Standard * 0.0019412449;

        Double P_0 = AltitudeCalculator.Pres_Standard;

        Double Temp0 = AltitudeCalculator.Temp_Standard;       

        Double W_to;

        Double Wlanding = 115000;

        //========================================================================

        Double[] W_S_list;
        Double[] ClmaxTO_list;
        Double[] ClmaxLD_list;
        Double[] Clmax_list;
        Double[] T_W_TO_L_list;
        Double[] T_W_TO_Cr_list;
        Double[,] T_W_TO;

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //========================================================================

        public RoskamWingEstimateJet(string AircraftType, Double Wto)
        {
            InitializeComponent();
            cmbAircft_Type.Text = AircraftType;
            W_to = Wto;
            //Vcr = V_cr;
            //txt_Vcr.Text = V_cr.ToString();
        }

        private void WingEstimateJet_Load(object sender, EventArgs e)
        {
            LoadAircraftType();
            LoadSkinFriction();

            LoadCombos.LoadDistanceCombos(cmb_Hto, "ft");
            LoadCombos.LoadDistanceCombos(cmb_H_landing, "ft");
            LoadCombos.LoadDistanceCombos(cmb_Hcr, "ft");
            //LoadCombos.LoadDistanceCombos(cmb_Hcl, "ft");

            LoadCombos.LoadTempCombos(cmb_Temp, "F");

            LoadCombos.LoadVelCombos(cmb_Vcr, "mach");

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Wing Loading W/S in lbf/ft2",
                Separator = new Separator
                {
                    Step = 5,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Take-off Thrust-T0-Weight Ratio ~ (T/W))",
                Separator = new Separator
                {
                    //Step = 25,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });
        }

        private void LoadAircraftType()
        {
            cmbAircft_Type.Items.Add("Business Jet");
            cmbAircft_Type.Items.Add("Transport Jet");
            cmbAircft_Type.Items.Add("Supersonic Cruise Airplanes");            
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //========================================================================

        private void WingAreaListLoad()
        {
            Double Sw = Double.Parse(txt_WsEnter.Text);
            W_S_list = new Double[5];
            for (int i = 0; i < 5; i++)
            {
                W_S_list[i] = Sw;
                Sw = Sw + 20;
            }


            dataGridView1.Columns["Column" + 1].HeaderText = W_S_list[0].ToString();
            dataGridView1.Columns["Column2"].HeaderText = W_S_list[1].ToString();
            dataGridView1.Columns["Column3"].HeaderText = W_S_list[2].ToString();
            dataGridView1.Columns["Column4"].HeaderText = W_S_list[3].ToString();
            dataGridView1.Columns["Column5"].HeaderText = W_S_list[4].ToString();

            /*  
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

        private void ClmaxTOListLoad()
        {
            Double Clmx = Double.Parse(txt_Cl_max_TO.Text);
            ClmaxTO_list = new Double[5];
            for (int i = 0; i < 5; i++)
            {
                ClmaxTO_list[i] = Clmx;
                Clmx = Clmx + 0.4;
            }
        }

        private void ClmaxListLoad()
        {
            Double Clmx = Double.Parse(txt_Cl_max.Text);
            Clmax_list = new Double[4];
            for (int i = 0; i < 4; i++)
            {
                Clmax_list[i] = Clmx;
                Clmx = Clmx + 0.3;
            }
        }

        private void ClmaxLDListLoad()
        {
            Double Clmx = Double.Parse(txt_Clmax_L.Text);
            ClmaxLD_list = new Double[4];
            for (int i = 0; i < 4; i++)
            {
                ClmaxLD_list[i] = Clmx;
                Clmx = Clmx + 0.3;
            }
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

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //========================================================================

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

        private void CalculateWetArea()
        {
            try
            {
                if (cmbAircft_Type.Text == "Transport Jet")
                {
                    c = 0.0199;
                    d = 0.7531;
                }
                else if (cmbAircft_Type.Text == "Business Jet")
                {
                    c = 0.2263;
                    d = 0.6977;
                }
                else if (cmbAircft_Type.Text == "Supersonic Cruise Airplanes")
                {
                    c = -1.1868;
                    d = 0.9609;
                }               

                Swet = Math.Round(Math.Pow(10, (c + d * Math.Log10(W_to))));

                GetaAndb();

                f = Math.Round(Math.Pow(10, (a + b * Math.Log10(Swet))),1);                
            }
            catch
            {
                return;
            }
        }

        private void GetTempRatio()
        {
            Double Temp_input = Double.Parse(txt_TO_temp.Text);

            if (cmb_Temp.Text == "F")
            {
                Temp_cr = Temp_input;
            }
            else if (cmb_Temp.Text == "C")
            {
                Temp_cr = Temp_input * 1.8 + 32;
            }
            else if (cmb_Temp.Text == "K")
            {
                Temp_cr = (Temp_input - 273.15) * 1.8 + 32;
            }

            Temp_cr = Temp_cr + 459.67;

            Temp_Ratio = Math.Round(Temp_cr / ((Temp0 - 273.15) * 1.8 + 32 + 459.67), 4);

            Press_Ratio = Math.Round(Pressrel / Temp_Ratio, 4);
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void txt_WsEnter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WingAreaListLoad();
                CalculateTO_T_W();

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
                ClmaxTOListLoad();
                CalculateTO_T_W();
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
                CalculateLD_T_W();
            }
            catch
            {
                return;
            }
        }

        private void txt_H_to_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateTO_T_W();
            }
            catch
            {
                return;
            }
        }

        private void txt_Hcr_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                CalculateCL_T_W();
            }
            catch
            {
                return;
            }
        }

        private void cmbAircft_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateCd0();
                LoadCarpetPlot();
            }
            catch
            {
                return;
            }
        }

        //========================================================================

        private void CalculateCd0()
        {
            try
            {               
                CalculateWetArea();
                Double sum = 0;
                for (int i = 0; i < W_S_list.Length; i++)
                {
                    sum = sum + W_S_list[i];
                }
                Double avg = sum / W_S_list.Length;
                C_d0 = Math.Round(f * avg / W_to, 4);                

                AR = Double.Parse(txt_AR.Text);
                e = Double.Parse(txt_e.Text);
                ClmaxA = Double.Parse(txt_Clmax_A.Text);

                K = Math.Round(AR * e * Math.PI, 1);

                Double Cd_0_TO_flaps = Double.Parse(txt_TOflaps.Text);
                Double Cd_0_L_flaps = Double.Parse(txt_Lflaps.Text);
                Double Cd_0_L_gears = Double.Parse(txt_Lgears.Text);
                Double Cd_0_Stop_prop = Double.Parse(txt_Stop_prop.Text);

                Double Cd_0_Appr = Cd_0_L_gears + C_d0 + 0.5 * (Cd_0_TO_flaps + Cd_0_L_flaps);

                Double Cl_A = ClmaxA / Math.Pow(1.5, 2);

                Double CD = Cd_0_Appr + Math.Pow(Cl_A, 2)/K;

                L_D = Math.Round(Cl_A / CD,1);

                Double T_W_L = 2 * (0.021 + 1 / L_D);

                T_W_to = Math.Round(T_W_L * (Wlanding / W_to) / 0.8,2);

                Sw = f / C_d0;

                label1.Text = W_to.ToString();               
            }
            catch
            {
                return;
            }
        }

        private void CalculateTO_T_W()
        {
            dataGridView1.Rows.Clear();

            T_W_TO = new double[5, 5];

            GetTempRatio();

            Sto = Double.Parse(txt_H_to.Text);            

            AltitudeCalculator.CalcAtmos(txt_H_to.Text, cmb_Hto);

            P_h = AltitudeCalculator.P_h;

            Pressrel = P_h / P_0;            

            Double abc = Math.Round(37.5 / (Press_Ratio * Sto),5);            

            for (int i = 0; i < W_S_list.Length; i++)
            {
                for (int j = 0; j < ClmaxTO_list.Length; j++)
                {
                    TWTO = abc * 1.17 * W_S_list[i] / ClmaxTO_list[j];
                    T_W_TO[i, j] = Math.Round(TWTO, 2);
                }
            }            

            for (int i = 0; i < T_W_TO.GetLength(0); i++)
            {
                string[] row = new string[T_W_TO.GetLength(1)];

                for (int j = 0; j < T_W_TO.GetLength(1); j++)
                {
                    row[j] = T_W_TO[j, i].ToString();
                }

                dataGridView1.Rows.Add(row);
            }

            label1.Text = ClmaxTO_list[0].ToString();
        }

        private void CalculateLD_T_W()
        {
            T_W_TO_L_list = new Double[5];

            Clmax_L = Double.Parse(txt_Clmax_L.Text);

            S_land = Double.Parse(txt_Slanding.Text);

            V_sl = Math.Round(Math.Sqrt(S_land / (0.3 * 1.69)),1);

            AltitudeCalculator.CalcAtmos(txt_H_landing.Text, cmb_H_landing);

            Wl_Wto = Double.Parse(txt_Wl_Wto.Text);

            rhoh = AltitudeCalculator.rho_h;

            GetTempRatio();

            Double abc = Math.Round(Math.Pow(V_sl*1.688,2)*0.5*rhoh * 0.0019412449 / Temp_Ratio,1);

            for (int i = 0; i < 4; i++)
            {
                T_W_TO_L_list[i] = Math.Round(abc*Clmax_L/ Wl_Wto, 1);
                Clmax_L = Clmax_L + 0.4;
            }

            listBox1.DataSource = T_W_TO_L_list;
            label1.Text = abc.ToString();

            LoadCarpetPlot();
        }

        private void CalculateCR_T_W()
        {
            AltitudeCalculator.CalcAtmos(txt_Hcr.Text, cmb_Hcr);

            T_W_TO_Cr_list = new Double[5];

            a_h = AltitudeCalculator.a_h;

            Vcr = Double.Parse(txt_Vcr.Text)*a_h/0.3048;

            q_cr = Math.Round(Math.Pow(Vcr,2) * 0.5 * AltitudeCalculator.rho_h * 0.0019412449,1);

            CalculateCd0();

            Double ab = Math.Round((C_d0 + 0.0005) * q_cr,2);

            Double cd;

            e_clean = Double.Parse(txt_eClean.Text);

            if(Vcr != 0)
            {
                cd = Math.Round(Math.PI * AR * e_clean * q_cr, 0);
            }
            else
            {
                cd = 1;
            }            

            for (int i = 0; i < W_S_list.Length; i++)
            {
                T_W_TO_Cr_list[i] = Math.Round(((ab / W_S_list[i]) + (W_S_list[i]/cd))/0.23 , 2);
            }
            
        }

        private void CalculateCL_T_W()
        {
            RC = 8.33;

            CalculateCd0();
            CalculateCR_T_W();

            AltitudeCalculator.CalcAtmos(txt_Hcr.Text, cmb_Hcr);

            Vcr_cl = Math.Round(Double.Parse(txt_Vcr.Text) * AltitudeCalculator.a_h /0.3048,0);

            Double Cl_cl = Math.Round(W_to / (q_cr * Sw),2);

            Double CD_cl = Math.Round(C_d0 + Math.Pow(Cl_cl, 2) / (AR * Math.PI * 0.8),4);

            Double LD_cl = Math.Round(Cl_cl / CD_cl,1);

            T_W_cl = Math.Round(((RC / Vcr_cl) + (1 / LD_cl)) / 0.23, 2);

            LoadCarpetPlot();

            label1.Text = T_W_cl.ToString();
        }

        private void LoadCarpetPlot()
        {
            try
            {
                cartesianChart1.Series = new SeriesCollection
            {
                    
                new LineSeries
                {
                    Title = "T_W_cl",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0],T_W_cl),
                        new ObservablePoint(W_S_list[4],T_W_cl)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "T_W_climb",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0],T_W_to),
                        new ObservablePoint(W_S_list[4],T_W_to)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Cl_max " + Double.Parse(txt_Clmax_L.Text).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(T_W_TO_L_list[0],0),
                        new ObservablePoint(T_W_TO_L_list[0],0.8)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Cl_max " + (Double.Parse(txt_Clmax_L.Text) + 0.4).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(T_W_TO_L_list[1],0),
                        new ObservablePoint(T_W_TO_L_list[1],0.8)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Cl_max " + (Double.Parse(txt_Clmax_L.Text)+ 0.8).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(T_W_TO_L_list[2],0),
                        new ObservablePoint(T_W_TO_L_list[2],0.8)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Cl_max " + (Double.Parse(txt_Clmax_L.Text) + 1.2).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(T_W_TO_L_list[3],0),
                        new ObservablePoint(T_W_TO_L_list[3],0.8)
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "T_W_TO_Cr",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO_Cr_list[0]),
                        new ObservablePoint(W_S_list[1], T_W_TO_Cr_list[1]),
                        new ObservablePoint(W_S_list[2], T_W_TO_Cr_list[2]),
                        new ObservablePoint(W_S_list[3], T_W_TO_Cr_list[3]),
                        new ObservablePoint(W_S_list[4], T_W_TO_Cr_list[4])
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Clmax " + ClmaxTO_list[0].ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO[0,0]),
                        new ObservablePoint(W_S_list[1], T_W_TO[1,0]),
                        new ObservablePoint(W_S_list[2], T_W_TO[2,0]),
                        new ObservablePoint(W_S_list[3], T_W_TO[3,0]),
                        new ObservablePoint(W_S_list[4], T_W_TO[4,0])
                    },
                    LineSmoothness = 0
                }
                ,
                new LineSeries
                {
                    Title = "Clmax " + ClmaxTO_list[1].ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO[0,1]),
                        new ObservablePoint(W_S_list[1], T_W_TO[1,1]),
                        new ObservablePoint(W_S_list[2], T_W_TO[2,1]),
                        new ObservablePoint(W_S_list[3], T_W_TO[3,1]),
                        new ObservablePoint(W_S_list[4], T_W_TO[4,1])
                    },
                    LineSmoothness = 0
                }
                ,
                new LineSeries
                {
                    Title = "Clmax "  + ClmaxTO_list[2].ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO[0,2]),
                        new ObservablePoint(W_S_list[1], T_W_TO[1,2]),
                        new ObservablePoint(W_S_list[2], T_W_TO[2,2]),
                        new ObservablePoint(W_S_list[3], T_W_TO[3,2]),
                        new ObservablePoint(W_S_list[4], T_W_TO[4,2])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Clmax " + ClmaxTO_list[3].ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO[0,3]),
                        new ObservablePoint(W_S_list[1], T_W_TO[1,3]),
                        new ObservablePoint(W_S_list[2], T_W_TO[2,3]),
                        new ObservablePoint(W_S_list[3], T_W_TO[3,3]),
                        new ObservablePoint(W_S_list[4], T_W_TO[4,3])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Clmax " + ClmaxTO_list[4].ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], T_W_TO[0,4]),
                        new ObservablePoint(W_S_list[1], T_W_TO[1,4]),
                        new ObservablePoint(W_S_list[2], T_W_TO[2,4]),
                        new ObservablePoint(W_S_list[3], T_W_TO[3,4]),
                        new ObservablePoint(W_S_list[4], T_W_TO[4,4])
                    },
                    LineSmoothness = 0
                }

            };
            }
            catch
            {
                return;
            }
        }
    }
}
