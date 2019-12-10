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

namespace DECCA2
{
    public partial class SystemsFlapAdjustment : BaseWindow
    {
        Double S_w, W_to, W_f, W_i, V_c, Cl_i, V_s, Cf_C, Clmax, Cl_max_gross, AR, b_w, cr_w, tr_w, MAC, i_w, a_2d;

        private void txt_AR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                GetWingParameters();
                GetFlapParameters();
                LiftingLineFlap();
            }
            catch
            {
                return;
            }
        }

        int N;

        Double alpha0, alpha_twist, V_to, Cl_to, bf_b, delta_flap_TO, wing_TO_AOA, flap_AOA_0,Cl_Wing;

        Double Clmax_hld = 0;

        Double rho0 = AltitudeCalculator.Dens_Standard;

        public SystemsFlapAdjustment(Double Cli, Double Clmax_gross, Double Clmaxhld, Double Clmax1
            , int N1, Double Sw, Double AR1, Double trw, Double alphaZero, Double bw,
            Double MAC1, Double crw, Double alphatwist, Double Cfc, CheckBox fowler, CheckBox Double1,
            CheckBox Kruger, CheckBox LeadingFlap, CheckBox LeadingSlat, 
            CheckBox Plain, CheckBox Slotted, CheckBox Split, CheckBox Triple, Double Clwing, 
            Double Wto,Double Vs)
        {
            InitializeComponent();

            W_to = Wto;

            V_s = Vs;

            Cl_Wing = Clwing;
            txt_Cl_Wing.Text = Cl_Wing.ToString();

            Cf_C = Cfc;
            txt_Cf_C.Text = Cf_C.ToString();

            chk_Fowler.CheckState = fowler.CheckState;
            chk_Double.CheckState = Double1.CheckState;
            chk_Kruger.CheckState = Kruger.CheckState;
            chk_LeadingFlap.CheckState = LeadingFlap.CheckState;
            chk_LeadingSlat.CheckState = LeadingSlat.CheckState;
            chk_Plain.CheckState = Plain.CheckState;
            chk_Slotted.CheckState = Slotted.CheckState;
            chk_Split.CheckState = Split.CheckState;
            chk_Triple.CheckState = Triple.CheckState;

            cr_w = crw;
            txt_RootChord.Text = cr_w.ToString();

            alpha_twist = alphatwist;
            txt_Twist.Text = alpha_twist.ToString();

            N = N1;
            txt_Segments.Text = N.ToString();

            S_w = Sw;
            txt_Sw.Text = Sw.ToString();

            AR = AR1;
            txt_AR.Text = AR.ToString();

            tr_w = trw;
            txt_Taper.Text = tr_w.ToString();

            alpha0 = alphaZero;
            txt_Zerolift.Text = alpha0.ToString();

            b_w = bw;
            txt_WingSpan.Text = b_w.ToString();

            MAC = MAC1;
            txt_MAC.Text = MAC.ToString();

            Cl_i = Cli;
            txt_Cli.Text = Cl_i.ToString();

            Cl_max_gross = Clmax_gross;
            txt_Cl_max_gross.Text = Cl_max_gross.ToString();

            Clmax_hld = Clmaxhld;
            txt_Clmax_hld.Text = Clmax_hld.ToString();

            Clmax = Clmax1;
            txt_FlapUpClmax.Text = Clmax.ToString();
        }

        private void SystemsFlapAdjustment_Load(object sender, EventArgs e)
        {
            GetFlapParameters();
            GetWingParameters();
            //LiftingLineFlap();
        }

        private void GetWingParameters()
        {
            N = Int16.Parse(txt_Segments.Text);
            AR = Double.Parse(txt_AR.Text);           

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

        private void GetFlapParameters()
        {
            V_to = Math.Round(1.2 * V_s * 0.5144447, 0);
            txt_Vto.Text = V_to.ToString();

            Cl_to = Math.Round(2 * W_to * 9.81 / (rho0 * Math.Pow(V_to, 2) * S_w), 3);
            txt_Clto.Text = Cl_to.ToString();

            bf_b = Double.Parse(txt_Bf_b.Text);
            wing_TO_AOA = Double.Parse(txt_TOWingAOA.Text);
            delta_flap_TO = Double.Parse(txt_TOFlapDeflect.Text);

            flap_AOA_0 = -Cf_C * 1.15 * delta_flap_TO;

            txt_ZeroLftFlapAOA.Text = flap_AOA_0.ToString();
        }

        List<Double> LHS_f, mu_f, z_f, c_f, theta_set, res_f, Cl1_f, alpha0_set;
        Double[,] B_f;

        private void LiftingLineFlap()
        {
            LHS_f = new List<double>();
            mu_f = new List<double>();
            z_f = new List<double>();
            c_f = new List<double>();
            theta_set = new List<double>();
            res_f = new List<double>();

            B_f = new Double[N, N];

            for (Double theta = Math.PI / (2 * N); theta <= Math.PI / 2; theta += Math.PI / (2 * N))
            {
                theta_set.Add(theta);
                z_f.Add((b_w / 2) * Math.Cos(theta));
                Double c1 = cr_w * (1 - (1 - tr_w) * Math.Cos(theta));
                c_f.Add(c1);
                mu_f.Add(Math.Round(c1 * a_2d / (4 * b_w), 4));
            }

            listBox1.DataSource = c_f;

            int count = 0;

            Double a_0_fd = alpha0 + flap_AOA_0;

            alpha0_set = new List<double>();

            for (int i = 0; i < N; i++)
            {
                if (((i + 1) / Convert.ToDouble(N)) > (1 - bf_b))
                {

                    alpha0_set.Add(a_0_fd);
                }
                else
                {
                    alpha0_set.Add(alpha0);
                }
            }

            for (Double alpha = i_w + alpha_twist; alpha <= i_w; alpha -= alpha_twist / (N - 1))
            {
                LHS_f.Add(Math.Round(mu_f[count] * (alpha - alpha0_set[count]) / 57.3, 4));
                count++;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    B_f[i, j] = Math.Round(Math.Sin((2 * (j + 1) - 1) * theta_set[i]) * (1 + (mu_f[i] * (2 * (j + 1) - 1)) / Math.Sin(theta_set[i])), 4);
                }
            }

            MatrixSolver(B_f, LHS_f);

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

            Cl1_f = new List<double>();

            for (int i = 0; i < N; i++)
            {
                Cl1_f.Add(Math.Round(4 * b_w * sum2[i] / c_f[i], 4));
            }

            listBox2.DataSource = Cl1_f;

            LoadGraph();

            Double Cl_Wing_f = Math.Round(Math.PI * AR * result[0], 3);

            txt_Cl_to.Text = Cl_Wing_f.ToString();

        }

        IList<Double> result;
        LinearEquationSolver linear;

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

        private void LoadGraph()
        {
            var chart = chart1.ChartAreas[0];

            chart.AxisX.Minimum = 0.0;
            chart.AxisX.Maximum = b_w / 2;
            chart.AxisY.Minimum = 0;
            chart.AxisY.Maximum = Cl1_f[8] + 0.2;
            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 0.05;

            chart1.Series.Add("Lifting-Line_f");
            chart1.Series["Lifting-Line_f"].ChartType = SeriesChartType.Spline;
            chart1.Series["Lifting-Line_f"].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;

            chart1.Series["Lifting-Line_f"].Points.AddXY(b_w / 2, 0);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[0], Cl1_f[0]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[1], Cl1_f[1]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[2], Cl1_f[2]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[3], Cl1_f[3]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[4], Cl1_f[4]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[5], Cl1_f[5]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[6], Cl1_f[6]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[7], Cl1_f[7]);
            chart1.Series["Lifting-Line_f"].Points.AddXY(z_f[8], Cl1_f[8]);
        }
    }
}
