using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DECCA2
{
    class LoadCombos
    {
        public static void LoadVelCombos(ComboBox combo, string speed)
        {
            combo.Text = speed;
            combo.Items.Add("m/s");
            combo.Items.Add("fps");
            combo.Items.Add("knots");
            combo.Items.Add("km/h");
            combo.Items.Add("mach");
            combo.Items.Add("fpm");
            combo.Items.Add("KTAS");
            combo.Items.Add("KEAS");
            combo.Items.Add("KCAS");
        }

        public static void LoadPower(ComboBox combo,string power)
        {
            combo.Text = power;
            combo.Items.Add("lbs");
            combo.Items.Add("hp");
            combo.Items.Add("N");
            combo.Items.Add("kW");            
        }

        public static void LoadUnitType(ComboBox combo,string unit)
        {
            combo.Text = unit;
            combo.Items.Add("Metric");
            combo.Items.Add("British");
        }

        public static void LoadAreaCombos(ComboBox combo, string area)
        {
            combo.Text = area;
            combo.Items.Add("m^2");
            combo.Items.Add("ft^2");
        }

        public static void LoadDistanceCombos(ComboBox combo, string distance)
        {
            combo.Text = distance;
            combo.Items.Add("ft");
            combo.Items.Add("m");
            combo.Items.Add("miles");
            combo.Items.Add("nm");
            combo.Items.Add("km");
        }

        public static void LoadWeightCombos(ComboBox combo,string weight)
        {
            combo.Text = weight;
            combo.Items.Add("lbs");
            combo.Items.Add("kg");
            combo.Items.Add("N");
        }

        public static void LoadTempCombos(ComboBox combo, string temp)
        {
            combo.Text = temp;
            combo.Items.Add("C");
            combo.Items.Add("F");
            combo.Items.Add("K");
            combo.Items.Add("R");
        }

        public static void LoadAircraftType(ComboBox cmbAircft_Type)
        {
            cmbAircft_Type.Text = "Jet Transport";
            cmbAircft_Type.Items.Add("Jet Transport");
            cmbAircft_Type.Items.Add("Business Jet");
            cmbAircft_Type.Items.Add("Sailplane (glider)");
            cmbAircft_Type.Items.Add("GA-single Engine");
            cmbAircft_Type.Items.Add("GA-twin Engine");
            cmbAircft_Type.Items.Add("Subsonic Military");
            cmbAircft_Type.Items.Add("Supersonic Fighter");
            cmbAircft_Type.Items.Add("Home-built");
            cmbAircft_Type.Items.Add("Helicopter");
            cmbAircft_Type.Items.Add("Ultralight");
        }
    }
}
