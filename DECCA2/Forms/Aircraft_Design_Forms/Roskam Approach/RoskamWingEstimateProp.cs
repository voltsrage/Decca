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
    public partial class WingEstimateForm_form : Form
    {
        Double a, b, c, d, e, AR, np,Ip,Hcr,Rc0,RC,H_abs,Tcl;

        Double Hto, rhoh, Swet, f, C_d0, CL32_CD, L_D, Sto, WpTO,Wl_Wto,S_land, Clmax_L, V_sl;

        Double rho0 = AltitudeCalculator.Dens_Standard * 0.0019412449;

        Double W_to;

        Double Clmax = 1.7;

        Double ClmaxTO = 1.8;

        Double Cl_climb;

        Double RCP;

        Double FAR_2365 = Math.Round(300 * Math.Pow(33000, -1), 4);

        Double FAR_2367;

        RoskamWeightEst Aircraft = new RoskamWeightEst();

        //========================================================================

        Double[] W_S_list;
        Double[] ClmaxTO_list;
        Double[] ClmaxLD_list;
        Double[] Clmax_list;
        Double[] W_S_TO_L_list;
        Double[,] W_P_TO;       

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<Double> Vso_fps, Vso_knots,W_P_cr, W_P_cl, W_P_TO_2365_RC,
            RCP_list, RC_list, W_P_TO_2365_CGR, W_P_TO_2367, W_P_TO_2377;        

        //========================================================================

        public WingEstimateForm_form(string AircraftType,Double Wto)
        {
            InitializeComponent();
            cmbAircft_Type.Text = AircraftType;
            W_to = Wto;
        }

        private void WingEstimateProp_Load(object sender, EventArgs e)
        {
            LoadAircraftType();
            LoadSkinFriction();

            LoadCombos.LoadDistanceCombos(cmb_Hto, "ft");
            LoadCombos.LoadDistanceCombos(cmb_H_landing, "ft");
            LoadCombos.LoadDistanceCombos(cmb_Hcr, "ft");
            LoadCombos.LoadDistanceCombos(cmb_Hcl, "ft");

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
                Title = "Take-off Power Loading ~ (W/P) LBS/HP",
                Separator = new Separator
                {
                    //Step = 25,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });
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
                Sw = Sw + 10;
            }            

            /*  
           WingEstimateProp.Columns["Column" + 1].HeaderText = W_S_list[0].ToString();
           WingEstimateProp.Columns["Column2"].HeaderText = W_S_list[1].ToString();
           WingEstimateProp.Columns["Column3"].HeaderText = W_S_list[2].ToString();
           WingEstimateProp.Columns["Column4"].HeaderText = W_S_list[3].ToString();
           //dataGridView1.Columns["Column5"].HeaderText = W_S_list[4].ToString();


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
                Clmx = Clmx + 0.3;
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

        private void LoadAircraftType()
        {
            cmbAircft_Type.Items.Add("GA - Single Engine");
            cmbAircft_Type.Items.Add("GA - Twin Engine");
            cmbAircft_Type.Items.Add("Home-built");
        }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //=================================================================

        private void txt_AR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateCd0();
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
                ClmaxListLoad();
            }
            catch
            {
                return;
            }
        }

        private void txt_Clmax_L_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClmaxLDListLoad();
                CalculateW_PL();
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
                Calculate_RCP();
                CalculateW_P_TO();
            }
            catch
            {
                return;
            }
        }

        private void txt_H_landing_TextChanged(object sender, EventArgs e)
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

        private void txt_WsEnter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WingAreaListLoad();
                Calculate_RCP();
                CalculateW_P_TO();
            }
            catch
            {
                return;
            }
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

        private void txt_Tcl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateRc0_Rc();
            }
            catch
            {
                return;
            }
        }

        private void txt_PwIndex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateW_P_cr();
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
                CalculateWetArea();
            }
            catch
            {
                return;
            }
        }

        private void cmb_cf_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateWetArea();
                CalculateCd0();
            }
            catch
            {
                return;
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //================================================================

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

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        private void Calculate_RCP()
        {
            Vso_fps = new List<double>();
            Vso_knots = new List<double>();
            RCP_list = new List<double>();
            RC_list = new List<double>();

            AltitudeCalculator.CalcAtmos(txt_H_to.Text, cmb_Hto);

            rhoh = AltitudeCalculator.rho_h * 0.0019412449;

            for (int i = 0; i < W_S_list.Length; i++)
            {
                Double Vso = Math.Round(Math.Sqrt(2 * W_S_list[i] / (rhoh * Clmax)), 0);
                Vso_fps.Add(Vso);
                Vso_knots.Add(Math.Round(Vso / 1.688, 0));
                FAR_2367 = Math.Round(0.027 * Math.Pow(Vso / 1.688, 2), 0);
                RC_list.Add(FAR_2367);
                RCP_list.Add(Math.Round(FAR_2367 / 33000, 4));
            }

            //listBox1.DataSource = RCP_list;
        }
        
        private void CalculateWetArea()
        {
            try
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

                Swet = Math.Round(Math.Pow(10, (c + d * Math.Log10(W_to))));

                GetaAndb();

                f = Math.Round(Math.Pow(10, (a + b * Math.Log10(Swet))));
                label1.Text = Swet.ToString();
            }
            catch
            {
                return;
            }
        }

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

                label1.Text = "Cd0 is " + C_d0.ToString();

                AR = Double.Parse(txt_AR.Text);
                e = Double.Parse(txt_e.Text);
                np = Double.Parse(txt_np.Text);

                Double Cd_0_TO_flaps = Double.Parse(txt_TOflaps.Text);
                Double Cd_0_L_flaps = Double.Parse(txt_Lflaps.Text);
                Double Cd_0_L_gears = Double.Parse(txt_Lgears.Text);
                Double Cd_0_Stop_prop = Double.Parse(txt_Stop_prop.Text);

                Cl_climb = ClmaxTO - 0.2;

                Double CD = C_d0 + Cd_0_TO_flaps + Math.Pow(Cl_climb, 2) / (Math.PI * AR * e);

                L_D = Math.Round(Cl_climb / CD, 1);

                W_P_TO_2365_RC = new List<double>();
                W_P_TO_2365_CGR = new List<double>();
                W_P_TO_2367 = new List<double>();
                W_P_TO_2377 = new List<double>();

                CL32_CD = Math.Round(1.345 * Math.Pow(AR * e, 0.75) / Math.Pow(C_d0 + Cd_0_TO_flaps, 0.25), 1);

                Double CL32_CD_stop = Math.Round(1.345 * Math.Pow(AR * e, 0.75) / Math.Pow(C_d0 + Cd_0_Stop_prop, 0.25), 1);

                Double CGR = 0.0833;

                Double CGR_2377 = 0.0333;

                Double Clmax_L = 2;

                Double Cl_Land = Clmax_L - 0.2;

                Double CD_Land = C_d0 + Cd_0_L_flaps + Cd_0_L_gears + Math.Pow(Cl_Land, 2) / (Math.PI * AR * e);

                Double L_D_land = Cl_Land / CD_Land;

                Double CGRP = Math.Round((CGR + 1 / L_D) / Math.Sqrt(Cl_climb), 4);

                Double CGRP_2377 = Math.Round((CGR_2377 + 1 / L_D_land) / Math.Sqrt(Cl_Land), 4);

                AltitudeCalculator.CalcAtmos(txt_H_to.Text, cmb_Hto);

                for (int i = 0; i < W_S_list.Length; i++)
                {
                    Double right = FAR_2365 + Math.Sqrt(W_S_list[i]) / (19 * CL32_CD * 1);
                    W_P_TO_2365_RC.Add(Math.Round(np / (right * 1.1), 1));

                    Double right2 = RCP_list[i] + Math.Sqrt(W_S_list[i]) / (19 * CL32_CD_stop * Math.Sqrt(AltitudeCalculator.rel_rho_h));
                    W_P_TO_2367.Add(Math.Round(np / (right2), 1));

                    Double right3 = 18.97 * np / (CGRP * Math.Sqrt(W_S_list[i]));
                    W_P_TO_2365_CGR.Add(Math.Round(right3 * 0.85, 1));

                    Double right4 = 18.97 * np / (CGRP_2377 * Math.Sqrt(W_S_list[i]));
                    W_P_TO_2377.Add(Math.Round(right4, 1));
                }
                LoadCarpetPlot();
                //listBox1.DataSource = W_P_TO_2377;
                //label1.Text = CGRP_2377.ToString();
            }
            catch
            {
                return;
            }
        }

        private void CalculateW_P_TO()
        {
            //dataGridView1.Rows.Clear();
            W_P_TO = new double[5, 5];

            List<Double> xlist = new List<double>();
            Sto = Double.Parse(txt_Sto.Text);
            Hto = Double.Parse(txt_H_to.Text);
            AltitudeCalculator.CalcAtmos(txt_H_to.Text, cmb_Hto);

            Double rhorel = AltitudeCalculator.rho_h * 0.0019412449 / rho0;


            Double a1 = 0.009;
            Double b1 = 4.9;
            Double c1 = -Sto;

            Double inner = Math.Sqrt(Math.Pow(b1, 2) - 4 * a1 * c1);
            Double x1 = Math.Round((-b1 - inner) / (2 * a1), 1);
            Double Top23 = Math.Round((-b1 + inner) / (2 * a1), 1);

            Double ans = Top23 * AltitudeCalculator.rel_rho_h;

            label1.Text = "Top23 is "+ ans.ToString();

            for (int i = 0; i < W_S_list.Length; i++)
            {
                for (int j = 0; j < ClmaxTO_list.Length; j++)
                {
                    WpTO = Top23 * rhorel * ClmaxTO_list[j] / W_S_list[i];
                    W_P_TO[i, j] = Math.Round(WpTO, 1);
                }
            }

            label1.Text = Top23.ToString();

            for (int i = 0; i < W_P_TO.GetLength(0); i++)
            {
                string[] row = new string[W_P_TO.GetLength(1)];

                for (int j = 0; j < W_P_TO.GetLength(1); j++)
                {
                    row[j] = W_P_TO[j,i].ToString();
                }

                //dataGridView1.Rows.Add(row);
            }

            LoadCarpetPlot();

        }

        private void CalculateW_PL()
        {
            S_land = Double.Parse(txt_Slanding.Text);

            W_S_TO_L_list = new Double[4];

            Clmax_L = Double.Parse(txt_Clmax_L.Text);

            AltitudeCalculator.CalcAtmos(txt_H_landing.Text, cmb_H_landing);

            Wl_Wto = Double.Parse(txt_Wl_Wto.Text);

            V_sl = Math.Round(Math.Sqrt(S_land / 0.265) * 1.688, 1);           

            for (int i = 0; i < 4; i++)
            {
                W_S_TO_L_list[i] = Math.Round(Math.Pow(V_sl, 2) * Clmax_L * AltitudeCalculator.rho_h * 0.0019412449 * 0.5 / Wl_Wto, 1);
                Clmax_L = Clmax_L + 0.3;
            }

            LoadCarpetPlot();
        }

        private void CalculateW_P_cr()
        {
            Ip = Double.Parse(txt_PwIndex.Text);
            Hcr = Double.Parse(txt_Hcr.Text);

            W_P_cr = new List<double>();

            AltitudeCalculator.CalcAtmos(txt_Hcr.Text, cmb_Hcr);

            Double rhorel = AltitudeCalculator.rho_h * 0.0019412449 / rho0;

            for (int i = 0; i < W_S_list.Length; i++)
            {
                W_P_cr.Add(Math.Round(W_S_list[i]*0.7/(Math.Pow(Ip, 3) * rhorel), 1));
            }

            //label1.Text = (Math.Pow(Ip, 3) * rhorel).ToString();
            //listBox1.DataSource = W_P_cr;

            LoadCarpetPlot();
        }

        private void CalculateRc0_Rc()
        {
            Tcl = Double.Parse(txt_Tcl.Text);
            H_abs = Double.Parse(txt_Hcl.Text);
            Hcr = Double.Parse(txt_Hcr.Text);

            Rc0 = Math.Round((H_abs / Tcl) * Math.Log(1/(1 - Hcr / H_abs)),0);

            RC = Rc0 * (1 - Hcr / H_abs);

            RCP = Math.Round(Rc0 / 33000,4);

            CalculateCd0();

            CL32_CD = Math.Round(1.345 * Math.Pow(AR * e, 0.75) / Math.Pow(C_d0, 0.25),1);

            Double bot = 19 * CL32_CD;

            W_P_cl = new List<double>();

            for (int i = 0; i < W_S_list.Length; i++)
            {
                W_P_cl.Add(Math.Round(np/(RCP + Math.Sqrt(W_S_list[i])/bot), 1));
            }

            //label1.Text = CL32_CD.ToString();
            //listBox1.DataSource = W_P_cl;
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //===============================================================

        private void LoadCarpetPlot()
        {
            try
            {
                cartesianChart1.Series = new SeriesCollection
            {
                    /*
                new LineSeries
                {
                    Title = "W_P_TO_2365_RC",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO_2365_RC[0]),
                        new ObservablePoint(W_S_list[1], W_P_TO_2365_RC[1]),
                        new ObservablePoint(W_S_list[2], W_P_TO_2365_RC[2]),
                        new ObservablePoint(W_S_list[3], W_P_TO_2365_RC[3]),
                        new ObservablePoint(W_S_list[4], W_P_TO_2365_RC[4])
                    },
                    LineSmoothness = 1
                },*/
                new LineSeries
                {
                    Title = "W_P_TO_2365_CGR",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO_2365_CGR[0]),
                        new ObservablePoint(W_S_list[1], W_P_TO_2365_CGR[1]),
                        new ObservablePoint(W_S_list[2], W_P_TO_2365_CGR[2]),
                        new ObservablePoint(W_S_list[3], W_P_TO_2365_CGR[3]),
                        new ObservablePoint(W_S_list[4], W_P_TO_2365_CGR[4])
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "W_P_TO_2367",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO_2367[0]),
                        new ObservablePoint(W_S_list[1], W_P_TO_2367[1]),
                        new ObservablePoint(W_S_list[2], W_P_TO_2367[2]),
                        new ObservablePoint(W_S_list[3], W_P_TO_2367[3]),
                        new ObservablePoint(W_S_list[4], W_P_TO_2367[4])
                    },
                    LineSmoothness = 1
                },
                /*
                new LineSeries
                {
                    Title = "W_P_TO_2377",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO_2377[0]),
                        new ObservablePoint(W_S_list[1], W_P_TO_2377[1]),
                        new ObservablePoint(W_S_list[2], W_P_TO_2377[2]),
                        new ObservablePoint(W_S_list[3], W_P_TO_2377[3]),
                        new ObservablePoint(W_S_list[4], W_P_TO_2377[4])
                    },
                    LineSmoothness = 1
                },
                */
                new LineSeries
                {
                    Title = "W_P_cr",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_cr[0]),
                        new ObservablePoint(W_S_list[1], W_P_cr[1]),
                        new ObservablePoint(W_S_list[2], W_P_cr[2]),
                        new ObservablePoint(W_S_list[3], W_P_cr[3]),
                        new ObservablePoint(W_S_list[4], W_P_cr[4])
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Cl_max "+txt_Clmax_L.Text,
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_TO_L_list[0], 0),
                        new ObservablePoint(W_S_TO_L_list[0], 35)
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max "+ (Double.Parse(txt_Clmax_L.Text) + 0.3).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_TO_L_list[1], 0),
                        new ObservablePoint(W_S_TO_L_list[1], 35)
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max "+ (Double.Parse(txt_Clmax_L.Text) + 0.6).ToString(),
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_TO_L_list[2], 0),
                        new ObservablePoint(W_S_TO_L_list[2], 35)
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max "+ (Double.Parse(txt_Clmax_L.Text) + 0.9).ToString() ,
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_TO_L_list[3], 0),
                        new ObservablePoint(W_S_TO_L_list[3], 35)
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Clmax",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO[0,0]),
                        new ObservablePoint(W_S_list[1], W_P_TO[1,0]),
                        new ObservablePoint(W_S_list[2], W_P_TO[2,0]),
                        new ObservablePoint(W_S_list[3], W_P_TO[3,0]),
                        new ObservablePoint(W_S_list[4], W_P_TO[4,0])
                    },
                    LineSmoothness = 1
                }
                ,
                new LineSeries
                {
                    Title = "Clmax",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO[0,1]),
                        new ObservablePoint(W_S_list[1], W_P_TO[1,1]),
                        new ObservablePoint(W_S_list[2], W_P_TO[2,1]),
                        new ObservablePoint(W_S_list[3], W_P_TO[3,1]),
                        new ObservablePoint(W_S_list[4], W_P_TO[4,1])
                    },
                    LineSmoothness = 1
                }
                ,
                new LineSeries
                {
                    Title = "Clmax",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO[0,2]),
                        new ObservablePoint(W_S_list[1], W_P_TO[1,2]),
                        new ObservablePoint(W_S_list[2], W_P_TO[2,2]),
                        new ObservablePoint(W_S_list[3], W_P_TO[3,2]),
                        new ObservablePoint(W_S_list[4], W_P_TO[4,2])
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Clmax",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO[0,3]),
                        new ObservablePoint(W_S_list[1], W_P_TO[1,3]),
                        new ObservablePoint(W_S_list[2], W_P_TO[2,3]),
                        new ObservablePoint(W_S_list[3], W_P_TO[3,3]),
                        new ObservablePoint(W_S_list[4], W_P_TO[4,3])
                    },
                    LineSmoothness = 1
                },
                new LineSeries
                {
                    Title = "Clmax",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_list[0], W_P_TO[0,4]),
                        new ObservablePoint(W_S_list[1], W_P_TO[1,4]),
                        new ObservablePoint(W_S_list[2], W_P_TO[2,4]),
                        new ObservablePoint(W_S_list[3], W_P_TO[3,4]),
                        new ObservablePoint(W_S_list[4], W_P_TO[4,4])
                    },
                    LineSmoothness = 1
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
