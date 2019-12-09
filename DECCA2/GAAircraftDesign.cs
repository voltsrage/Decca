using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace DECCA2
{
    public partial class GAAircraftDesign : Form
    {   

        //                                   GLOBAL VALUES AND CONSTANTS
        //=======================================================================================================
        AltitudeCalculator altitude = new AltitudeCalculator();

        private List<double> Pbhp_TO_set, Pbhp_Climb_set, Pbhp_ceiling_set, Pbhp_cr_set,Pbhp_airspeed_set;

        public Double e, AR, K, Vcr, T_W_cr, CDmin, n, ROC, Vclimb, Vv, Wto_guess,np;

        Double T_W_TO,q_cr,Vlof,T_W_Climb,T_W, T_W_ceiling;

        Double Pbhp_TO,Pbhp_cr,Pbhp_Climb,Pbhp_ceiling,Pbhp,rho;

        Double rho_sl = AltitudeCalculator.Dens_Standard * 0.00194122499;

        public static Double PropEffi;        

        Double[] W_surf;

        Double[] CL_max;

        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private void WingAreaListLoad()
        {
            Double Sw = Double.Parse(txt_SwEnter.Text);
            W_surf = new Double[8];
            for (int i = 0; i < 8; i++)
            {
                W_surf[i] = Sw;                
                Sw = Sw + 10;                
            }
            
            dataGridView1.Columns["Column" + 1].HeaderText = W_surf[0].ToString();
            dataGridView1.Columns["Column2"].HeaderText = W_surf[1].ToString();
            dataGridView1.Columns["Column3"].HeaderText = W_surf[2].ToString();
            dataGridView1.Columns["Column4"].HeaderText = W_surf[3].ToString();
            dataGridView1.Columns["Column5"].HeaderText = W_surf[4].ToString();
            dataGridView1.Columns["Column6"].HeaderText = W_surf[5].ToString();
            dataGridView1.Columns["Column7"].HeaderText = W_surf[6].ToString();
            dataGridView1.Columns["Column8"].HeaderText = W_surf[7].ToString();

            label23.Text = W_surf[0].ToString();
            label24.Text = W_surf[1].ToString();
            label25.Text = W_surf[2].ToString();
            label26.Text = W_surf[3].ToString();
            label27.Text = W_surf[4].ToString();
            label28.Text = W_surf[5].ToString();
            label29.Text = W_surf[6].ToString();
            label30.Text = W_surf[7].ToString();
        }

        private void ClmaxListLoad()
        {
            Double Clmx = Double.Parse(txt_ClmaxEntry.Text);
            CL_max = new Double[8];
            for (int i = 0; i < 8; i++)
            {
               CL_max[i] = Clmx;
                Clmx = Clmx + 0.1;
            }

            label15.Text = CL_max[0].ToString();
            label16.Text = CL_max[1].ToString();
            label17.Text = CL_max[2].ToString();
            label18.Text = CL_max[3].ToString();
            label19.Text = CL_max[4].ToString();
            label20.Text = CL_max[5].ToString();
            label21.Text = CL_max[6].ToString();
            label22.Text = CL_max[7].ToString();
        }

        //                                      INITIALIZE AND FORM LOAD
        //==========================================================================================================

        public GAAircraftDesign()
        {
            InitializeComponent();
        }

        private void RoskamDesign_Load(object sender, EventArgs e)
        {
            Clear();
            LoadCombos.LoadDistanceCombos(cmbHeight_cr, "ft");
            LoadCombos.LoadDistanceCombos(cmbHeight_cl, "ft");
            LoadCombos.LoadDistanceCombos(cmb_Sg, "ft");

            LoadCombos.LoadVelCombos(cmb_Vclimb, "KCAS");
            LoadCombos.LoadVelCombos(cmb_Vlof, "KCAS");
            LoadCombos.LoadVelCombos(cmb_ROC, "fpm");
            LoadCombos.LoadVelCombos(cmb_Velcr, "KTAS");
            

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

            cartesianChartCarpetPlot.AxisX.Add(new Axis
            {
                Title = "Cruising Speed, KTAS",
                Separator = new Separator
                {
                    Step = 5,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });

            cartesianChartCarpetPlot.AxisY.Add(new Axis
            {
                Title = "Stalling Speed, KCAS",
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
                Title = "Brake-horsepower Required, BHP",
                Separator = new Separator
                {
                    //Step = 25,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Required Cl_max",
                Separator = new Separator
                {
                    //Step = 0.25,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                },
                Position = AxisPosition.RightTop
            });
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        public void CalculateK()
        {
            AR = Double.Parse(txt_AR.Text);
            e = 1.78 * (1 - 0.045 * Math.Pow(AR, 0.68)) - 0.64;
            K = Math.Round(1 / (Math.PI * AR * e), 5);            
        }

        private void Clear()
        {
            txt_AR.Clear();
            txt_CDmin.Clear();
            txt_ClmaxEntry.Clear();
            txt_Height_cl.Clear();
            txt_Height_cr.Clear();
            txt_MinBHP.Clear();
            txt_MinBHP_W_S.Clear();
            txt_PropEfficiency.Clear();
            txt_ROC.Clear();
            txt_Sg.Clear();
            txt_SwEnter.Clear();
            txt_Vcr.Clear();
            txt_Velclimb.Clear();
            txt_Vto.Clear();
            txt_Wto_est.Clear();
            txt_n.Clear();
        }


        //                                   THRUST FUNCTIONS
        //===========================================================================================================

        public void ThrustToWeightCruise()
        {
            try
            {
                Pbhp_cr_set = new List<double>();
                Vcr = Double.Parse(txt_Vcr.Text) * 1.688;
                CDmin = Double.Parse(txt_CDmin.Text);
                n = Double.Parse(txt_n.Text);
                CalculateK();
                rho = Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6);
                q_cr = Math.Round(0.5 * rho * Math.Pow(Vcr, 2), 1);
                Double i = 5;
                Wto_guess = Double.Parse(txt_Wto_est.Text);
                while (i <= 60)
                {
                    T_W_cr = Math.Round(q_cr * ((CDmin / i) + K * Math.Pow((n / q_cr), 2) * i), 4);
                    Double P_cr = Wto_guess * Vcr * T_W_cr / (550 * Double.Parse(txt_PropEff.Text));
                    Pbhp_cr = Math.Round(P_cr / (1.132 * rho/rho_sl - 0.132), 1);
                    Pbhp_cr = IfSetInfinity(Pbhp_cr);
                    Pbhp_cr_set.Add(Pbhp_cr);

                    i = i + 5;
                }
                LoadGraph();
            }
            catch
            {
                return;
            }

        }

        private void ThrustToWeightClimb()
        {
            try
            {
                Pbhp_Climb_set = new List<double>();
                Vclimb = Double.Parse(txt_Velclimb.Text) * 1.688;
                ROC = Double.Parse(txt_ROC.Text);
                CDmin = Double.Parse(txt_CDmin.Text);
                Vv = ROC / 60;
                CalculateK();                
                Double q = Math.Round(0.5 * rho_sl * Math.Pow(Vclimb, 2), 1);
                Double i = 5;
                while (i <= 60)
                {
                    T_W_Climb = Math.Round((Vv / Vclimb) + CDmin * (q / i) + (K / q) * i, 4);
                    Pbhp_Climb = Math.Round(Double.Parse(txt_Wto_est.Text) * Vclimb * T_W_Climb / (550 * Double.Parse(txt_PropEff.Text)), 1);
                    if (Double.IsInfinity(Pbhp_Climb))
                    {
                        Pbhp_Climb = 0;
                    }
                    Pbhp_Climb_set.Add(Pbhp_Climb);

                    i = i + 5;
                }
                LoadGraph();
            }
            catch
            {
                return;
            }
        }

        private void ThrustToWeightTakeoff()
        {
            try
            {
                Pbhp_TO_set = new List<double>();
                Vlof = Double.Parse(txt_Vto.Text) * 1.688;
                Double mu = 0.04;
                Double CDto = Double.Parse(txt_CD_to.Text);
                Double CLto = Double.Parse(txt_Cl_to.Text);
                Double Sg = Double.Parse(txt_Sg.Text);
                Double q = Math.Round(0.5 * rho_sl * Math.Pow(Vlof / Math.Sqrt(2), 2), 2);
                Double rho = Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6);
                Double a = Math.Pow(Vlof, 2) / (2 * AltitudeCalculator.g * Sg / .3048);
                Double i = 5;
                while (i <= 60)
                {
                    Double b = q * CDto / i;
                    Double c = mu * (1 - q * CLto / i);
                    T_W_TO = Math.Round(a + b + c, 4);
                    Double P_cr = Math.Round(Double.Parse(txt_Wto_est.Text) * (Vlof / Math.Sqrt(2)) * T_W_TO / (550 * 0.6), 1);
                    Pbhp_TO = Math.Round(P_cr / (1.132 * rho/rho_sl - 0.132), 1);
                    Pbhp_TO_set.Add(Pbhp_TO);

                    i = i + 5;
                }
                LoadGraph();
            }
            catch
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cartesianChart1.Visible == true)
            {
                cartesianChart1.Visible = false;
                cartesianChartCarpetPlot.Visible = true;
                button1.Text = "BHP and Stall";
            }
            else
            {
                cartesianChart1.Visible = true;
                cartesianChartCarpetPlot.Visible = false;
                button1.Text = "Carpet Plot";
            }
        }
        
        private void ThrustToWeight()
        {
            try
            {
                Pbhp_airspeed_set = new List<double>();
                CalculateK();
                Double i = 5;
                Double rho = Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6);
                while (i <= 60)
                {
                    T_W = Math.Round((q_cr * CDmin / i) + K * i / q_cr, 4);
                    Double P_cr = Double.Parse(txt_Wto_est.Text) * Vcr * T_W / (550 * Double.Parse(txt_PropEff.Text));
                    Pbhp = Math.Round(P_cr / (1.132 * rho/rho_sl - 0.132), 1);
                    if (Double.IsInfinity(Pbhp))
                    {
                        Pbhp = 0;
                    }
                    Pbhp_airspeed_set.Add(Pbhp);

                    i = i + 5;
                }
                LoadGraph();
            }
            catch
            {
                return;
            }
        }        

        private void ThrustToWeightCeiling()
        {
            try
            {
                Pbhp_ceiling_set = new List<double>();
                Double ROc_cl = 100 / 60;
                AltitudeCalculator.CalcAtmos(txt_Height_cl.Text, cmbHeight_cl);
                CalculateK();
                Double rho = Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6);
                Double i = 5;
                while (i <= 60)
                {
                    Double Vy = Math.Sqrt((2 / rho) * i * Math.Sqrt(K / (3 * CDmin)));
                    T_W_ceiling = Math.Round((ROc_cl / Vy) + 4 * Math.Sqrt(K * CDmin / 3), 5);
                    Double P_cr = Double.Parse(txt_Wto_est.Text) * Vy * T_W_ceiling / (550 * Double.Parse(txt_PropEff.Text));
                    Pbhp_ceiling = Math.Round(P_cr / (1.132 * rho/rho_sl - 0.132), 1);
                    if (Double.IsInfinity(Pbhp_ceiling))
                    {
                        Pbhp_ceiling = 0;
                    }
                    Pbhp_ceiling_set.Add(Pbhp_ceiling);

                    i = i + 5;
                }
                LoadGraph();
            }
            catch
            {
                return;
            }
        }

        private void btn_WingDesign_Click(object sender, EventArgs e)
        {
            try
            {
                GAAircraftAirfoilDesign airfoil = new GAAircraftAirfoilDesign();
                airfoil.ShowDialog();
            }
            catch
            {
                return;
            }
        }

        public Double MinPow, q_cr1, W_S;
        private List<Double> MinPow_set, W_S_set;
        private int index;

        public void ThrustToWeightMin()
        {
            try
            {
                MinPow_set = new List<double>();
                W_S_set = new List<double>();
                Double Vcr1 = Double.Parse(txt_Vcr.Text) * 1.688;
                CDmin = Double.Parse(txt_CDmin.Text);
                n = Double.Parse(txt_n.Text);
                CalculateK();
                Double rho = Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6);
                q_cr1 = Math.Round(0.5 * rho * Math.Pow(Vcr1, 2), 1);
                Double i = 5;
                while (i <= 60)
                {
                    Double T_W_cr1 = Math.Round(q_cr1 * ((CDmin / i) + K * Math.Pow((n / q_cr1), 2) * i), 4);
                    Double P_cr = Double.Parse(txt_Wto_est.Text) * Vcr1 * T_W_cr1 / (550 * Double.Parse(txt_PropEff.Text));
                    Double Pbhp_cr1 = Math.Round(P_cr / (1.132 * rho / rho_sl - 0.132), 1);
                    MinPow_set.Add(Pbhp_cr1);
                    W_S_set.Add(i);

                    i = i + 0.1;
                }
                MinPow = MinPow_set.Min();
                index = MinPow_set.IndexOf(MinPow);
                W_S = Math.Round(W_S_set[index], 2);
            }
            catch
            {
                return;
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        //                                       DYNAMIC ENTRY FUNCTIONS
        //==========================================================================================================

        private void txt_Height_cl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                ThrustToWeightCeiling();
                ClmaxGraph();
                ClmaxGraph2();

                AltitudeCalculator.CalcAtmos(txt_Height_cr.Text, cmbHeight_cr);                
                PropEfficiency.PropEff(MinPow, Vcr, Math.Round(AltitudeCalculator.rho_h * 0.00194122449, 6), 78 / 12, 0.85);
                txt_PropEfficiency.Text = PropEffi.ToString();

                VmaxEst();
                CarpetPlot();
                LoadCarpetPlot();
            }
            catch
            {
                return;
            }
        }

        private void txt_PropEff_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ThrustToWeightCeiling();
                ThrustToWeightTakeoff();
                ThrustToWeightClimb();
                ThrustToWeightCruise();
                ThrustToWeight();

            }
            catch
            {
                return;
            }
        }

        private void txt_Vto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AltitudeCalculator.CalcAtmos(txt_Height_cr.Text, cmbHeight_cr);
                ThrustToWeightTakeoff();
            }
            catch
            {
                return;
            }

        }

        private void txt_ROC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AltitudeCalculator.CalcAtmos(txt_Height_cr.Text, cmbHeight_cr);
                ThrustToWeightClimb();
            }
            catch
            {
                return;
            }
        }

        public void txt_Height_cr_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AltitudeCalculator.CalcAtmos(txt_Height_cr.Text, cmbHeight_cr);
                ThrustToWeightCruise();

                ThrustToWeight();
                ThrustToWeightMin();
                txt_MinBHP.Text = MinPow.ToString();
                txt_MinBHP_W_S.Text = W_S.ToString();

                txt_ROC_TextChanged(sender, e);
                txt_Vto_TextChanged(sender, e);
                txt_Height_cl_TextChanged(sender, e);

                //label33.Text = (rho/rho_sl).ToString();
            }
            catch
            {
                return;
            }
        }

        private void txt_SwEnter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                WingAreaListLoad();
                txt_Height_cl_TextChanged(sender, e);
            }
            catch
            {
                return;
            }            
        }

        private void txt_ClmaxEntry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClmaxListLoad();
                txt_Height_cl_TextChanged(sender, e);
            }
            catch
            {
                return;
            }
        }

        private void txt_Wto_est_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_Height_cr_TextChanged(sender, e);
                CalculateK();
            }
            catch
            {
                return;
            }
        }


        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::


        //                                     GRAPH FUNCTIONS
        //===========================================================================================================

        private void LoadCarpetPlot()
        {
            try
            {
                cartesianChartCarpetPlot.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Cl_max 1.6",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,0]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,0]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,0]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,0]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,0]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,0]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,0]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,0])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 1.7",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,1]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,1]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,1]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,1]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,1]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,1]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,1]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,1])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 1.8",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,2]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,2]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,2]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,2]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,2]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,2]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,2]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,2])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 1.9",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,3]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,3]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,3]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,3]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,3]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,3]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,3]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,3])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 2.0",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,4]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,4]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,4]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,4]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,4]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,4]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,4]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,4])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 2.1",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,5]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,5]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,5]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,5]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,5]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,5]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,5]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,5])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 2.2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,6]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,6]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,6]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,6]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,6]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,6]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,6]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,6])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "Cl_max 2.3",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,7]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,7]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,7]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,7]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,7]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,7]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,7]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "90 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,0]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,1]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,2]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,3]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,4]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,5]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,6]),
                        new ObservablePoint(Vmax1_set[0],Vs_set[0,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "100 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,0]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,1]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,2]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,3]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,4]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,5]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,6]),
                        new ObservablePoint(Vmax1_set[1],Vs_set[1,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "110 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,0]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,1]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,2]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,3]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,4]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,5]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,6]),
                        new ObservablePoint(Vmax1_set[2],Vs_set[2,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "120 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,0]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,1]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,2]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,3]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,4]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,5]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,6]),
                        new ObservablePoint(Vmax1_set[3],Vs_set[3,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "130 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,0]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,1]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,2]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,3]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,4]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,5]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,6]),
                        new ObservablePoint(Vmax1_set[4],Vs_set[4,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "140 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,0]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,1]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,2]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,3]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,4]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,5]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,6]),
                        new ObservablePoint(Vmax1_set[5],Vs_set[5,7])
                    }
                },
                new LineSeries
                {
                    Title = "150 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,0]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,1]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,2]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,3]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,4]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,5]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,6]),
                        new ObservablePoint(Vmax1_set[6],Vs_set[6,7])
                    },
                    LineSmoothness = 0
                },
                new LineSeries
                {
                    Title = "160 ft\xB2",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,0]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,1]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,2]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,3]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,4]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,5]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,6]),
                        new ObservablePoint(Vmax1_set[7],Vs_set[7,7])
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
        
        private void LoadGraph()
        {
            try
            {
                cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Service Ceiling",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Pbhp_ceiling_set[0]),
                        new ObservablePoint(10,Pbhp_ceiling_set[1]),
                        new ObservablePoint(20,Pbhp_ceiling_set[3]),
                        new ObservablePoint(30,Pbhp_ceiling_set[5]),
                        new ObservablePoint(40,Pbhp_ceiling_set[7]),
                        new ObservablePoint(50,Pbhp_ceiling_set[9]),
                        new ObservablePoint(55,Pbhp_ceiling_set[10])
                    },
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "Climb Requirement",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Pbhp_Climb_set[0]),
                        new ObservablePoint(10,Pbhp_Climb_set[1]),
                        new ObservablePoint(20,Pbhp_Climb_set[3]),
                        new ObservablePoint(30,Pbhp_Climb_set[5]),
                        new ObservablePoint(40,Pbhp_Climb_set[7]),
                        new ObservablePoint(50,Pbhp_Climb_set[9]),
                        new ObservablePoint(55,Pbhp_Climb_set[10])
                    },
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "Airspeed Requirement",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Pbhp_airspeed_set[0]),
                        new ObservablePoint(10,Pbhp_airspeed_set[1]),
                        new ObservablePoint(20,Pbhp_airspeed_set[3]),
                        new ObservablePoint(30,Pbhp_airspeed_set[5]),
                        new ObservablePoint(40,Pbhp_airspeed_set[7]),
                        new ObservablePoint(50,Pbhp_airspeed_set[9]),
                        new ObservablePoint(55,Pbhp_airspeed_set[10])
                    },
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "T-O Requirement",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Pbhp_TO_set[0]),
                        new ObservablePoint(10,Pbhp_TO_set[1]),
                        new ObservablePoint(20,Pbhp_TO_set[3]),
                        new ObservablePoint(30,Pbhp_TO_set[5]),
                        new ObservablePoint(40,Pbhp_TO_set[7]),
                        new ObservablePoint(50,Pbhp_TO_set[9]),
                        new ObservablePoint(55,Pbhp_TO_set[10])
                    },
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "Turn Requirement",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Pbhp_cr_set[0]),
                        new ObservablePoint(10,Pbhp_cr_set[1]),
                        new ObservablePoint(20,Pbhp_cr_set[3]),
                        new ObservablePoint(30,Pbhp_cr_set[5]),
                        new ObservablePoint(40,Pbhp_cr_set[7]),
                        new ObservablePoint(50,Pbhp_cr_set[9]),
                        new ObservablePoint(55,Pbhp_cr_set[10])
                    },
                    ScalesYAt = 0
                },
                new LineSeries
                {
                    Title = "Cl_max = 60",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Clmax_60[0]),
                        new ObservablePoint(10,Clmax_60[1]),
                        new ObservablePoint(20,Clmax_60[3]),
                        new ObservablePoint(30,Clmax_60[5]),
                        new ObservablePoint(40,Clmax_60[7]),
                        new ObservablePoint(50,Clmax_60[9]),
                        new ObservablePoint(55,Clmax_60[10])
                    },
                    ScalesYAt = 1
                },
                new LineSeries
                {
                    Title = "Cl_max = 65",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(5,Clmax_65[0]),
                        new ObservablePoint(10,Clmax_65[1]),
                        new ObservablePoint(20,Clmax_65[3]),
                        new ObservablePoint(30,Clmax_65[5]),
                        new ObservablePoint(40,Clmax_65[7]),
                        new ObservablePoint(50,Clmax_65[9]),
                        new ObservablePoint(55,Clmax_65[10])
                    },
                    StrokeThickness = 2,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection(2),
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(107, 185, 69)),
                    ScalesYAt = 1
                }
            };
            }
            catch
            {
                return;
            }


        }

        private List<Double> Clmax_60, Clmax_65;

        private void ClmaxGraph()
        {
            Clmax_60 = new List<double>();

            Double Vs = 60;
            Double i = 5;
            Double q = 0.5 * Math.Pow((Vs * 1.688), 2) * AltitudeCalculator.Dens_Standard * 0.00194;
            while (i <= 60)
            {
                Double Cl_max = Math.Round(i / q, 2);
                Clmax_60.Add(Cl_max);

                i = i + 5;
            }
            LoadGraph();
        }

        private void ClmaxGraph2()
        {
            Clmax_65 = new List<double>();

            Double Vs = 65;
            Double i = 5;
            Double q = 0.5 * Math.Pow((Vs * 1.688), 2) * AltitudeCalculator.Dens_Standard * 0.00194;
            while (i <= 60)
            {
                Double Cl_max = Math.Round(i / q, 2);
                Clmax_65.Add(Cl_max);

                i = i + 5;
            }
            LoadGraph();
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::                   

        
        //                                          TABLES FOR CARPET PLOT
        //===========================================================================================================

        List<Double> Vmax1_set;
        Double[,] Max_V;
        Double[,] Vs_set;
        private List<Double> WingArea, MaxCl;

        private void CarpetPlot()
        {
            Vs_set = new Double[8, 8];
            WingArea = new List<double>();
            MaxCl = new List<double>();


            for (int i = 0; i < W_surf.Length; i++)
            {
                for (int j = 0; j < CL_max.Length; j++)
                {
                    WingArea.Add(W_surf[i]);
                    MaxCl.Add(CL_max[j]);
                    Double Vs = Math.Sqrt((2 * Wto_guess) / (AltitudeCalculator.Dens_Standard * 0.00194 * W_surf[i] * CL_max[j]));
                    Vs_set[i, j] = Math.Round(Vs / 1.688,0);
                }
            }

            for (int i = 0; i < Vs_set.GetLength(0); i++)
            {
                string[] row = new string[Vs_set.GetLength(1)];

                for (int j = 0; j < Vs_set.GetLength(1); j++)
                {
                    row[j] = Vs_set[j,i].ToString();
                }

                dataGridView1.Rows.Add(row);
            }
        }

        private void VmaxEst()
        {
            Vmax1_set = new List<double>();
            AltitudeCalculator.CalcAtmos(txt_Height_cr.Text, cmbHeight_cr);
            CalculateK();
            int i = 0;
            while (i < W_surf.Length)
            {
                Double abc = Vmax_Prop.Perf_Vmax_Prop(W_surf[i], K, CDmin, Wto_guess, 0.75 * MinPow, PropEffi);
                Vmax1_set.Add(Math.Round(abc/1.688,0));

                i += 1;
            }
            Max_V = new Double[8, 8];
            for (int j = 0; j < Vmax1_set.Count(); j += 1)
            {
                for (int k = 0; k < Vmax1_set.Count(); k += 1)
                {
                    Max_V[k, j] = Vmax1_set[k];
                }
            }

            for (int m = 0; m < Max_V.GetLength(0); m++)
            {
                string[] row = new string[Max_V.GetLength(1)];

                for (int j = 0; j < Max_V.GetLength(1); j++)
                {
                    row[j] = Max_V[j, m].ToString();
                }

                dataGridView2.Rows.Add(row);
            }
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::        

        private void btn_AltCal_Click(object sender, EventArgs e)
        {
            DeccaCalculator calculator = new DeccaCalculator();
            calculator.ShowDialog();
        }        

        private double IfSetInfinity(double num)
        {
            if (Double.IsInfinity(num))
            {
                num = 0;
            }
            return num;
        }
    }
}
