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
    public partial class RoskamWeightEst : Form
    {
        Double R, np, cp, cj, cj_atn, Endr, L_D,L_D_ltr, Vcr, Hcr, Vclimb,ROC, ClimbDistance;

        public Double Wto, Mff, W_total, We, Ratio,W_to;

        Double Mtfo;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAircft_Type.Text.Trim().Contains("Jet"))
                {
                    RoskamWingEstimateJet jet = new RoskamWingEstimateJet(cmbAircft_Type.Text, Double.Parse(txt_Wto.Text));
                    jet.ShowDialog();
                }
                else
                {
                    WingEstimateForm_form prop = new WingEstimateForm_form(cmbAircft_Type.Text, Wto);
                    prop.ShowDialog();
                }
            }
            catch
            {
                return;
            }
        }

        Double W1, W2, W3, W4, W5, W6, W7, W_f, A, B, C, C2, D, F, W_ltr, W_atn, R_atn, V_atn, L_D_atn;       

        Double Wto_Pl, Wto_We, Wto_Rng, Wto_cp, Wto_np, Wto_LD, Wto_Ecp, Wto_ELD, Wto_Endr, Wto_V, Mres;        

        List<Double> Ratio_lst;

        public Double W_pax = 175;
        Double W_bag = 33.333;

        //============================================================================

        private void LoadAircraftType()
        {
            cmbAircft_Type.Items.Add("Transport Jet");
            cmbAircft_Type.Items.Add("Business Jet");            
            cmbAircft_Type.Items.Add("GA - Single Engine");
            cmbAircft_Type.Items.Add("GA - Twin Engine");            
            cmbAircft_Type.Items.Add("Home-built");         
        }

        public RoskamWeightEst()
        {
            InitializeComponent();
        }

        private void Aircraft_Load(object sender, EventArgs e)
        {
            LoadAircraftType();
            LoadCombos.LoadDistanceCombos(cmb_Hcr, "ft");
            //LoadCombos.LoadDistanceCombos(cmb_H_atn, "ft");
            LoadCombos.LoadDistanceCombos(cmb_Range, "miles");
            LoadCombos.LoadDistanceCombos(cmb_Atndist, "nm");

            LoadCombos.LoadVelCombos(cmb_Vcr, "mach");
            LoadCombos.LoadVelCombos(cmb_ClimbSpeed, "knots");
            LoadCombos.LoadVelCombos(cmb_Vatn, "knots");
            LoadCombos.LoadVelCombos(cmb_ROC, "fpm");

            JetsDisabled();

            button1.Enabled = false;
        }

        //============================================================================

        public void  GetWeightRatios()
        {
            if (cmbAircft_Type.Text == "GA - Single Engine")
            {
                W1 = 0.995; //Engine Warm up
                W2 = 0.997; //Taxi
                W3 = 0.998; //Takeoff
                W4 = 0.992; //Climb    
                W6 = 0.993; //Descent
                W7 = 0.993; //Landing, Taxi, Shutdown

                A = -0.1440;
                B = 1.1162;

            }
            else if (cmbAircft_Type.Text == "GA - Twin Engine")
            {
                W1 = 0.992; //Engine Warm up
                W2 = 0.996; //Taxi
                W3 = 0.996; //Takeoff
                W4 = 0.990; //Climb    
                W6 = 0.992; //Descent
                W7 = 0.992; //Landing, Taxi, Shutdown

                A = 0.0966;
                B = 1.0298;
            }
            else if (cmbAircft_Type.Text == "Business Jet")
            {
                W1 = 0.990; //Engine Warm up
                W2 = 0.995; //Taxi
                W3 = 0.995; //Takeoff
                W4 = 0.980; //Climb    
                W6 = 0.990; //Descent
                W7 = 0.992; //Landing, Taxi, Shutdown

                A = 0.2678;
                B = 0.9979;
            }
            else if (cmbAircft_Type.Text == "Transport Jet")
            {
                W1 = 0.990; //Engine Warm up
                W2 = 0.990; //Taxi
                W3 = 0.995; //Takeoff
                W4 = 0.985; //Climb    
                W6 = 0.985; //Descent
                W7 = 0.995; //Landing, Taxi, Shutdown

                A = 0.0833;
                B = 1.0383;
            }
            else if (cmbAircft_Type.Text == "Home-built")
            {
                W1 = 0.998; //Engine Warm up
                W2 = 0.998; //Taxi
                W3 = 0.998; //Takeoff
                W4 = 0.995; //Climb    
                W6 = 0.995; //Descent
                W7 = 0.995; //Landing, Taxi, Shutdown

                A = 0.3411;
                B = 0.9519;
            }
        }

        public void CalculateJetWeight()
        {
            if (cmb_Range.Text == "nm" || cmb_Atndist.Text == "nm")
            {
                R = Double.Parse(txt_Range.Text);
            }
            else if (cmb_Range.Text == "ft" || cmb_Atndist.Text == "ft")
            {
                R = Double.Parse(txt_Range.Text) * 0.000164579;
            }
            else if (cmb_Range.Text == "m" || cmb_Atndist.Text == "m")
            {
                R = Double.Parse(txt_Range.Text) * 0.000539957349081371537;
            }
            else if (cmb_Range.Text == "km" || cmb_Atndist.Text == "km")
            {
                R = Double.Parse(txt_Range.Text) * 0.539957349081371537;
            }
            else if (cmb_Range.Text == "miles" || cmb_Atndist.Text == "miles")
            {
                R = Double.Parse(txt_Range.Text) * 0.86897712000001081645;
            }

            Hcr = Double.Parse(txt_Hcr.Text);

            Vclimb = Double.Parse(txt_ClimbSpeed.Text);

            ROC = Double.Parse(txt_ROC.Text);

            ClimbDistance = Math.Round((Hcr / ROC) * Vclimb / 60, 1);

            AltitudeCalculator.CalcAtmos(txt_Hcr.Text, cmb_Hcr);

            Vcr = Double.Parse(txt_Vcr.Text);

            W5 = Math.Round(1 / (Math.Exp((R - ClimbDistance) * cp / (L_D * AltitudeCalculator.a_h * Vcr * 1.943844))), 3);

            L_D_ltr = Double.Parse(txt_LD_ltr.Text);
            Endr = Double.Parse(txt_LtrTime.Text);
            cj = Double.Parse(txt_LtrFuelEff.Text);

            W_ltr = Math.Round(1 / Math.Exp(Endr * cj / L_D_ltr), 3);

            R_atn = Double.Parse(txt_Atndist.Text);
            L_D_atn = Double.Parse(txt_LDatn.Text);
            cj_atn = Double.Parse(txt_LtrFuelEff_atn.Text);
            V_atn = Double.Parse(txt_Vatn.Text);

            W_atn = Math.Round(1 / (Math.Exp(R_atn * cj_atn / (L_D_atn * V_atn))), 3);

            Mff = Math.Round(W1 * W2 * W3 * W4 * W5 * W6 * W7 * W_ltr * W_atn, 3);

            Mres = 0;

            W_f = (1 - Mff);

            C = 1 - (1 + Mres) * (1 - Mff) - Mtfo;

            C2 = Mff * (1 + Mres) - Mtfo - Mres;

            F = Math.Round(-B * Math.Pow(Wto, 2) * (1 + Mres) * Mff / (C * Wto * (1 - B) - D), 2);

            Wto_Rng = Math.Round(F * cp / (L_D * AltitudeCalculator.a_h * Vcr * 1.943844), 1);

            Wto_Endr = Math.Round(F * cp / L_D, 1);

            Wto_V = Math.Round(-F * R * cp / (Math.Pow(AltitudeCalculator.a_h * Vcr * 1.943844, 2) * L_D), 1);

            Wto_cp = Math.Round(F * R / (L_D * AltitudeCalculator.a_h * Vcr * 1.943844), 1);

            Wto_LD = Math.Round(-F * R * cp / (Math.Pow(L_D, 2) * AltitudeCalculator.a_h * Vcr * 1.943844), 1);

            Wto_Ecp = Math.Round(F * Endr / (L_D), 1);

            Wto_ELD = Math.Round(-F * Endr * cj / Math.Pow(L_D_ltr, 2), 1);
        }

        public void CalculatePropWeight()
        {
            if (cmb_Range.Text == "miles")
            {
                R = Double.Parse(txt_Range.Text);
            }
            else if (cmb_Range.Text == "ft")
            {
                R = Double.Parse(txt_Range.Text) * 0.000189394;
            }
            else if (cmb_Range.Text == "m")
            {
                R = Double.Parse(txt_Range.Text) * 0.0006213713910761;
            }
            else if (cmb_Range.Text == "km")
            {
                R = Double.Parse(txt_Range.Text) * 0.6213713910761;
            }

            W5 = Math.Round(1 / (Math.Exp(R * cp / (L_D * 375 * np))), 3);

            Mres = 0.25;

            Mff = W1 * W2 * W3 * W4 * W5 * W6 * W7;

            W_f = (1 + Mres) * (1 - Mff);

            C = 1 - (1 + Mres) * (1 - Mff) - Mtfo;

            C2 = Mff * (1 + Mres) - Mtfo - Mres;

            F = Math.Round(-B * Math.Pow(Wto, 2) * (1 + Mres) * Mff / (C * Wto * (1 - B) - D), 2);

            Wto_Rng = Math.Round(F * cp / (375 * np * L_D), 1);

            Wto_cp = Math.Round(F * R / (375 * np * L_D), 1);

            Wto_np = Math.Round(-F * R * cp / (375 * Math.Pow(np, 2) * L_D), 1);

            Wto_LD = Math.Round(-F * R * cp / (375 * Math.Pow(L_D, 2) * np), 1);
        }

        public void TakeoffWeightCal()
        {
            try
            {
                GetWeightRatios();

                Mtfo = 0.005;

                Ratio_lst = new List<double>();
                cp = Double.Parse(txt_FuelEff.Text);
                np = Double.Parse(txt_PropEff.Text);
                L_D = Double.Parse(txt_LD.Text);

                if (cmbAircft_Type.Text.Trim().Contains("Jet") || cmbAircft_Type.Text.Trim().Contains("Fighter"))
                {
                    CalculateJetWeight();
                }
                else
                {
                    CalculatePropWeight();
                }

                Double i = Double.Parse(txt_W_est_start.Text);

                W_total = (Double.Parse(txt_PaxNo.Text) + Double.Parse(txt_CrewNo.Text)) * (W_pax + W_bag);

                do
                {
                    Double W_oe_tent = i - W_f * i - W_total;
                    Double W_tent = W_oe_tent - Mtfo * i;
                    We = Math.Pow(10, (Math.Log10(i) - A) / B);
                    Ratio = Math.Abs(W_tent - We) / We;
                    Wto = i;
                    Ratio_lst.Add(Ratio);
                    i++;
                    if (Ratio < 0.0051)
                        break;
                } while (i < Double.Parse(txt_W_est_end.Text));

                W_to = Wto;

                D = W_total;

                Wto_Pl = Math.Round(B * Wto / (D - C * (1 - B) * Wto), 2);

                Wto_We = Math.Round(B * 126100 / (Math.Pow(10, ((Math.Log10(126100) - A) / B))), 2);

                txt_Wto_cp_cr.Text = Wto_cp.ToString();
                txt_Wto_cp_E.Text = Wto_Ecp.ToString();
                txt_Wto_Ld_cr.Text = Wto_LD.ToString();
                txt_Wto_LD_E.Text = Wto_ELD.ToString();
                txt_Wto_Pay.Text = Wto_Pl.ToString();
                txt_Wto_Range.Text = Wto_Rng.ToString();
                txt_Wto_V.Text = Wto_V.ToString();
                txt_Wto_We.Text = Wto_We.ToString();
                txt_Wto_np.Text = Wto_np.ToString();

                txt_Wto.Text = Wto.ToString();
                txt_W_e.Text = Math.Round(We, 2).ToString();
                txt_W_f.Text = Math.Round(Wto * W_f, 2).ToString();
                listBox1.DataSource = Ratio_lst;

                lblResult.Text = F.ToString();
                W_to = Double.Parse(txt_Wto.Text);
            }
            catch
            {
                return;
            }
        }       
        
        private void Clear()
        {
            listBox1.Items.Clear();
            txt_Atndist.Clear();
            txt_ClimbSpeed.Clear();
            txt_CrewNo.Clear();
            txt_FuelEff.Clear();
            txt_Hcr.Clear();
            txt_LD.Clear();
            txt_LDatn.Clear();
            txt_LD_ltr.Clear();
            txt_LtrFuelEff.Clear();
            txt_LtrFuelEff_atn.Clear();
            txt_LtrTime.Clear();
            txt_PaxNo.Clear();
            txt_PropEff.Clear();
            txt_Range.Clear();
            txt_ROC.Clear();
            txt_Vatn.Clear();
            txt_Vcr.Clear();
            txt_Wto.Clear();
            txt_Wto_cp_cr.Clear();
            txt_Wto_cp_E.Clear();
            txt_Wto_Ld_cr.Clear();
            txt_Wto_LD_E.Clear();
            txt_Wto_Pay.Clear();
            txt_Wto_Range.Clear();
            txt_Wto_V.Clear();
            txt_Wto_We.Clear();
            txt_W_e.Clear();
            txt_W_est_end.Clear();
            txt_W_est_start.Clear();
            txt_W_f.Clear();
        }

        private void JetsEnabled()
        {

            txt_Atndist.Enabled = true;
            txt_ClimbSpeed.Enabled = true;
            txt_CrewNo.Enabled = true;
            txt_FuelEff.Enabled = true;
            txt_Hcr.Enabled = true;
            txt_LD.Enabled = true;
            txt_LDatn.Enabled = true;
            txt_LD_ltr.Enabled = true;
            txt_LtrFuelEff.Enabled = true;
            txt_LtrFuelEff_atn.Enabled = true;
            txt_LtrTime.Enabled = true;
            txt_PaxNo.Enabled = true;
            txt_PropEff.Enabled = true;
            txt_Range.Enabled = true;
            txt_ROC.Enabled = true;
            txt_Vatn.Enabled = true;
            txt_Vcr.Enabled = true;
            txt_W_est_end.Enabled = true;
            txt_W_est_start.Enabled = true;

            cmb_Hcr.Enabled = true;
            cmb_ROC.Enabled = true;
            cmb_Vatn.Enabled = true;
            cmb_Vcr.Enabled = true;
            cmb_Atndist.Enabled = true;
            cmb_ClimbSpeed.Enabled = true;
        }

        private void JetsDisabled()
        {
            txt_Atndist.Enabled = false;
            txt_ClimbSpeed.Enabled = false;
            txt_CrewNo.Enabled = true;
            txt_FuelEff.Enabled = true;
            txt_Hcr.Enabled = false;
            txt_LD.Enabled = true;
            txt_LDatn.Enabled = false;
            txt_LD_ltr.Enabled = false;
            txt_LtrFuelEff.Enabled = false;
            txt_LtrFuelEff_atn.Enabled = false;
            txt_LtrTime.Enabled = false;
            txt_PaxNo.Enabled = true;
            txt_PropEff.Enabled = true;
            txt_Range.Enabled = true;
            txt_ROC.Enabled = false;
            txt_Vatn.Enabled = false;
            txt_Vcr.Enabled = false;
            txt_W_est_end.Enabled = true;
            txt_W_est_start.Enabled = true;

            cmb_Hcr.Enabled = false;
            cmb_ROC.Enabled = false;
            cmb_Vatn.Enabled = false;
            cmb_Vcr.Enabled = false;
            cmb_Atndist.Enabled = false;
            cmb_ClimbSpeed.Enabled = false;
        }

        //============================================================================

        private void cmbAircft_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            try
            {
                txt_Range_TextChanged(sender, e);

                if (cmbAircft_Type.Text.Trim().Contains("Jet") || cmbAircft_Type.Text.Trim().Contains("Fighter"))
                {
                    JetsEnabled();
                }
                else
                {
                    JetsDisabled();
                }
            }
            catch
            {
                return;
            }
        }

        private void txt_Range_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TakeoffWeightCal();
            }
            catch
            {
                return;
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
