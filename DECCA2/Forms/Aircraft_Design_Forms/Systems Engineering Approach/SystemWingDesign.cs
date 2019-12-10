using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Data.Text;
using ILNumerics;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;
using System.Windows.Forms.DataVisualization.Charting;

namespace DECCA2
{
    public partial class SystemWingDesign : BaseWindow
    {
        Double S_w, W_to, W_f,W_i,V_c,Cl_i,V_s,Cf_C,Clmax, Cl_max_gross, AR,b_w,cr_w,tr_w,MAC,i_w,a_2d;

        int N;

        Double alpha0, alpha_twist, Cl_Wing;

        Double Clmax_hld = 0;

        bool wake = true;

        Random rnd = new Random();

        Double rho0 = AltitudeCalculator.Dens_Standard; //* 0.00194122449;
        
        public SystemWingDesign(Double Wto, Double Sw, Double Wf, Double Wi,Double Vcrs,Double Vs)
        {
            InitializeComponent();

            Recog_speech = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            Recog_speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(MainEvent_Recognized);

            LoadGrammarandCommands();

            Recog_speech.SetInputToDefaultAudioDevice();

            Recog_speech.RecognizeAsync(RecognizeMode.Multiple);

            Decca.SelectVoice("Microsoft Zira Desktop");
            Decca.Rate = 0;

            W_to = Wto * 0.4535924;
            S_w = Math.Round(Sw * 0.092903,2);
            W_f = Wf * 0.4535924;
            W_i = Wi * 0.4535924;
            V_c = Vcrs * 0.3028;
            V_s = Vs;
        }

        private void txt_AR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                GetWingParameters();                
                LiftingLine();                
            }
            catch
            {
                return;
            }
        }

        //===============================================================================
        //Speech Recognition

        SpeechRecognitionEngine Recog_speech = null;
        SpeechSynthesizer Decca = new SpeechSynthesizer();              

        private void chk_Plain_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetFlapIncrement();
            }
            catch
            {
                return;
            }
        }

        private void pb_Exit_Click(object sender, EventArgs e)
        {
            //SystemsEngApproachAircraftDesign aircraftDesign = new SystemsEngApproachAircraftDesign();
            this.Close();
            //aircraftDesign.Show();
        }

        private void btn_horTail_Click(object sender, EventArgs e)
        {
            SystemHorizTail horizTail = new SystemHorizTail(W_to, V_c, S_w, Double.Parse(txt_AR.Text), Double.Parse(txt_Taper.Text)
               , Double.Parse(txt_WingSetting.Text), Double.Parse(txt_Twist.Text), Double.Parse(txt_MAC.Text), b_w,cr_w,MAC);

            //W_to,V_c,S_w,Double.Parse(txt_AR.Text), Double.Parse(txt_Taper.Text)
            ///, Double.Parse(txt_WingSetting.Text), Double.Parse(txt_Twist.Text),Double.Parse(txt_MAC.Text)

            horizTail.ShowDialog();
        }

        private void txt_Cf_C_TextChanged(object sender, EventArgs e)
        {
            chk_Double.Enabled = true;
            chk_Fowler.Enabled = true;
            chk_Plain.Enabled = true;
            chk_LeadingSlat.Enabled = true;
            chk_LeadingFlap.Enabled = true;
            chk_Kruger.Enabled = true;
            chk_Split.Enabled = true;
            chk_Triple.Enabled = true;
            chk_Slotted.Enabled = true;
        }

        SpeechRecognitionEngine listen = new SpeechRecognitionEngine();

        private void LoadGrammarandCommands()
        {
            try
            {
                string MachineName = Environment.UserName;

                string constring = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(constring);
                using (SqlCommand cmd = new SqlCommand("Select * FROM BaseCommands", con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var Loadcmd = sdr["Commands"].ToString();
                            Grammar commandgrammar = new Grammar(new GrammarBuilder(new Choices(Loadcmd)));
                            Recog_speech.LoadGrammarAsync(commandgrammar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SayAsync(string input)
        {
            Decca.SpeakAsync(input);
            wake = false;
        }

        private void MainEvent_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                string Name = "Trendon";
                string AI_name = "Decca";
                string speech = e.Result.Text;
                int ranNum;

                if (speech == "Hey Decca")
                {
                    wake = true;
                }
                if (speech == "stop talking")
                {
                    Decca.SpeakAsyncCancelAll();
                    ranNum = rnd.Next(1, 2);
                    if (ranNum == 1)
                    {
                        SayAsync("Yes sir");
                    }
                    if (ranNum == 2)
                    {
                        SayAsync("I am sorry I will be quiet");
                    }
                }

                if (wake == true)
                {
                    switch (speech)
                    {
                        case "what's up":
                            //Decca.SpeakAsync("hello" + Name);
                            System.DateTime timenow = System.DateTime.Now;
                            if (timenow.Hour >= 5 && timenow.Hour < 12)
                            {
                                SayAsync("Goodmorning" + Name);
                            }
                            if (timenow.Hour >= 12 && timenow.Hour < 18)
                            {
                                SayAsync("Good afternoon" + Name);
                            }
                            if (timenow.Hour >= 18 && timenow.Hour < 24)
                            {
                                SayAsync("Good evening" + Name);
                            }
                            if (timenow.Hour < 5)
                            {
                                SayAsync("Hello" + Name + ", you are still awake, you should go to sleep, it's getting late.");
                            }
                            break;
                        case "Maximize Window":
                        case "Maximize":
                            SayAsync("Maximizing window");
                            this.WindowState = FormWindowState.Maximized;
                            break;
                        case "Minimize Window":
                        case "Minimize":
                            SayAsync("Minimizing window");
                            this.WindowState = FormWindowState.Minimized;
                            break;
                        case "Restore Window":
                        case "Restore":
                            SayAsync("Restoring window");
                            this.WindowState = FormWindowState.Normal;
                            break;
                            /*
                        case "Close Program":
                        case "See you later Decca":
                        case "Bye Bye":
                        case "Later Decca":
                            System.DateTime timenow1 = System.DateTime.Now;
                            if (timenow1.Hour >= 5 && timenow1.Hour < 12)
                            {
                                Decca.Speak("Have a wonderful morning and spectacular day" + Name);
                            }
                            if (timenow1.Hour >= 12 && timenow1.Hour < 18)
                            {
                                Decca.Speak("Have a splendid afternoon" + Name);
                            }
                            if (timenow1.Hour >= 18 && timenow1.Hour < 24)
                            {
                                Decca.Speak("What a wonderful evening, do not forget to grab dinner" + Name);
                            }
                            if (timenow1.Hour < 5)
                            {
                                Decca.Speak(Name + ", you are still awake, you should go to sleep, it's getting late.");
                            }
                            Application.Exit();
                            break;
                            */
                    }
                }

            }
            catch
            {
                return;
            }
        }

        //*******************************************************************************

        private void SystemWingDesign_Load(object sender, EventArgs e)
        {
            CalculateClmaxs();
            txt_Clmax_hld.Text = Clmax_hld.ToString();           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemsFlapAdjustment flapAdjustment = new SystemsFlapAdjustment(Cl_i,Cl_max_gross,
                Clmax_hld,Clmax,N,S_w,AR,tr_w,alpha0,b_w,MAC,cr_w,alpha_twist,Cf_C,
                chk_Fowler,chk_Double,chk_Kruger,chk_LeadingFlap,chk_LeadingSlat,chk_Plain,chk_Slotted,
                chk_Split,chk_Triple,Cl_Wing, W_to,V_s);
            this.Close();
            flapAdjustment.ShowDialog();
        }

        private void CalculateClmaxs()
        {
            Double W_ave = (W_f + W_i) * 0.5;
            Double Cl_c = 2 * W_ave * 9.81 / (rho0*Math.Pow(V_c,2)*S_w);            

            Double Cl_cw = Cl_c / 0.95;
            Cl_i = Math.Round(Cl_cw / 0.9,3);
            txt_Cli.Text = Cl_i.ToString();

            Double Cl_max = 2 * W_to * 9.81/ (rho0 * Math.Pow(V_s*0.5144447, 2) * S_w);
            
            Double Cl_max_cw = Cl_max / 0.95;

            Cl_max_gross = Math.Round(Cl_max_cw / 0.9, 3); ;
            txt_Cl_max_gross .Text = Cl_max_gross.ToString();
        }

        private void CheckBoxIncrement(CheckBox check,Double increment)
        {
            if (check.Checked)
            {
                Clmax_hld += increment;
            }
            else if(!check.Checked)
            {
                increment = 0;
            }
            else
            {
                Clmax_hld -= increment;
            }
        }

        private void GetFlapIncrement()
        {
            Cf_C = Double.Parse(txt_Cf_C.Text);
            Clmax_hld = 0;

            CheckBoxIncrement(chk_Plain, 0.9);
            CheckBoxIncrement(chk_Split, 0.9);
            CheckBoxIncrement(chk_Fowler, 1.3);
            CheckBoxIncrement(chk_Slotted, 1.3 * Cf_C);
            CheckBoxIncrement(chk_Double, 1.6 * Cf_C);
            CheckBoxIncrement(chk_Triple, 1.9 * Cf_C);
            CheckBoxIncrement(chk_LeadingFlap, 0.3);
            CheckBoxIncrement(chk_LeadingSlat, 0.4);
            CheckBoxIncrement(chk_Kruger, 0.4);

            if (cmb_FlapDeflect.Text == "60" || cmb_FlapDeflect.Text == "")
            {
                Clmax_hld = Clmax_hld;
            }
            else if (cmb_FlapDeflect.Text == "30")
            {
                Clmax_hld = Clmax_hld/2;
            }

            txt_Clmax_hld.Text = Clmax_hld.ToString();

            Clmax = Math.Round(Cl_max_gross - Clmax_hld,4);

            txt_FlapUpClmax.Text = Clmax.ToString();
        }

        List<Double> LHS,mu,z,c,theta_set,res,Cl1;
        Double[,] B;
        
        LinearEquationSolver linear;        
        
        private void GetWingParameters()
        {
            N = Int16.Parse(txt_Segments.Text);
            AR = Double.Parse(txt_AR.Text);

            B = new Double[N, N];

            txt_Sw.Text = S_w.ToString();
            tr_w = Double.Parse(txt_Taper.Text);

            b_w = Math.Round(Math.Sqrt(AR * S_w), 4);
            txt_WingSpan.Text = b_w.ToString();

            MAC = Math.Round(S_w / b_w, 4);
            txt_MAC.Text = MAC.ToString();

            cr_w = Math.Round((1.5 * MAC * (1 + tr_w)) / (1 + tr_w + Math.Pow(tr_w, 2)), 4);
            txt_RootChord.Text = cr_w.ToString();

            i_w = Double.Parse(txt_WingSetting.Text);

            alpha0 = Double.Parse(txt_Zerolift.Text);

            a_2d = Double.Parse(txt_LiftCurve.Text);

            alpha_twist = Double.Parse(txt_Twist.Text);
        }        

        private void LiftingLine()
        {
            LHS = new List<double>();
            mu = new List<double>();
            z = new List<double>();
            c = new List<double>();
            theta_set = new List<double>();
            res = new List<double>();            
            
            for (Double theta = Math.PI/(2*N); theta <= Math.PI/2; theta += Math.PI / (2 * N))
            {
                theta_set.Add(theta);
                z.Add((b_w / 2) * Math.Cos(theta));
                Double c1 = cr_w * (1 - (1 - tr_w) * Math.Cos(theta));
                c.Add(c1);
                mu.Add(Math.Round(c1 * a_2d/(4 * b_w),4));
            }           

            int count = 0;
            for (Double alpha  = i_w + alpha_twist; alpha <= i_w; alpha -= alpha_twist/(N-1))
            {
                LHS.Add(Math.Round(mu[count]*(alpha - alpha0) / 57.3,4));
                count++;
            }

            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    B[i, j] = Math.Round(Math.Sin((2 * (j+1) - 1) * theta_set[i]) * (1 + (mu[i] * (2 * (j + 1) - 1))/ Math.Sin(theta_set[i])),4);                    
                }                
            }  
            
            MatrixSolver(B,LHS);
            
            Double[] sum1 = new Double[N];
            Double[] sum2 = new Double[N];

            for (int i = 0; i < N; i++)
            {
                sum1[i] = 0;
                sum2[i] = 0;
                for (int j = 0; j < N; j++)
                {
                    sum1[i] = sum1[i] + (2 * j - 1) * result[j] * Math.Sin((2 *(j+1) - 1) * theta_set[i]);
                    sum2[i] = Math.Round(sum2[i] + result[j] * Math.Sin((2 * (j+1) - 1) * theta_set[i]),4);
                }
            }

            listBox1.DataSource = sum2;

            Cl1 = new List<double>();

            for (int i = 0; i < N; i++)
            {
                Cl1.Add(Math.Round(4 * b_w * sum2[i] / c[i],4));
            }

            listBox1.DataSource = Cl1;

            LoadGraph();

            Cl_Wing = Math.Round(Math.PI * AR * result[0],3);

            txt_Cl_Wing.Text = Cl_Wing.ToString();

            Double a_c1 = 1 / (a_2d / 57.3);

            Double a_c2 = (W_i + W_f) *9.81/ (Math.Pow(V_c, 2) * S_w * 0.381);

            Double a_c = a_c1 * a_c2 + alpha0;

            label1.Text = a_c.ToString();
           
        }      

        IList<Double> result;
        
        private void MatrixSolver(Double[,] B,List<Double> LHS)
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

        private void LoadGraph()
        {
            var chart = chart1.ChartAreas[0];

            chart.AxisX.Minimum = 0.0;
            chart.AxisX.Maximum = b_w/2;
            chart.AxisY.Minimum = 0;
            chart.AxisY.Maximum = Cl1[8] + 0.2;
            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 0.05;

            chart1.Series.Add("Lifting-Line");
            chart1.Series["Lifting-Line"].ChartType = SeriesChartType.Spline;
            chart1.Series["Lifting-Line"].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series["Lifting-Line"].Points.AddXY(b_w/2, 0);
            chart1.Series["Lifting-Line"].Points.AddXY(z[0], Cl1[0]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[1], Cl1[1]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[2], Cl1[2]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[3], Cl1[3]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[4], Cl1[4]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[5], Cl1[5]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[6], Cl1[6]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[7], Cl1[7]);
            chart1.Series["Lifting-Line"].Points.AddXY(z[8], Cl1[8]);
        }
    }
}
