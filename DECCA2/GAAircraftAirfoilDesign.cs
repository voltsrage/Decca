using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Axis = LiveCharts.Wpf.Axis;
using SeriesCollection = LiveCharts.SeriesCollection;

namespace DECCA2
{
    public partial class GAAircraftAirfoilDesign : Form
    {
        Double N,t,x_camber,C,m,k1,a,Cl_i,Wto,Vc;

        Double[] xlist, ylist, yc_list, yc_prime_list, phi_list,theta_list,xu_list
            ,xl_list, yu_list, yl_list;

        private void txt_testWto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                Double W0 = 10800 / (0.613 - 0.93 * Math.Pow(Double.Parse(txt_testWto.Text), -0.07));
                label21.Text = W0.ToString();
            }
            catch
            {
                return;
            }
        }

        Double cr_avg, c_rw, c_tw, c_mgcw, x_mgcw, y_mgcw, b_w, AR_w, S_w, tr_w, Le_sw, Qr_sw, d;

        private void txt_tr_w_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                CalculateClc();

                tr_w = Double.Parse(txt_tr_w.Text);

                c_rw = Math.Round(cr_avg * 2 / (tr_w + 1), 2);
                txt_c_rw.Text = c_rw.ToString();

                c_tw = Math.Round(c_rw * tr_w, 2);
                txt_c_tw.Text = c_tw.ToString();

                Double inner = (1 + tr_w + Math.Pow(tr_w, 2)) / (1 + tr_w);
                c_mgcw = Math.Round((2 / 3) * c_rw * inner, 2);
                txt_mgcw.Text = y_mgcw.ToString();

                y_mgcw = Math.Round((b_w / 6) * ((1 + 2 * tr_w) / (1 + tr_w)), 2);
                txt_y_mgcw.Text = y_mgcw.ToString();

                Le_sw = Double.Parse(txt_Leadingsweep.Text) * Math.PI / 180;
                x_mgcw = Math.Round(y_mgcw * Math.Tan(Le_sw),2);
                txt_x_mgcw.Text = x_mgcw.ToString();

                Double Qr_sw_rad = Math.Tan(Le_sw) + (c_rw*.5/b_w)*(tr_w - 1);
                Qr_sw = Math.Round(Math.Atan(Qr_sw_rad)*180/Math.PI, 2);
                txt_QuarterChordsweep.Text = Qr_sw.ToString();

                d = ((b_w/2) * Math.Tan(Le_sw));    
                
                LoadPlanformGraph2();
            }
            catch
            {
                return;
            }

        }

        GAAircraftDesign roskam = new GAAircraftDesign();
        
        private void cmb_AircraftType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();            
            if (cmb_AircraftType.Text == "NACA Four-digit")
            {
                cmb_MeanLine.Visible = false;
                lbl_Meanline.Visible = false;

                label6.Visible = false;
                label7.Visible = false;

                txt_a.Visible = false;
                txt_Cli.Visible = false;
            }
            else if (cmb_AircraftType.Text == "NACA Five-digit")
            {
                cmb_MeanLine.Visible = false;
                lbl_Meanline.Visible = false;

                label6.Visible = false;
                label7.Visible = false;

                txt_a.Visible = false;
                txt_Cli.Visible = false;
            }
            else if (cmb_AircraftType.Text == "NACA Six-digit")
            {
                cmb_MeanLine.Visible = false;
                lbl_Meanline.Visible = false;

                label6.Visible = true;
                label7.Visible = true;

                txt_a.Visible = true;
                txt_Cli.Visible = true;
            }
        }

        private void cmb_MeanLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_MeanLine.Text == "210")
            {
                x_camber = 0.05;
                m = 0.0580;
                k1 = 361.4;
            }
            else if (cmb_MeanLine.Text == "220")
            {
                x_camber = 0.10;
                m = 0.1260;
                k1 = 51.640;
            }
            else if (cmb_MeanLine.Text == "230")
            {
                x_camber = 0.15;
                m = 0.2025;
                k1 = 15.957;
            }
            else if (cmb_MeanLine.Text == "240")
            {
                x_camber = 0.20;
                m = 0.2900;
                k1 = 6.643;
            }
            else if (cmb_MeanLine.Text == "250")
            {
                x_camber = 0.25;
                m = 0.2900;
                k1 = 3.230;
            }
        }

        public GAAircraftAirfoilDesign()
        {
            InitializeComponent();
        }

        private void AirfoilDesign_Load(object sender, EventArgs e)
        {
            lbl_Meanline.Visible = false;
            cmb_MeanLine.Visible = false;

            label6.Visible = false;
            label7.Visible = false;

            txt_a.Visible = false;
            txt_Cli.Visible = false;

            var obj = new GAAircraftDesign();
            obj.CalculateK();
            obj.ThrustToWeightMin();
            obj.ThrustToWeightCruise();

            Double W_S_w = obj.W_S;
            txt_W_S.Text = W_S_w.ToString();

            Wto = obj.Wto_guess;
            Vc = obj.Vcr;

            S_w = Math.Round(Wto/ obj.W_S, 2);
            txt_S_w.Text = S_w.ToString();

            AR_w = obj.AR;
            txt_AR.Text = AR_w.ToString();

            b_w = Math.Round(Math.Sqrt(obj.AR * S_w), 2);
            txt_b_w.Text = b_w.ToString();

            cr_avg = b_w / AR_w;
            txt_c_avg.Text = cr_avg.ToString();

           
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Wingspan, (ft)",
                Separator = new Separator
                {
                    Step = 1,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });

            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Chord. (ft)",
                Separator = new Separator
                {
                    //Step = 25,
                    StrokeThickness = 0.25,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(64, 79, 86)),
                    IsEnabled = true
                }
            });
            
        }

        private void Calculate4Digit()
        {
            N = Double.Parse(txt_N.Text);

            xlist = new Double[Convert.ToInt16(N)];
            ylist = new Double[Convert.ToInt16(N)];
            yc_list = new Double[Convert.ToInt16(N)];
            yc_prime_list = new Double[Convert.ToInt16(N)];
            phi_list = new Double[Convert.ToInt16(N)];
            theta_list = new Double[Convert.ToInt16(N)];
            xu_list = new Double[Convert.ToInt16(N)];
            xl_list = new Double[Convert.ToInt16(N)];
            yu_list = new Double[Convert.ToInt16(N)];
            yl_list = new Double[Convert.ToInt16(N)];

            xlist[0] = 0;

            t = Double.Parse(txt_Thick.Text);
            C = Double.Parse(txt_CamberPerChord.Text);
            x_camber = Double.Parse(txt_Locx.Text);            

            ylist[0] = Math.Round(5 * t * (0.29690 * Math.Sqrt(xlist[0]) - 0.12600 * xlist[0]
                - 0.35160 * Math.Pow(xlist[0], 2) + 0.28430 * Math.Pow(xlist[0], 3)
                - 0.10150 * Math.Pow(xlist[0], 4)), 4);

            yc_list[0] = Math.Round(C * (2 * x_camber - xlist[0]) * xlist[0] / Math.Pow(x_camber, 2), 4);
            yc_prime_list[0] = Math.Round((2 * C / x_camber) * (1 - xlist[0] / x_camber), 2);
            phi_list[0] = 0 * 90 / (N - 1);
            theta_list[0] = Math.Round(Math.Atan(yc_prime_list[0]) * 57.3, 2);
            xu_list[0] = xlist[0] - ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            xl_list[0] = xlist[0] + ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            yu_list[0] = yc_list[0] + ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);
            yl_list[0] = yc_list[0] - ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);

            Double delta_phi = Math.PI / (2 * (N - 1));

            for (int i = 1; i < xlist.Length; i++)
            {                 
                xlist[i] = Math.Round(1 - Math.Cos(i * delta_phi),4);
                ylist[i] = Math.Round(5 *t* (0.29690 * Math.Sqrt(xlist[i]) - 0.12600* xlist[i]
                    - 0.35160*Math.Pow(xlist[i], 2) + 0.28430 * Math.Pow(xlist[i], 3)
                    - 0.10150 * Math.Pow(xlist[i], 4)),4);
                phi_list[i] = Math.Round(i * 90 / (N - 1),2);

                if (xlist[i] <= x_camber)
                {
                    yc_list[i] = Math.Round(C * (2 * x_camber - xlist[i])* xlist[i] / Math.Pow(x_camber, 2), 4);
                    yc_prime_list[i] = Math.Round((2 * C / x_camber) * (1 - xlist[i] / x_camber),4);
                }
                else
                {
                    yc_list[i] = Math.Round(C * ((1 - 2 * x_camber) + 2*x_camber*xlist[i] 
                        - Math.Pow(xlist[i], 2)) / Math.Pow(1 - x_camber, 2), 4);
                    yc_prime_list[i] = Math.Round((2 * C * (x_camber - xlist[i])/ Math.Pow(1 - x_camber, 2)), 4); ;
                }
                theta_list[i] = Math.Round(Math.Atan(yc_prime_list[i]) * 57.3, 2);
                xu_list[i] = Math.Round(xlist[i] - ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);
                xl_list[i] = Math.Round(xlist[i] + ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);

                yu_list[i] = Math.Round(yc_list[i] + ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
                yl_list[i] = Math.Round(yc_list[i] - ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
            }            
            listBox1.DataSource = xlist;
            listBox2.DataSource = ylist;
            listBox3.DataSource = yc_list;
            listBox4.DataSource = yc_prime_list;
            listBox5.DataSource = phi_list;
            listBox6.DataSource = theta_list;
            listBox7.DataSource = xu_list;
            listBox8.DataSource = yu_list; 
            listBox9.DataSource = xl_list;
            listBox10.DataSource = yl_list;
        }

        private void Calculate5Digit()
        {
            N = Double.Parse(txt_N.Text);

            xlist = new Double[Convert.ToInt16(N)];
            ylist = new Double[Convert.ToInt16(N)];
            yc_list = new Double[Convert.ToInt16(N)];
            yc_prime_list = new Double[Convert.ToInt16(N)];
            phi_list = new Double[Convert.ToInt16(N)];
            theta_list = new Double[Convert.ToInt16(N)];
            xu_list = new Double[Convert.ToInt16(N)];
            xl_list = new Double[Convert.ToInt16(N)];
            yu_list = new Double[Convert.ToInt16(N)];
            yl_list = new Double[Convert.ToInt16(N)];

            xlist[0] = 0;

            t = Double.Parse(txt_Thick.Text);
            C = Double.Parse(txt_CamberPerChord.Text);
            x_camber = Double.Parse(txt_Locx.Text);

            m = 0.003 + x_camber + 2.2 * Math.Pow(x_camber, 2);
            k1 = 0.05991 * Math.Pow(x_camber, -2.919);

            ylist[0] = Math.Round(5 * t * (0.29690 * Math.Sqrt(xlist[0]) - 0.12600 * xlist[0]
                - 0.35160 * Math.Pow(xlist[0], 2) + 0.28430 * Math.Pow(xlist[0], 3)
                - 0.10150 * Math.Pow(xlist[0], 4)), 4);

            yc_list[0] = Math.Round((k1/6)*(Math.Pow(xlist[0],3) - 3*m* Math.Pow(xlist[0],2) +
                Math.Pow(m,2)*(3-m)*xlist[0]), 4);
            yc_prime_list[0] = Math.Round((2 * C / x_camber) * (1 - xlist[0] / x_camber), 2);
            phi_list[0] = 0 * 90 / (N - 1);
            theta_list[0] = Math.Round(Math.Asin(yc_prime_list[0]) * 57.3, 2);
            xu_list[0] = xlist[0] - ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            xl_list[0] = xlist[0] + ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            yu_list[0] = yc_list[0] + ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);
            yl_list[0] = yc_list[0] - ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);

            Double delta_phi = Math.PI / (2 * (N - 1));

            for (int i = 1; i < xlist.Length; i++)
            {
                xlist[i] = Math.Round(1 - Math.Cos(i * delta_phi), 4);
                ylist[i] = Math.Round(5 * t * (0.29690 * Math.Sqrt(xlist[i]) - 0.12600 * xlist[i]
                    - 0.35160 * Math.Pow(xlist[i], 2) + 0.28430 * Math.Pow(xlist[i], 3)
                    - 0.10150 * Math.Pow(xlist[i], 4)), 4);
                phi_list[i] = Math.Round(i * 90 / (N - 1), 2);

                if (xlist[i] <= x_camber)
                {
                    yc_list[i] = Math.Round((k1 / 6) * (Math.Pow(xlist[i], 3) - 3 * m * Math.Pow(xlist[i], 2) +
                    Math.Pow(m, 2) * (3 - m) * xlist[i]), 4);
                    yc_prime_list[i] = Math.Round((2 * C / x_camber) * (1 - xlist[i] / x_camber), 4);
                }
                else
                {
                    yc_list[i] = Math.Round((k1 * Math.Pow(m,3))*(1 - xlist[i])/6, 4);
                    yc_prime_list[i] = Math.Round((2 * C * (x_camber - xlist[i]) / Math.Pow(1 - x_camber, 2)), 4); ;
                }
                theta_list[i] = Math.Round(Math.Asin(yc_prime_list[i]) * 57.3, 2);
                xu_list[i] = Math.Round(xlist[i] - ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);
                xl_list[i] = Math.Round(xlist[i] + ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);

                yu_list[i] = Math.Round(yc_list[i] + ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
                yl_list[i] = Math.Round(yc_list[i] - ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
            }
            listBox1.DataSource = xlist;
            listBox2.DataSource = ylist;
            listBox3.DataSource = yc_list;
            listBox4.DataSource = yc_prime_list;
            listBox5.DataSource = phi_list;
            listBox6.DataSource = theta_list;
            listBox7.DataSource = xu_list;
            listBox8.DataSource = yu_list;
            listBox9.DataSource = xl_list;
            listBox10.DataSource = yl_list;
        }

        private void Calculate6Digit()
        {
            N = Double.Parse(txt_N.Text);

            xlist = new Double[Convert.ToInt16(N)];
            ylist = new Double[Convert.ToInt16(N)];
            yc_list = new Double[Convert.ToInt16(N)];
            yc_prime_list = new Double[Convert.ToInt16(N)];
            phi_list = new Double[Convert.ToInt16(N)];
            theta_list = new Double[Convert.ToInt16(N)];
            xu_list = new Double[Convert.ToInt16(N)];
            xl_list = new Double[Convert.ToInt16(N)];
            yu_list = new Double[Convert.ToInt16(N)];
            yl_list = new Double[Convert.ToInt16(N)];

            xlist[0] = 0;

            t = Double.Parse(txt_Thick.Text);
            C = Double.Parse(txt_CamberPerChord.Text);
            x_camber = Double.Parse(txt_Locx.Text);

            Cl_i = Double.Parse(txt_Cli.Text);

            a = Double.Parse(txt_a.Text);

            ylist[0] = Math.Round(5 * t * (0.29690 * Math.Sqrt(xlist[0]) - 0.12600 * xlist[0]
                - 0.35160 * Math.Pow(xlist[0], 2) + 0.28430 * Math.Pow(xlist[0], 3)
                - 0.10150 * Math.Pow(xlist[0], 4)), 4);
            if (a == 1)
            {
                yc_list[0] = Math.Round(-(Cl_i / (4 * Math.PI)) * ((1 - x_camber) * Math.Log(1 - x_camber) +
                t * Math.Log(t)), 4);
                yc_prime_list[0] = Math.Round((Cl_i / (4 * Math.PI)) * (Math.Log(1 - x_camber) - Math.Log(x_camber)), 4);
            }
            else if (a < 1)
            {
                Double g = (-1 / (1 - a)) * (Math.Pow(a,2)*(0.5*Math.Log(a) - 0.25) + 0.25);
                Double h = (1 / (1 - a)) * (0.5 * Math.Pow(1 - a, 2) * Math.Log(1 - a) - 0.25*Math.Pow(1-a,2)) + g;

                Double outside = Cl_i / (2 * Math.PI * (1 + a));

                Double s = 0.5 * Math.Pow(a - x_camber, 2) * Math.Log(a - x_camber);
                Double u = 0.5 * Math.Pow(1 - x_camber, 2) * Math.Log(1 - x_camber);
                Double v = 0.25 * Math.Pow(1 - x_camber, 2);
                Double w = 0.25 * Math.Pow(a - x_camber, 2);

                Double first = (1 / (1 - a)) * (s - u + v - w);

                Double second = first - x_camber * Math.Log(x_camber) + g - h * x_camber;

                yc_list[0] = outside*(second);

                Double third = (1 / (1 - a)) * ((1 - x_camber) * Math.Log(1 - x_camber) - (a - x_camber) * Math.Log(a - x_camber));
                Double fourth = third - Math.Log(t) - 1 - h;

                yc_prime_list[0] = outside * fourth;
            }
            phi_list[0] = 0 * 90 / (N - 1);
            theta_list[0] = Math.Round(Math.Atan(yc_prime_list[0]) * 57.3, 2);
            xu_list[0] = xlist[0] - ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            xl_list[0] = xlist[0] + ylist[0] * Math.Sin(theta_list[0] * Math.PI / 180);
            yu_list[0] = yc_list[0] + ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);
            yl_list[0] = yc_list[0] - ylist[0] * Math.Cos(theta_list[0] * Math.PI / 180);

            Double delta_phi = Math.PI / (2 * (N - 1));

            for (int i = 1; i < xlist.Length; i++)
            {
                xlist[i] = Math.Round(1 - Math.Cos(i * delta_phi), 4);
                ylist[i] = Math.Round(5 * t * (0.29690 * Math.Sqrt(xlist[i]) - 0.12600 * xlist[i]
                    - 0.35160 * Math.Pow(xlist[i], 2) + 0.28430 * Math.Pow(xlist[i], 3)
                    - 0.10150 * Math.Pow(xlist[i], 4)), 4);
                phi_list[i] = Math.Round(i * 90 / (N - 1), 2);


                if (a == 1)
                {
                    yc_list[i] = Math.Round(-(Cl_i / (4 * Math.PI)) * ((1 - x_camber) * Math.Log(1 - x_camber) +
                    t * Math.Log(t)), 4);
                    yc_prime_list[i] = Math.Round((Cl_i / (4 * Math.PI)) * (Math.Log(1 - x_camber) - Math.Log(x_camber)), 4);
                }
                else if (a < 1)
                {
                    Double g = (-1 / (1 - a)) * (Math.Pow(a, 2) * (0.5 * Math.Log(a) - 0.25) + 0.25);
                    Double h = (1 - a) * (0.5 * Math.Log(1 - a) - 0.25) + g;

                    Double outside = Cl_i / (2 * Math.PI * (1 + a));

                    Double s = 0.5 * Math.Pow(a - x_camber, 2) * Math.Log(a - x_camber);
                    Double u = 0.5 * Math.Pow(1 - x_camber, 2) * Math.Log(1 - x_camber);
                    Double v = 0.25 * Math.Pow(1 - x_camber, 2);
                    Double w = 0.25 * Math.Pow(a - x_camber, 2);

                    Double first = (1 / (1 - a)) * (s - u + v - w);

                    Double second = x_camber * Math.Log(x_camber) + g - h * x_camber;

                    yc_list[i] = outside * (first - second);

                    Double third = (1 / (1 - a)) * ((1 - x_camber) * Math.Log(1 - x_camber) - (a - x_camber) * Math.Log(a - x_camber));
                    Double fourth = third - Math.Log(t) - 1 - h;

                    yc_prime_list[i] = outside * fourth;
                }


                    theta_list[i] = Math.Round(Math.Atan(yc_prime_list[i]) * 57.3, 2);
                xu_list[i] = Math.Round(xlist[i] - ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);
                xl_list[i] = Math.Round(xlist[i] + ylist[i] * Math.Sin(theta_list[i] * (Math.PI / 180)), 4);

                yu_list[i] = Math.Round(yc_list[i] + ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
                yl_list[i] = Math.Round(yc_list[i] - ylist[i] * Math.Cos(theta_list[i] * (Math.PI / 180)), 4);
            }
            listBox1.DataSource = xlist;
            listBox2.DataSource = ylist;
            listBox3.DataSource = yc_list;
            listBox4.DataSource = yc_prime_list;
            listBox5.DataSource = phi_list;
            listBox6.DataSource = theta_list;
            listBox7.DataSource = xu_list;
            listBox8.DataSource = yu_list;
            listBox9.DataSource = xl_list;
            listBox10.DataSource = yl_list;
        }

        private void txt_N_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                if (cmb_AircraftType.Text == "NACA Four-digit")
                {                    
                    Calculate4Digit();
                }
                else if (cmb_AircraftType.Text == "NACA Five-digit")
                {
                    Calculate5Digit();
                }
                else if (cmb_AircraftType.Text == "NACA Six-digit")
                {
                    Calculate6Digit();
                }
                LoadGraph();                
            }
            catch
            {
                return;
            }
        }

        private void LoadGraph()
        {
            var chart = chart1.ChartAreas[0];

            chart.AxisX.Minimum = 0.0;
            chart.AxisX.Maximum = 1.0;
            chart.AxisY.Minimum = -0.1;
            chart.AxisY.Maximum = 0.15;
            chart.AxisX.Interval = 0.1;
            chart.AxisY.Interval = 0.05;

            chart1.Series.Add("Airfoil");
            chart1.Series["Airfoil"].ChartType = SeriesChartType.Spline;
            chart1.Series["Airfoil"].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series.Add("Airfoil1");
            chart1.Series["Airfoil1"].ChartType = SeriesChartType.Spline;
            chart1.Series["Airfoil1"].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series.Add("Airfoil2");
            chart1.Series["Airfoil2"].ChartType = SeriesChartType.Spline;
            chart1.Series["Airfoil2"].Color = Color.Blue;
            //chart1.Series["Airfoil2"].
            chart1.Series[0].IsVisibleInLegend = false;

            for (int i = 0; i < N ;i++)
            {
                chart1.Series["Airfoil"].Points.AddXY(xu_list[i], yu_list[i]);
                chart1.Series["Airfoil1"].Points.AddXY(xl_list[i], yl_list[i]);
                chart1.Series["Airfoil2"].Points.AddXY(xl_list[i], yc_list[i]);
            }          
            
        }

        private void CalculateClc()
        {
            Double W1 = 0.995;
            Double W2 = 0.997;
            Double W3 = 0.998;
            Double W4 = 0.992;

            Double W_i = Wto * W1 * W2 * W3 * W4;

            Double Range = 1207;

            Double cp = 0.5;

            Double np = GAAircraftDesign.PropEffi;

            Double L_D = 12;

            Double W5 = 1/(Math.Exp(Range * cp / (L_D * 375 * np)));

            Double W_f = W_i * W5;

            Double W_ave = 0.5 * (W_i + W_f);

            Double Cl_c = 2 * W_ave / (0.00187*Math.Pow(Vc,2)*S_w);

            Double Cl_cw = Cl_c / 0.95;

            Double Cl_i = Cl_cw / 0.9;



            label21.Text = Cl_i.ToString();
        }

        /*
        private void LoadPlanformGraph()
        {
            Double TE = (c_rw - d - c_tw);
            var charts = chart2.ChartAreas[0];

            charts.AxisX.Minimum = -((b_w / 2) + 1);
            charts.AxisX.Maximum = (b_w / 2) + 1;
            charts.AxisY.Minimum = Math.Round(-TE - 4,2);
            charts.AxisY.Maximum = Math.Round(c_rw + 4,2);
            charts.AxisX.Interval = 2;
            charts.AxisY.Interval = 1;            

            chart2.Series.Add("Planform");
            chart2.Series["Planform"].ChartType = SeriesChartType.Line;
            chart2.Series["Planform"].Color = Color.Red;
            chart2.Series[0].IsVisibleInLegend = false;

            chart2.Series["Planform"].Points.AddXY(0, 0);
            chart2.Series["Planform"].Points.AddXY(0, c_rw);
            chart2.Series["Planform"].Points.AddXY(b_w/2, c_rw - d);
            chart2.Series["Planform"].Points.AddXY( b_w/2, c_rw - d - c_tw);
            chart2.Series["Planform"].Points.AddXY(0, 0);
            chart2.Series["Planform"].Points.AddXY(-(b_w / 2), (c_rw - d - c_tw));
            chart2.Series["Planform"].Points.AddXY(-(b_w / 2), (c_rw - d));
            chart2.Series["Planform"].Points.AddXY(0, c_rw);
        }
        */

            
        private void LoadPlanformGraph2()
        {
            try
            {
                cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Wing Planform",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0, 0),
                        new ObservablePoint(0, c_rw),
                        new ObservablePoint(b_w/2, c_rw - d),
                        new ObservablePoint(b_w/2, c_rw - d - c_tw),
                        new ObservablePoint(0, 0),
                        new ObservablePoint(-(b_w / 2), (c_rw - d - c_tw)),
                        new ObservablePoint(-(b_w / 2), (c_rw - d)),
                        new ObservablePoint(0, c_rw)                        
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
