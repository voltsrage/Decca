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
    public partial class DeccaCd0Calculator : Form
    {
        SystemsEngApproachAircraftDesign aircraftDesign = new SystemsEngApproachAircraftDesign();

        Double V_max,h_int,CD0,W,S_w,P;

        public DeccaCd0Calculator()
        {
            InitializeComponent();
        }        

        private void DeccaCd0Calculator_Load(object sender, EventArgs e)
        {
            LoadCombos.LoadAircraftType(cmbAircft_Type);
            LoadCombos.LoadVelCombos(cmb_VelMax,"knots");
            LoadCombos.LoadDistanceCombos(cmb_Hcr, "feet");
            LoadCombos.LoadWeightCombos(cmb_Mto1,"lbs");
            LoadCombos.LoadAreaCombos(cmb_Sw, "ft^2");
            LoadCombos.LoadPower(cmb_Tsl, "N");

            EngineNoLoad();

            txt_Prop_eff1.Enabled = false;
        }

        private void EngineNoLoad()
        {
            cmb_EngineNo.Text = "2";
            cmb_EngineNo.Items.Add("1");
            cmb_EngineNo.Items.Add("2");
            cmb_EngineNo.Items.Add("3");
            cmb_EngineNo.Items.Add("4");
        }

        private void cmbAircft_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
            {
                lbl_Tsl.Text = "T_sl:";
                txt_Prop_eff1.Enabled = false;
            }
            else
            {
                lbl_Tsl.Text = "P_sl:";
                txt_Prop_eff1.Enabled = true;
            }
        }

        private void CalculateCD0()
        {
            
            CalculateVel();
            CalculateWeight();
            CalculateArea();            
            Double K = Math.Round(1 / (Double.Parse(txt_AR_1.Text)*Math.PI*Double.Parse(txt_e_1.Text)),3);
            Double bottom = aircraftDesign.Dens_Standard * S_w *Math.Pow(V_max,2);
            Double bottom1 = aircraftDesign.rho_h * (aircraftDesign.rho_h/1.225) * S_w * Math.Pow(V_max, 2);
            CalculatePower();
            Double topright = (4 * K * Math.Pow(W, 2)) / bottom1;            
            if (cmbAircft_Type.Text.Contains("Jet") || cmbAircft_Type.Text.Contains("Fighter"))
            {
                CD0 = Math.Round(((2 * Double.Parse(cmb_EngineNo.Text) * P - topright) / bottom),3);
            }
            else
            {
                CD0 = Math.Round((((2 * Double.Parse(cmb_EngineNo.Text) * Double.Parse(txt_Prop_eff1.Text) * P) - topright) / bottom),3);
            }
            txt_CDO1.Text = CD0 .ToString();
        }       

        private void CalculateVel()
        {
            if(cmb_Hcr.Text == "m")
            {
                h_int = Double.Parse(txt_H_cr1.Text);
            }
            else if (cmb_Hcr.Text == "feet")
            {
                h_int = (Double.Parse(txt_H_cr1.Text) * 0.3048);
            }

            aircraftDesign.CalcAtmos(h_int.ToString());
            if (cmb_VelMax.Text == "mach")
            {
                V_max = Double.Parse(txt_Vel_1.Text) * aircraftDesign.a_h;
            }
            else if (cmb_VelMax.Text == "m/s")
            {
                V_max = Double.Parse(txt_Vel_1.Text);
            }
            else if (cmb_VelMax.Text == "ft/s")
            {
                V_max = Double.Parse(txt_Vel_1.Text) * 0.3048;
            }
            else if (cmb_VelMax.Text == "knots")
            {
                V_max = Double.Parse(txt_Vel_1.Text) * 0.514444;
            }
            //V_max = Math.Round(V_max,0);
        }

        private void CalculatePower()
        {
            if(cmb_Tsl.Text == "N")
            {
                P = Double.Parse(txt_T_sl_1.Text);
            }
            else if (cmb_Tsl.Text == "lbs")
            {
                P = Double.Parse(txt_T_sl_1.Text) * 4.44822;
            }
            else if (cmb_Tsl.Text == "hp")
            {
                P = Double.Parse(txt_T_sl_1.Text) * 745.7/V_max;
            }
            else if (cmb_Tsl.Text == "kW")
            {
                P = Double.Parse(txt_T_sl_1.Text) * 1000 / V_max;
            }
        }

        private void CalculateWeight()
        {
            if (cmb_Mto1.Text == "N")
            {
                W = Double.Parse(txt_M_to_1.Text);
            }
            else if (cmb_Mto1.Text == "kg")
            {
                W = Double.Parse(txt_M_to_1.Text)*aircraftDesign.g;
            }
            else if (cmb_Mto1.Text == "lbs")
            {
                W = Double.Parse(txt_M_to_1.Text) * 4.44822;
            }
        }

        private void CalculateArea()
        {
            if(cmb_Sw.Text == "m^2")
            {
                S_w = Double.Parse(txt_Sw_1.Text);
            }
            else if (cmb_Sw.Text == "ft^2")
            {
                S_w = Double.Parse(txt_Sw_1.Text) * 0.09290304;
            }
        }

        private void txt_e_1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateCD0();
            }
            catch
            {
                return;
            }
        }       
    }
}
