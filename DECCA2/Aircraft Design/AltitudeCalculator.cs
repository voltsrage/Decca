using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DECCA2
{
    public class AltitudeCalculator
    {
        public static Double Pres_Standard = 101325; // Sea Level Pressure (N/m2)

        public static Double Temp_Standard = 288.15; // Sea Level Temperature  (K)

        //Double Vis_Standard = 1.7893E-10; // Sea Level Viscosity (m/s)

        public static Double Dens_Standard = 1.225; // Sea Level Density (kg/m3)        

        public static Double R = 287.05287; // Gas Constant (J/(kgK)

        public static Double gamma = 1.4; // Ratio of Specific Heats

        public static Double a = 0.0065;

        public static Double g = 9.80665; //Gravity

        public static Double r_e = 6356766; //Radius of the earth       

        public static Double P_h, T, rho_h, rel_rho_h, a_h, g_h, m_h, q_c, qc_p0, qc_p, a0, h_int, V_int;

        public static void CalcAtmos(string h, ComboBox combo) 
        {
            Double A_Standard = Math.Sqrt(gamma * R * Temp_Standard);
            try
            {
                h_int = Convert.ToDouble(h);
                if (combo.Text == "m")
                {
                    h_int = Convert.ToDouble(h); ;
                }
                else if (combo.Text == "ft")
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
                    rho_h = Math.Round((P_h / (R * T_tps)), 3);
                    rel_rho_h = Math.Round((rho_h / Dens_Standard), 3);
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
    }
}
