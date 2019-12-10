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
    public partial class SystemsEngApproachAircraftDesign : BaseWindow
    {
        Double W2_W1 = 0.98; //Engine Warmup, Taxi and Takeoff
        Double W3_W2 = 0.97; //Climb
        Double W5_W4 = 0.99; //Descent
        Double W6_W5 = 0.997; //Approach and Landing

        private List<double> W_S_Vmax,testing, W_S_roc, W_S_roc_c,W_S_TO;

        public Double Pres_Standard = 101325; // Sea Level Pressure (N/m2)

        public Double Temp_Standard = 288.15; // Sea Level Temperature  (K)

        //Double Vis_Standard = 1.7893E-10; // Sea Level Viscosity (m/s)

        public Double Dens_Standard = 1.225; // Sea Level Density (kg/m3)        

        public Double R = 287.05287; // Gas Constant (J/(kgK)

        public Double gamma = 1.4; // Ratio of Specific Heats

        public Double a = 0.0065;

        public Double g = 9.80665; //Gravity

        public Double r_e = 6356766; //Radius of the earth

        //public SeriesCollection SeriesCollection { get; set; }

        public Double P_h, T, rho_h, rel_rho_h, a_h, g_h, m_h, q_c, qc_p0, qc_p, a0, h_int, V_int;

        Double Rng, c, L_D, V_crs, W4_W3,W_c,W_pl,Wf_Wto,a1,b,Wto,hcr_int;

        Double W_S_vs,K;

        Double W_crew = 140; //Weight crew

        Double W_pilot = 200; //Weight pilot            

        private void rad_We_Wto1_CheckedChanged(object sender, EventArgs e)
        {
            CalculateWto();
        }

        Double W_pax = 180; //Weight pax

        private void cmb_Wto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertUnits.ConvertWeighttoLbs(cmb_Wto, txt_Wto, Wto);
        }

        Double W_lug = 50; //Weight luggage

        private void txt_VelStall_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateStallSpeedW_S(Dens_Standard);
            }
            catch
            {
                return;
            }
            
        }

        private void txt_Oswald_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateMaxSpeedW_S();                 

            }
            catch
            {
                
                return;
            }
            
        }

        private void txt_Wto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TakeoffWeightCal();
                CalculateCl();
                CalculateTandSw();
            }
            catch
            {
                return;
            }
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Double Wi = W2_W1 * W3_W2 * Wto;
                Double Wf = Wi * W4_W3;
                
                SystemWingDesign systemWingDesign = new SystemWingDesign(Double.Parse(txt_Wto.Text),
                    Double.Parse(txt_SurfaceArea.Text), Wf, Wi, V_crs, Double.Parse(txt_VelStall.Text));
                systemWingDesign.ShowDialog();
                
            }
            catch
            {
                MessageBox.Show("Please choose empty weight fraction!", "Missing value.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void txt_PaxNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateEmptyWeight();
            }
            catch
            {
                return;
            }
            
        }        

        private void txt_HeightCeiling_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateROC_C_W_S();
            }
            catch
            {
                return;
            }
        }

        private void txt_PropEff_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_H_TO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateTOW_S();
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
                CalculateROCW_S();
                W_S_roc.Clear();                
            }
            catch
            {
                return;
            }
            
        }

        private void txt_W_S_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateTandSw();
                CalculateCl();
            }
            catch
            {
                return;
            }
        }
        
        public void CalcAtmos(string h)
        {
            Double A_Standard = Math.Sqrt(gamma * R * Temp_Standard);
            try
            {
                h_int = Convert.ToDouble(h);
                if (cmbHeight.Text == "m" || cmb_HeightCeiling.Text == "m")
                {
                    h_int = Convert.ToDouble(h); ;
                }
                else if (cmbHeight.Text == "ft" || cmb_HeightCeiling.Text == "ft")
                {
                    h_int = h_int * 0.3048;
                }
                a0 = Math.Sqrt((gamma * Temp_Standard * R));

                //Temperature
                Double T_tps = Temp_Standard - (a * h_int);
                Double T_11km_20km = 216.65;
                //Double T_20km_47km = T_11km_20km + (0.001 * (h_int - 20000));
                Double T_20km_47km = (-131.21 + 273.15) + 0.00299 * h_int;
                Double T_47km = (-131.21 + 273.15) + 0.00299 * 47000;
                g_h = Math.Round(g / (Math.Pow(((r_e + h_int) / r_e), 2)), 3);
                Double g_11km = Math.Round(g / (Math.Pow(((r_e + 11000) / r_e), 2)), 3);
                Double g_20km = Math.Round(g / (Math.Pow(((r_e + 20000) / r_e), 2)), 3);
                Double g_47km = Math.Round(g / (Math.Pow(((r_e + 47000) / r_e), 2)), 3);

                //===================================================================================

                //Atmospheric Conditions at Tropopause (@ 11km)
                Double P_11km = Pres_Standard * Math.Pow((T_11km_20km / Temp_Standard), (g_11km / (a * R)));
                Double rho_11km = P_11km / (R * T_11km_20km);
                Double rel_rho_11km = rho_11km / Dens_Standard;

                Double P_47km = Math.Round(2488 * Math.Pow((T_47km / T_11km_20km), -11.388), 3);

                if (h_int <= 11000)
                {
                    T = T_tps;
                    P_h = Math.Round(Pres_Standard * Math.Pow((T_tps / Temp_Standard), (g_h / (a * R))), 3);
                    rho_h = Math.Round((P_h / (R * T_tps)), 2);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard), 2);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T), 3);
                }
                else if (h_int <= 25000)
                {
                    Double h0 = 11000;
                    T = T_11km_20km;
                    P_h = Math.Round(P_11km * Math.Exp(-(g_h / (R * T)) * (h_int - h0)), 3);
                    rho_h = Math.Round((P_h / (R * T)), 2);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard), 3);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T), 3);
                }
                else if (h_int < 47000)
                {
                    T = T_20km_47km;
                    //P_h = Math.Round(P_20km * Math.Pow((T/T_11km_20km),-(g_h / (0.001*R))), 2);
                    P_h = Math.Round(2488 * Math.Pow((T_20km_47km / T_11km_20km), -11.388), 3);
                    rho_h = Math.Round((P_h / (R * T)), 4);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard), 3);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T), 3);
                }
                else if (h_int < 53000)
                {
                    T = 282.66;
                    //P_h = Math.Round(P_20km * Math.Pow((T/T_11km_20km),-(g_h / (0.001*R))), 2);
                    P_h = Math.Round(P_47km * Math.Exp(-(g_h / (R * T)) * (h_int - 47000)), 3);
                    rho_h = Math.Round((P_h / (R * T)), 4);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard), 3);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T), 3);
                }
                


            }
            catch 
            {
                return;
            }            
        }

        public SystemsEngApproachAircraftDesign()
        {
            InitializeComponent();
            
        }        

        private void cmbEng_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetaAndbForAircraftType();

            cartesianChart1.AxisY.Clear();

            cartesianChart1.Series.Clear(); ;

            if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
            {
                txt_PropEff.Enabled = false;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Thrust Loading (T'W)",
                    Separator = new Separator
                    {
                        Step = 0.1,
                        StrokeThickness = 0.25,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                        IsEnabled = true
                    }
                });
            }
            else 
            {
                txt_PropEff.Enabled = true;
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Thrust Loading (T'W)",
                    Separator = new Separator
                    {
                        Step = 1,
                        StrokeThickness = 0.25,
                        Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                        IsEnabled = true
                    }
                });
            }
        }        

        private void txt_Range_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {            
                e.Handled = true;
            }
        }

        private void txt_Range_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetaAndbForAircraftType();
                CalculateEmptyWeight();
            }
            catch
            {
                return;
            }
        }

        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                //CalculateMaxSpeedW_S();
                //CalculateMaxSpeedW_S();
                CalculateTOW_S();
            }
            catch
            {
                return;
            }
        }        

        private void DeccaAircraftDesign_Load(object sender, EventArgs e)
        {
            txt_PropEff.Enabled = false;           

            //Speeds
            LoadCombos.LoadVelCombos(cmb_VelStall, "knots");
            LoadCombos.LoadVelCombos(cmb_Velmax, "knots");
            LoadCombos.LoadVelCombos(cmb_VelCruise, "mach");
            LoadCombos.LoadVelCombos(cmb_ROC, "fpm");

            //Weight
            LoadCombos.LoadWeightCombos(cmb_PassWeight,"lbs");
            LoadCombos.LoadWeightCombos(cmb_CrewWeight,"lbs");
            LoadCombos.LoadWeightCombos(cmb_PilotWeight,"lbs");
            LoadCombos.LoadWeightCombos(cmb_Wto,"lbs");
            txt_CrewWeight.Text = W_crew.ToString();
            txt_PilotWeight.Text = W_pilot.ToString();
            txt_PaxWeight.Text = W_pax.ToString();

            //Distances
            LoadCombos.LoadDistanceCombos(cmb_Range, "km");
            LoadCombos.LoadDistanceCombos(cmbHeight, "ft");
            LoadCombos.LoadDistanceCombos(cmb_HeightCeiling, "ft");
            LoadCombos.LoadDistanceCombos(cmb_H_TO, "ft");
            LoadCombos.LoadDistanceCombos(cmb_S_TO, "ft");

            //Aircraft Type
            LoadCombos.LoadAircraftType(cmbAircft_Type);           

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Wing Loading (W/S)",
                Separator = new Separator
                {  
                    Step = 20,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });
            JetTransportTest();
        }        

        private void TakeoffWeightCal()
        {
            //Cruise fuel fraction
            if (txt_Range.Text.Trim().Length != 0 || txt_FuelEff.Text.Trim().Length != 0
                 || txt_CruiseHeight.Text.Trim().Length != 0 || txt_LiftToDrag.Text.Trim().Length != 0
                  || txt_VelCruise.Text.Trim().Length != 0)
            {
                if (cmb_Range.Text == "km")
                {
                    Rng = Double.Parse(txt_Range.Text)*1000*3.28;
                }

                c = Double.Parse(txt_FuelEff.Text)/3600;  
                
                CalcAtmos(txt_CruiseHeight.Text);
                if (cmb_VelCruise.Text == "mach")
                {
                    V_crs = Double.Parse(txt_VelCruise.Text) * a_h/0.3048;
                }
                else if (cmb_VelCruise.Text == "knots")
                {
                    V_crs = Double.Parse(txt_VelCruise.Text) * 1.688;
                }
                Double exp = -(Rng * c)/(0.866*V_crs*Double.Parse(txt_LiftToDrag.Text));
                W4_W3 = Math.Exp(exp);                 
            }
            else
            {
                lbl1.Text = "";
            }
        }

        private void OverallWeightFraction()
        {
            TakeoffWeightCal();
            Double W6_W1 = W2_W1 * W3_W2 * W4_W3 * W5_W4 * W6_W5;
            Wf_Wto = Math.Round(1.05 * (1 - W6_W1), 3);            
        }

        private void HumanWeights()
        {
            if (txt_PaxNo.Text.Trim().Length != 0 || txt_Pilot.Text.Trim().Length != 0 || txt_CrewNo.Text.Trim().Length != 0)
            {
                W_pl = (W_pax + W_lug) * Double.Parse(txt_PaxNo.Text);
                W_c = W_pilot * Double.Parse(txt_Pilot.Text) + W_crew * Double.Parse(txt_CrewNo.Text);                
            }
            else
            {                
                lbl1.Text = "";
            }            
        }

        private void SetaAndbForAircraftType()
        {
            if (cmbAircft_Type.Text == "Jet Transport")
            {
                a1 = -7.754E-8;
                b = 0.576;
            }
            else if (cmbAircft_Type.Text == "Business Jet")
            {
                a1 = 1.13E-6;
                b = 0.48;
            }
            else if (cmbAircft_Type.Text == "Sailplane (glider)")
            {
                a1 = 6.53E-3;
                b = -1.663;
            }
            else if (cmbAircft_Type.Text == "Home-built")
            {
                a1 = -4.6E-5;
                b = 0.68;
            }
            else if (cmbAircft_Type.Text == "Supersonic Fighter")
            {
                a1 = -1.1E-5;
                b = 0.97;
            }
            else if (cmbAircft_Type.Text == "Subsonic Military")
            {
                a1 = 1.39E-6;
                b = 0.64;
            }
            else if (cmbAircft_Type.Text == "GA-single Engine")
            {
                a1 = 1.543E-5;
                b = 0.57;
            }
            else if (cmbAircft_Type.Text == "GA-twin Engine")
            {
                a1 = 2.73E-4;
                b = -9.08;
            }
        }
        
        private void CalculateEmptyWeight()
        {
            //SetaAndbForAircraftType();
            HumanWeights();
            OverallWeightFraction();
            Double Wp = W_c + W_pl;
            Double wf1 = 1 - Wf_Wto;
            Double c2 = (-b*wf1-a1*Wp);
            Double b2 = (wf1 + b);
            Double a2 = -1;
            Double inner = Math.Sqrt(Math.Pow(b2, 2) - 4 * a2 * c2);
            Double x1 = (-b2 - inner) / (2 * a2);
            Double x2 = (-b2 + inner) / (2 * a2);
            rad_We_Wto1.Text = Math.Round(x1,4).ToString();
            rad_We_Wto2.Text = Math.Round(x2,4).ToString();           
        }

        private void CalculateWto()
        {
            try
            {
                if (rad_We_Wto1.Checked)
                {
                    Wto = Math.Round((W_pl + W_c) / (1 - Wf_Wto - Double.Parse(rad_We_Wto1.Text)), 2);
                    txt_Wto.Text = Wto.ToString();                    
                }
                else if (rad_We_Wto2.Checked)
                {
                    Wto = Math.Round((W_pl + W_c) / (1 - Wf_Wto - Double.Parse(rad_We_Wto2.Text)), 2);
                    txt_Wto.Text = Wto.ToString();                    
                }
            }
            catch
            {
                return;
            }
            
        }

        private void CalculateStallSpeedW_S(double rho)
        {
            W_S_vs = 0.5 * rho * 0.00194 * Math.Pow((Double.Parse(txt_VelStall.Text) * 1.688), 2) * Double.Parse(txt_Clmax.Text);            
        }

        private void CalculateTOW_S()
        {
            CalculateK();
            CalcAtmos(txt_H_TO.Text);

            Double V_to = 1.1 * Double.Parse(txt_VelStall.Text);
            Double mu = 0.04; //Friction Coefficient

            Double CD0_TO = Double.Parse(txt_Cd0.Text) + Double.Parse(txt_CD0_LG.Text) + Double.Parse(txt_CD0_HLD_TO.Text);
            Double CL_TO = Double.Parse(txt_CL_C.Text) + Double.Parse(txt_CL_FLAP_TO.Text);

            Double CD_TO = CD0_TO + K * Math.Pow(CL_TO, 2);

            Double CD_G = Math.Round((CD_TO - mu * CL_TO),3);

            Double CL_R = Math.Round((Double.Parse(txt_Clmax.Text) / 1.21),3);

            W_S_TO = new List<double>();

            double i = 20;
            while (i <= 160)
            {
                Double inner = Math.Exp(0.6 * rho_h * Math.Round((g_h / 0.3048), 1) * 0.00194 * CD_G * Double.Parse(txt_S_TO.Text)/ (i));
                Double Top = mu - Math.Round((mu + CD_G / CL_R),3) * inner;
                Double Bottom = 1 - inner;
                if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
                {
                    W_S_TO.Add(Math.Round((Top / Bottom), 3));
                }
                else
                {
                    W_S_TO.Add(Math.Round((Bottom / Top)*(Double.Parse(txt_PropEff.Text)*550/(V_to*1.688)), 3));                    
                }

                i = i + 20;
            }
            
            listBox1.DataSource = W_S_TO;
            LoadGraph2();
        }

        private void CalculateK()
        {
            K = Math.Round(1 / (Math.PI * Double.Parse(txt_AR.Text) * Double.Parse(txt_Oswald.Text)),3);            
        }

        private void CalculateMaxSpeedW_S()
        {
            CalculateK();
            CalcAtmos(txt_CruiseHeight.Text);
            W_S_Vmax = new List<double>();
            double i = 20;
            while (i <= 160)
            {               
                if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
                {
                    Double x = Math.Round(Dens_Standard * 0.00194 * Double.Parse(txt_Cd0.Text) * Math.Pow(Double.Parse(txt_Vmax.Text) * 1.688, 2) / (2 * i), 3);
                    Double y = Math.Round((2 * K * i) / (rho_h *0.00194* rel_rho_h * Math.Pow(Double.Parse(txt_Vmax.Text) * 1.688, 2)), 4);
                    Double W_S = Math.Round((x + y), 3);
                    W_S_Vmax.Add(W_S);
                }
                else
                {
                    Double x = Math.Round(Dens_Standard * 0.00194 * Double.Parse(txt_Cd0.Text) * Math.Pow(Double.Parse(txt_Vmax.Text) * 1.688, 3) / (2 * i), 3);
                    Double y = Math.Round((2 * K * i) / (rho_h *0.00194* rel_rho_h * Double.Parse(txt_Vmax.Text) * 1.688), 4);
                    Double W_S = Math.Round((Double.Parse(txt_PropEff.Text) * 550) / (x + y), 3);

                    //W_S_Vmax.Add(W_S);
                    W_S_Vmax.Add(W_S);
                }               
                i = i + 20;                
            }
            listBox1.DataSource = W_S_Vmax;
            LoadGraph2();
        }         

        private void CalculateROCW_S()
        {
            CalculateK();
            W_S_roc = new List<double>();
            double i = 20;
            while (i <= 160)
            {
                if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
                {
                    Double part1 = Dens_Standard * 0.00194 * Math.Sqrt(Double.Parse(txt_Cd0.Text) / K);
                    Double part2 = Math.Round(Math.Sqrt((2 * i) / part1), 3);
                    Double roc = Double.Parse(txt_ROC.Text) / 60;
                    W_S_roc.Add(Math.Round((roc / part2) + 1 / Double.Parse(txt_LiftToDrag.Text), 3));
                }
                else
                {
                    Double part1 = Dens_Standard * 0.00194 * Math.Sqrt(Double.Parse(txt_Cd0.Text)*3 / K);
                    Double part2 = Math.Round(Math.Sqrt((2 * i) / part1), 3);
                    Double roc = Double.Parse(txt_ROC.Text) / 60;
                    Double part3 = (roc / Double.Parse(txt_PropEff.Text)) + part2 * (1.155 / (Double.Parse(txt_PropEff.Text) * Double.Parse(txt_LiftToDrag.Text)));
                    W_S_roc.Add(Math.Round(550/part3, 3));
                }
                i = i + 20;
            }
            listBox1.DataSource = W_S_roc;
            LoadGraph2();
        }

        private void CalculateROC_C_W_S()
        {
            CalculateK();
            W_S_roc_c = new List<double>();
            CalcAtmos(txt_HeightCeiling.Text);
            double i = 20;
            while (i <= 160)
            {
                if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
                {                    
                    Double part1 = rho_h * 0.00194032 * Math.Sqrt(Double.Parse(txt_Cd0.Text) / K);
                    Double part2 = Math.Round(Math.Sqrt((2 * i) / part1), 3);
                    Double roc = 100 / 60;
                    W_S_roc_c.Add(Math.Round(((roc / part2) + 1 / Double.Parse(txt_LiftToDrag.Text)) / rel_rho_h, 3));
                }
                else
                {
                    Double part1 = rho_h * 0.00194032 * Math.Sqrt(Double.Parse(txt_Cd0.Text) * 3 / K);
                    Double part2 = Math.Round(Math.Sqrt((2 * i) / part1), 3);
                    Double roc1 = 100 / 60;
                    Double part3 = (roc1 / Double.Parse(txt_PropEff.Text)) + part2 * (1.155 / (Double.Parse(txt_PropEff.Text) * Double.Parse(txt_LiftToDrag.Text)));
                    W_S_roc_c.Add(Math.Round(550*rel_rho_h / part3, 3));                   
                }
                i = i + 20;
            }

            listBox1.DataSource = W_S_roc_c;
            LoadGraph2();
        }

        public int ytop;

        private void LoadGraph2()
        {
            if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
            {
                ytop = 1;
            }
            else
            {
                ytop = 10;
            }
                cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Rate of Climb",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(20,W_S_roc[0]),
                        new ObservablePoint(40,W_S_roc[1]),
                        new ObservablePoint(60,W_S_roc[2]),
                        new ObservablePoint(80,W_S_roc[3]),
                        new ObservablePoint(100,W_S_roc[4]),
                        new ObservablePoint(120,W_S_roc[5]),
                        new ObservablePoint(140,W_S_roc[6]),
                        new ObservablePoint(160,W_S_roc[7])                        
                    }
                },
                new LineSeries
                {
                    Title = "Max Velocity",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(20,W_S_Vmax[0]),
                        new ObservablePoint(40,W_S_Vmax[1]),
                        new ObservablePoint(60,W_S_Vmax[2]),
                        new ObservablePoint(80,W_S_Vmax[3]),
                        new ObservablePoint(100,W_S_Vmax[4]),
                        new ObservablePoint(120,W_S_Vmax[5]),
                        new ObservablePoint(140,W_S_Vmax[6]),
                        new ObservablePoint(160,W_S_Vmax[7])
                    }
                },
                new LineSeries
                {
                    Title = "Ceiling",
                   Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(20,W_S_roc_c[0]),
                        new ObservablePoint(40,W_S_roc_c[1]),
                        new ObservablePoint(60,W_S_roc_c[2]),
                        new ObservablePoint(80,W_S_roc_c[3]),
                        new ObservablePoint(100,W_S_roc_c[4]),
                        new ObservablePoint(120,W_S_roc_c[5]),
                        new ObservablePoint(140,W_S_roc_c[6]),
                        new ObservablePoint(160,W_S_roc_c[7])
                    }
                },
                new LineSeries
                {
                    Title = "Take-off",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(20,W_S_TO[0]),
                        new ObservablePoint(40,W_S_TO[1]),
                        new ObservablePoint(60,W_S_TO[2]),
                        new ObservablePoint(80,W_S_TO[3]),
                        new ObservablePoint(100,W_S_TO[4]),
                        new ObservablePoint(120,W_S_TO[5]),
                        new ObservablePoint(140,W_S_TO[6]),
                        new ObservablePoint(160,W_S_TO[7])
                    }
                },                 
                new LineSeries
                {
                    Title = "Stall Speed",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(W_S_vs,0),
                        new ObservablePoint(W_S_vs,ytop)
                    }
                }
                
            };
        }

        Double S_w, Thr;
        
        private void CalculateTandSw()
        {
            if (cmb_Wto.Text == "lbs")
            {
                S_w = Math.Round(Wto / Double.Parse(txt_W_S.Text), 1);
                Thr = Math.Round(Wto * Double.Parse(txt_T_W.Text), 1);
                txt_Thrust.Text = Thr.ToString();
                txt_SurfaceArea.Text = S_w.ToString();
            }
        }

        private void CalculateCl()
        {
            Double Wavg = Wto * 0.4535924;
            Double Vcr = V_crs * 0.3028;

            Double Cl_c = Math.Round((2 * Wavg*9.81/(Dens_Standard*Math.Pow(Vcr,2)* (S_w * 0.092903))),3);
            Double Cl_cw = Math.Round((Cl_c / 0.95),3);
            Double Cl_i = Math.Round((Cl_cw / 0.9), 3);

            txt_Clc.Text = Cl_c.ToString();
            txt_Clcw.Text = Cl_cw.ToString();
            txt_Cli.Text = Cl_i.ToString();

            Double Clmax = Math.Round((2 * Wavg * 9.81 / (Dens_Standard * Math.Pow(Double.Parse(txt_VelStall.Text) * 0.5144, 2) * (S_w * 0.092903))), 3);
            Double Cl_max_W = Math.Round((Clmax / 0.95),3);
            Double Cl_max_W_gross = Math.Round((Cl_max_W / 0.90), 3);

            txt_Cl_max.Text = Clmax.ToString();
            txt_Clmax_w.Text = Cl_max_W.ToString();
            txt_Clmax_gross.Text = Cl_max_W_gross.ToString();            
        }

        private void JetTransportTest()
        {
            cmbAircft_Type.SelectedIndex = 0;
            txt_Range.Text = "9500";
            txt_PaxNo.Text = "700";
            txt_Pilot.Text = "2";
            txt_CrewNo.Text = "14";
            txt_CruiseHeight.Text = "35000";
            txt_LiftToDrag.Text = "17";
            txt_VelCruise.Text = "0.8";


            txt_AR.Text = "8.2";
            txt_Oswald.Text = "0.85";
            txt_Cd0.Text = "0.019";

            txt_Vmax.Text = "500";
            txt_HeightCeiling.Text = "40000";

            //txt_H_TO.Text = "3000";
            txt_S_TO.Text = "9000";

            txt_VelStall.Text = "110";

            txt_CD0_HLD_TO.Text = "0.005";
            txt_CD0_LG.Text = "0.009";
            txt_CL_FLAP_TO.Text = "0.6";
            txt_CL_C.Text = "0.3";

            txt_Clmax.Text = "1.8";
            txt_ROC.Text = "2700";

            txt_W_S.Clear();
            txt_T_W.Clear();
        }

    }
}