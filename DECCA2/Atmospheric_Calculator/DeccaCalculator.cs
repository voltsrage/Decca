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
    public partial class DeccaCalculator : Form
    {
        bool enter_value = false;

        Double iFahrenheit, iCelcius, iKelvin;

        public Double Pres_Standard = 101325; // Sea Level Pressure (N/m2)

        public Double Temp_Standard = 288.15; // Sea Level Temperature  (K)

        Double Vis_Standard = 1.7893E-10; // Sea Level Viscosity (m/s)

        public Double Dens_Standard = 1.225; // Sea Level Density (kg/m3)        

        public Double R = 287.05287; // Gas Constant (J/(kgK)

        public Double gamma = 1.4; // Ratio of Specific Heats

        public Double a = 0.0065;

        public Double g = 9.80665; //Gravity

        public Double r_e = 6356766; //Radius of the earth

        public Double P_h, T, rho_h, rel_rho_h, a_h,g_h,m_h,q_c, qc_p0, qc_p,a0,h_int,V_int;

        Double Vtas, Vcas, Veas;

        String con;
        

        public DeccaCalculator()
        {
            InitializeComponent();
        }

        public double CalcAtmos(string h)
        {
            Double A_Standard = Math.Sqrt(gamma * R * Temp_Standard);
            try
            {
                h_int = Convert.ToDouble(h);
                if (cmbHeight.Text == "m")
                {
                    h_int = h_int;
                }
                else if (cmbHeight.Text == "ft")
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
                g_h = Math.Round(g/(Math.Pow(((r_e + h_int)/r_e),2)),3);
                Double g_11km = Math.Round(g / (Math.Pow(((r_e + 11000) / r_e), 2)), 3);
                Double g_20km = Math.Round(g / (Math.Pow(((r_e + 20000) / r_e), 2)), 3);
                Double g_47km = Math.Round(g / (Math.Pow(((r_e + 47000) / r_e), 2)), 3);

                //===================================================================================

                //Atmospheric Conditions at Tropopause (@ 11km)
                Double P_11km = Pres_Standard * Math.Pow((T_11km_20km/ Temp_Standard), (g_11km / (a * R)));
                Double rho_11km = P_11km / (R * T_11km_20km);
                Double rel_rho_11km = rho_11km / Dens_Standard;

                Double P_47km = Math.Round(2488 * Math.Pow((T_47km / T_11km_20km), -11.388), 3);

                if (h_int <= 11000)
                {
                    T = T_tps;
                    P_h = Math.Round(Pres_Standard * Math.Pow((T_tps / Temp_Standard), (g_h / (a * R))), 3);
                    rho_h = Math.Round((P_h/(R*T_tps)), 3);
                    rel_rho_h = Math.Round((rho_h/Dens_Standard), 3);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T),3);
                }
                else if (h_int <= 25000)
                {
                    Double h0 = 11000;
                    T = T_11km_20km;
                    P_h = Math.Round(P_11km*Math.Exp(-(g_h / (R * T)) * (h_int - h0)),3);
                    rho_h = Math.Round((P_h / (R * T)),3);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard),3);
                    a_h = Math.Round(Math.Sqrt(gamma * R * T), 3);
                }
                else if (h_int < 47000)
                {
                    T = T_20km_47km;
                    //P_h = Math.Round(P_20km * Math.Pow((T/T_11km_20km),-(g_h / (0.001*R))), 2);
                    P_h = Math.Round(2488 * Math.Pow((T_20km_47km / T_11km_20km), -11.388),3);
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
                txt_AtmosTemp.Text = Math.Round(T,3).ToString();
                txt_AtmosPres.Text = Math.Round(P_h,3).ToString();
                txt_AtmosDens.Text = Math.Round(rho_h,5).ToString();
                txt_AtmosRelDens.Text = Math.Round(rel_rho_h,3).ToString();
                txt_AtmosSound.Text = Math.Round(a_h,3).ToString();
                txt_AtmosGrav.Text = Math.Round(g_h,3).ToString();

                
            }
            catch(Exception ex)
            {
                AtmosClear();                   
            }
            return a_h;
        }

        private void AtmosClear()
        {
            txt_AtmosTemp.Clear();
            txt_AtmosPres.Clear();
            txt_AtmosRelDens.Clear();
            txt_AtmosDens.Clear();
            txt_AtmosSound.Clear();
            txt_AtmosGrav.Clear();
        }

        private void DeccaCalculator_Load(object sender, EventArgs e)
        {
            treeView1.Visible = false;
            DisableAtmosCond();
            pnl_Atmos.Visible = true;
            pnl_Height.Visible = true;

            cmbHeight.Text = "m";
            cmbHeight.Items.Add("m");
            cmbHeight.Items.Add("ft");
        }

        private void LoadComboBoxes()
        {
            if (lbl_Title.Text == "Temperature")
            {
                cmbInput.Text = ("Fahrenheit");
                cmbInput.Items.Add("Celcius");
                cmbInput.Items.Add("Fahrenheit");
                cmbInput.Items.Add("Kelvin");

                cmbOutput.Text = ("Celcius");
                cmbOutput.Items.Add("Celcius");
                cmbOutput.Items.Add("Fahrenheit");
                cmbOutput.Items.Add("Kelvin");
                   
            }
            else if (lbl_Title.Text == "Atmospheric" || lbl_Title.Text == "Velocity")
            {
                cmbAtmosTemp.Text = ("Kelvin");
                cmbAtmosTemp.Items.Add("Kelvin");
                cmbAtmosTemp.Items.Add("Celcius");
                cmbAtmosTemp.Items.Add("Fahrenheit");               

                cmbAtmosPres.Text = ("N/m2");
                cmbAtmosPres.Items.Add("N/m2");
                cmbAtmosPres.Items.Add("lb/in2");
                cmbAtmosPres.Items.Add("atm");
                cmbAtmosPres.Items.Add("lb/ft2");

                cmbAtmosDensity.Text = ("kg/m3");
                cmbAtmosDensity.Items.Add("kg/m3");
                cmbAtmosDensity.Items.Add("lb/in3");
                cmbAtmosDensity.Items.Add("lb/ft3");
                cmbAtmosDensity.Items.Add("slug/ft3");

                cmbAtmosSound.Text = ("m/s");
                cmbAtmosSound.Items.Add("m/s");
                cmbAtmosSound.Items.Add("km/h");
                cmbAtmosSound.Items.Add("fps");
                cmbAtmosSound.Items.Add("fpm");
                cmbAtmosSound.Items.Add("knots");

                cmbAtmosGrav.Text = ("m/s2");
                cmbAtmosGrav.Items.Add("m/s2");
                cmbAtmosGrav.Items.Add("ft/s2");

                LoadVelCombos(cmbVelUnits);
                LoadVelCombos(cmb_Vcas);
                LoadVelCombos(cmb_Vtas);
                LoadVelCombos(cmb_Veas);
            }

            else if (lbl_Title.Text == "Velocity" || lbl_Title.Text == "Atmospheric")
            {
                LoadVelCombos(cmbVelUnits);
                LoadVelCombos(cmb_Vcas);
                LoadVelCombos(cmb_Vtas);
                LoadVelCombos(cmb_Veas);
            }            
        }

        private void LoadVelCombos(ComboBox combo)
        {
            combo.Text = "m/s";
            combo.Items.Add("m/s");
            combo.Items.Add("fps");
            combo.Items.Add("knots");
            combo.Items.Add("km/h");
        }

        private void LoadWeightCombos(ComboBox combo)
        {
            combo.Text = "lbs";
            combo.Items.Add("lbs");
            combo.Items.Add("kg");
            combo.Items.Add("N");            
        }

        private void Clear()
        {
            lbl_InputDisplay.Text = "";
            lbl_Convert.Text = "";
            lbl_RemainTemp1.Text = "";
            lbl_RemainTemp2.Text = "";
            txt_Height.Clear();
            AtmosClear();
        }

        private void Atmos_Unit_Change(object sender, EventArgs e)
        {
            //Double kel, pas;

            //kel = Double.Parse(txt_AtmosTemp.Text);
            //pas = double.Parse(txt_AtmosPres.Text);

            //Temperature
            if(cmbAtmosTemp.Text == "Kelvin")
            {
                txt_AtmosTemp.Text = (T*1).ToString();
            }
            else if (cmbAtmosTemp.Text == "Celcius")
            {
                txt_AtmosTemp.Text = Math.Round((T - 273.15),2).ToString();
            }
            else if (cmbAtmosTemp.Text == "Fahrenheit")
            {
                txt_AtmosTemp.Text = (Math.Round((((9 * (T - 273.15)) / 5) + 32), 2).ToString()).ToString();
            }

            //Pressure
            if (cmbAtmosPres.Text == "N/m2")
            {
                txt_AtmosPres.Text = (P_h * 1).ToString();
            }
            else if (cmbAtmosPres.Text == "lb/ft2")
            {
                txt_AtmosPres.Text = Math.Round((P_h / 47.880172), 2).ToString();
            }
            else if (cmbAtmosPres.Text == "atm")
            {
                txt_AtmosPres.Text = Math.Round((P_h / 101325), 2).ToString();
            }
            else if (cmbAtmosPres.Text == "lb/in2")
            {
                txt_AtmosPres.Text = Math.Round((P_h / 6894.75728), 2).ToString();
            }

            //Density
            if (cmbAtmosDensity.Text == "kg/m3")
            {
                txt_AtmosDens.Text = (rho_h * 1).ToString();
            }
            else if (cmbAtmosDensity.Text == "lb/in3")
            {
                txt_AtmosDens.Text = Math.Round((rho_h/3.05119E+4), 5).ToString();
            }
            else if (cmbAtmosDensity.Text == "lb/ft3")
            {
                txt_AtmosDens.Text = Math.Round((rho_h / 17.65733336), 5).ToString();
            }
            else if (cmbAtmosDensity.Text == "slug/ft3")
            {
                txt_AtmosDens.Text = Math.Round((rho_h * 0.00194), 5).ToString();
            }

            //Speed
            if (cmbAtmosSound.Text == "m/s")
            {
                txt_AtmosSound.Text = (a_h * 1).ToString();
            }
            else if (cmbAtmosSound.Text == "km/h")
            {
                txt_AtmosSound.Text = Math.Round((a_h * 3.6 ), 2).ToString();
            }
            else if (cmbAtmosSound.Text == "fps")
            {
                txt_AtmosSound.Text = Math.Round((a_h / 0.3048), 2).ToString();
            }
            else if (cmbAtmosSound.Text == "fpm")
            {
                txt_AtmosSound.Text = Math.Round((a_h * 196.85), 2).ToString();
            }
            else if (cmbAtmosSound.Text == "knots")
            {
                txt_AtmosSound.Text = Math.Round((a_h / 0.514444), 2).ToString();
            }

            //GRavity
            if (cmbAtmosGrav.Text == "m/s2")
            {
                txt_AtmosGrav.Text = (g_h * 1).ToString();
            }
            else if (cmbAtmosGrav.Text == "ft/s2")
            {
                txt_AtmosGrav.Text = Math.Round((g_h / 0.3048), 2).ToString();
            }

        }

        private void VelUnitChange(ComboBox combo,TextBox text)
        {
            if(combo.Text == "m/s")
            {
                text.Text = "";
            }
        }

        private void EnableAtmosCond()
        {
            cmbAtmosTemp.Enabled = true;
            cmbAtmosSound.Enabled = true;
            cmbAtmosPres.Enabled = true;
            cmbAtmosDensity.Enabled = true;
            cmbAtmosGrav.Enabled = true;              
        }

        private void DisableAtmosCond()
        {
            cmbAtmosTemp.Enabled = false;
            cmbAtmosSound.Enabled = false;
            cmbAtmosPres.Enabled = false;
            cmbAtmosDensity.Enabled = false;
            cmbAtmosGrav.Enabled = false;           
        }
        
        private void cmbVelUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Vcas.Text = cmbVelUnits.Text;
            cmb_Veas.Text = cmbVelUnits.Text;
            cmb_Vtas.Text = cmbVelUnits.Text;           
        }

        private void button13_Click(object sender, EventArgs e)
        {            
            V_int = Double.Parse(txt_SpeedVtas.Text);
            CalculateVelByUnit(cmb_Vtas);

            txt_MachNumber.Text = Math.Round((V_int / a_h), 2).ToString();
            txt_SpeedVeas.Text = Math.Round((V_int * Math.Sqrt(rel_rho_h)), 2).ToString();
            GetDynamicPressureRatio(txt_MachNumber.Text);
            txt_SpeedVcas.Text = Math.Round(Double.Parse(txt_SpeedVeas.Text) * Double.Parse(con), 2).ToString();
            cmb_Veas_SelectedIndexChanged(sender, e);
            cmb_Vcas_SelectedIndexChanged(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            V_int = Double.Parse(txt_SpeedVeas.Text);
            CalculateVelByUnit(cmb_Veas);
            txt_SpeedVtas.Text = Math.Round((V_int / Math.Sqrt(rel_rho_h)), 2).ToString();
            txt_MachNumber.Text = Math.Round((Double.Parse(txt_SpeedVtas.Text) / a_h), 2).ToString();
            GetDynamicPressureRatio(txt_MachNumber.Text);
            txt_SpeedVcas.Text = Math.Round(V_int * Double.Parse(con), 2).ToString();
            cmb_Vtas_SelectedIndexChanged(sender, e);
            cmb_Vcas_SelectedIndexChanged(sender, e);
        }

        private void cmbHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcAtmos(txt_Height.Text);
        }

        private void cmb_Vtas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowVelConversion(cmb_Vtas, txt_SpeedVtas,1);
        }

        private void cmb_Vcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDynamicPressureRatio(txt_MachNumber.Text);

            ShowVelConversion(cmb_Vcas, txt_SpeedVcas,Double.Parse(con)* Math.Sqrt(rel_rho_h));
        }

        private void cmb_Veas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowVelConversion(cmb_Veas, txt_SpeedVeas,Math.Sqrt(rel_rho_h));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //Pressure Ratio            

            Double p_ratio = P_h / Pres_Standard;
            if (h_int > (36000 * 0.3048))
            {
                Double delta = (0.2233609) * Math.Exp(4.8063461E-5 * (36089.239 - h_int / .3048));
                GetMachFromVcas(delta);
            }
            else if (h_int <= (36000 * 0.3048))
            {
                Double delta = Math.Pow(1 - 6.8755857E-6 * (h_int / 0.3048), 5.255880);               
                GetMachFromVcas(delta);
            }
            cmb_Veas_SelectedIndexChanged(sender, e);
            cmb_Vtas_SelectedIndexChanged(sender, e);
        }        

        private void button10_Click(object sender, EventArgs e)
        {
            GetDynamicPressureRatio(txt_MachNumber.Text);
            txt_SpeedVtas.Text = Math.Round((m_h * a_h), 2).ToString();
            txt_SpeedVeas.Text = Math.Round((Double.Parse(txt_SpeedVtas.Text) * Math.Sqrt(rel_rho_h)), 2).ToString();
            txt_SpeedVcas.Text = Math.Round(Double.Parse(txt_SpeedVeas.Text) * Math.Sqrt(Pres_Standard / P_h) * Math.Sqrt((qc_p0 - 1) / (qc_p - 1)), 2).ToString();
            cmb_Vtas_SelectedIndexChanged(sender, e);
            cmb_Veas_SelectedIndexChanged(sender, e);
            cmb_Vcas_SelectedIndexChanged(sender, e);
        }        

        private void CalculateVelByUnit(ComboBox combo)
        {
            if (combo.Text == "m/s")
            {
                V_int = V_int;
            }
            else if (combo.Text == "fps")
            {
                V_int = V_int * 0.3048;
            }
            else if (combo.Text == "knots")
            {
                V_int = V_int * 0.514444;
            }
            else if (combo.Text == "km/h")
            {
                V_int = V_int / 3.6;
            }
        }

        private void ShowVelConversion(ComboBox combo,TextBox textBox,double k)
        {
            Double mach_conv = Double.Parse(txt_MachNumber.Text);

            if (combo.Text == "m/s")
            {
                textBox.Text = Math.Round((mach_conv*a_h*k),2).ToString();
            }
            else if (combo.Text == "fps")
            {
                textBox.Text = Math.Round(((mach_conv * a_h * k) / 0.3048),2).ToString();
            }
            else if (combo.Text == "knots")
            {
                textBox.Text = Math.Round(((mach_conv * a_h * k) / 0.51444),2).ToString();                
            }
            else if (combo.Text == "km/h")
            {
                textBox.Text = Math.Round(((mach_conv * a_h * k) * 3.6),2).ToString();
            }
        }

        private void btn_CE_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
        private void txt_MachNumber_TextChanged(object sender, EventArgs e)
        {
            if (txt_Height.Text == "" || txt_MachNumber.Text == "")
            {
                txt_SpeedVtas.Clear();
                txt_SpeedVeas.Clear();
                txt_SpeedVcas.Clear();
                txt_MachNumber.Clear();
            }
            else
            {
                GetDynamicPressureRatio(txt_MachNumber.Text);
            }            
        }

        private void GetMachFromVcas(double delta)
        {
            V_int = Double.Parse(txt_SpeedVcas.Text);
            CalculateVelByUnit(cmb_Vcas);

            Double m1 = Math.Pow(V_int / a0, 2);
            Double qc_vcas1 = Math.Pow((1 + 0.2 * m1), 3.5);
            Double qc_vcas = Pres_Standard * (qc_vcas1 - 1);

            Double pt_pa = ((Pres_Standard * delta) + qc_vcas) / (Pres_Standard * delta);

            Double mach_inner = Math.Pow(pt_pa, (1 / 3.5)) - 1;

            Double mach = Math.Sqrt(5 * mach_inner);

            txt_MachNumber.Text = Math.Round(mach, 2).ToString();

            txt_SpeedVtas.Text = Math.Round(mach * a_h, 2).ToString();

            txt_SpeedVeas.Text = Math.Round((mach * a_h) * Math.Sqrt(rel_rho_h), 2).ToString();
        }

        private void GetDynamicPressureRatio(string mach)
        {
            m_h = Double.Parse(mach);
            q_c = P_h * (Math.Pow(1 + 0.2 * Math.Pow(m_h, 2), 3.5) - 1);
            qc_p0 = Math.Pow(((q_c / Pres_Standard) + 1), 0.2857);
            qc_p = Math.Pow(((q_c / P_h) + 1), 0.2857);
            con = (Math.Round(Math.Sqrt(Pres_Standard / P_h) * Math.Sqrt((qc_p0 - 1) / (qc_p - 1)), 2)).ToString();
        }      

        private void ReturnLabel()
        {
            Vel_lblVcas.Text = "V_cas";
            Vel_lblVeas.Text = "V_eas";
            Vel_lblVtas.Text = "V_tas";
        }

        private void cmbInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void lbl_ToggleTreeView_Click(object sender, EventArgs e)
        {
            treeView1.Visible = true;
            pnl_Atmos.Visible = false;
            pnl_Height.Visible = false;
        }

        private void txt_Height_TextChanged(object sender, EventArgs e)
        {
            if (txt_Height.Text == "")
            {
                DisableAtmosCond();
                AtmosClear();
                lbl_Alt.Text = "";
            }
            else
            {                
                CalcAtmos(txt_Height.Text);
                EnableAtmosCond();
                Atmos_Unit_Change(sender, e);                
                txt_MachNumber_TextChanged(sender, e);

                try
                {
                    if (cmbHeight.Text == "m")
                    {
                        lbl_Alt.Text = "Approximately: " + (Math.Round((Double.Parse(txt_Height.Text) * 3.28084), 2)).ToString() + " ft";
                    }
                    else if (cmbHeight.Text == "ft")
                    {
                        lbl_Alt.Text = "Approximately: " + (Math.Round((Double.Parse(txt_Height.Text) / 3.28084), 2)).ToString() + " m,";
                    }
                }
                catch
                {
                    return;
                }
                
            }            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node .Name == "NodeCal")
            {
                lbl_Title.Text = "Temperature";
                treeView1.Visible = false;
                LoadComboBoxes();
            }
            if (e.Node.Name == "NodeSci")
            {
                lbl_Title.Text = "Scientific";
                treeView1.Visible = false;
            }
            if (e.Node.Name == "NodeAtmos")
            {
                lbl_Title.Text = "Atmospheric";
                LoadComboBoxes();
                pnl_Atmos.Visible = true;
                pnl_Height.Visible = true;
                treeView1.Visible = false;
            }
            if (e.Node.Name == "NodeStand")
            {
                lbl_Title.Text = "Standard";
                treeView1.Visible = false;
            }
            if (e.Node.Name == "NodeConv")
            {
                lbl_Title.Text = "Velocity";
                LoadComboBoxes();
                pnl_Height.Visible = true;
                treeView1.Visible = false;
            }
        }

        private void Num_Click(object sender, EventArgs e)
        {

            if (txt_Height.Text == "0" || enter_value)
                txt_Height.Text = "";

            enter_value = false;

            Button TempButton = (Button)sender;
            if (TempButton.Text == ".")
            {
                if (!txt_Height.Text.Contains("."))
                    txt_Height.Text = txt_Height.Text + TempButton.Text;
            }
            else
                txt_Height.Text = txt_Height.Text + TempButton.Text;

            if (lbl_InputDisplay.Text == "0" || enter_value)            
                lbl_InputDisplay.Text = "";                
            
            enter_value = false;

            Button TempButton1 = (Button)sender;
            if (TempButton.Text == ".")
            {
                if(!lbl_InputDisplay.Text.Contains("."))                
                    lbl_InputDisplay.Text = lbl_InputDisplay.Text + TempButton1.Text;                
            }            
            else         
                lbl_InputDisplay.Text = lbl_InputDisplay.Text + TempButton1.Text;

            //==========================================================================================================
            CalTemp();
            //==========================================================================================================

            //==========================================================================================================

        }

        private void CalTemp()
        {
            if (cmbInput.Text == cmbOutput.Text)
            {
                lbl_Convert.Text = lbl_InputDisplay.Text;
                if (cmbInput.Text == "Celcius")
                {
                    iCelcius = Double.Parse(lbl_InputDisplay.Text);
                    lbl_RemainTemp1.Text = (Math.Round((iCelcius + 273.15), 2).ToString()) + " K";
                    lbl_RemainTemp2.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString()) + " F";
                }
                if (cmbInput.Text == "Kelvin")
                {
                    iKelvin = Double.Parse(lbl_InputDisplay.Text);
                    lbl_RemainTemp1.Text = (Math.Round((iKelvin - 273.15), 2).ToString()) + "C";
                    iCelcius = iKelvin - 273.15;
                    lbl_RemainTemp2.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString()) + " F";
                }
                if (cmbInput.Text == "Fahrenheit")
                {
                    iFahrenheit = Double.Parse(lbl_InputDisplay.Text);
                    lbl_RemainTemp1.Text = (Math.Round(((((iFahrenheit - 32) * 5) / 9) + 273.15), 2).ToString()) + " K";
                    lbl_RemainTemp2.Text = (Math.Round((((iFahrenheit - 32) * 5) / 9), 2).ToString()) + " C";
                }
            }
            else if (cmbInput.Text == "Celcius" && cmbOutput.Text == "Fahrenheit")
            {
                iCelcius = Double.Parse(lbl_InputDisplay.Text);
                lbl_Convert.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString());
                lbl_RemainTemp1.Text = (Math.Round((iCelcius + 273.15), 2).ToString()) + " K";
            }
            else if (cmbInput.Text == "Fahrenheit" && cmbOutput.Text == "Celcius")
            {
                iFahrenheit = Double.Parse(lbl_InputDisplay.Text);
                lbl_Convert.Text = (Math.Round((((iFahrenheit - 32) * 5) / 9), 2).ToString());
                lbl_RemainTemp1.Text = (Math.Round(((((iFahrenheit - 32) * 5) / 9) + 273.15), 2).ToString()) + " K";
            }
            else if (cmbInput.Text == "Fahrenheit" && cmbOutput.Text == "Kelvin")
            {
                iFahrenheit = Double.Parse(lbl_InputDisplay.Text);
                lbl_Convert.Text = (Math.Round(((((iFahrenheit - 32) * 5) / 9) + 273.15), 2).ToString());
                lbl_RemainTemp1.Text = (Math.Round((((iFahrenheit - 32) * 5) / 9), 2).ToString())  + " C";
            }
            else if (cmbInput.Text == "Celcius" && cmbOutput.Text == "Kelvin")
            {
                iCelcius = Double.Parse(lbl_InputDisplay.Text);
                lbl_Convert.Text = (Math.Round((iCelcius + 273.15), 2).ToString());
                lbl_RemainTemp1.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString()) + " F";
            }
            else if (cmbInput.Text == "Kelvin" && cmbOutput.Text == "Celcius")
            {
                iKelvin = Double.Parse(lbl_InputDisplay.Text);
                lbl_Convert.Text = (Math.Round((iKelvin - 273.15), 2).ToString());
                iCelcius = iKelvin - 273.15;
                lbl_RemainTemp1.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString()) + " F";
            }
            else if (cmbInput.Text == "Kelvin" && cmbOutput.Text == "Fahrenheit")
            {
                iKelvin = Double.Parse(lbl_InputDisplay.Text);
                iCelcius = iKelvin - 273.15;
                lbl_Convert.Text = (Math.Round((((9 * iCelcius) / 5) + 32), 2).ToString());
                lbl_RemainTemp1.Text = iCelcius.ToString() + " C";
            }
        }

        private void btn_PM_Click(object sender, EventArgs e)
        {
            if (lbl_InputDisplay.Text.Contains("."))
            {
                lbl_InputDisplay.Text = lbl_InputDisplay.Text.Remove(0, 1);
            }
            else
            {
                lbl_InputDisplay.Text = (-1*Convert.ToInt16(lbl_InputDisplay.Text)).ToString();
            }
        }
    }
}
