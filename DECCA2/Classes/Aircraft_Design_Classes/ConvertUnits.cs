using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DECCA2
{
    public class ConvertUnits
    {
        
        public static void ConvertToVelMs(ComboBox combo, TextBox textBox, Double vel,TextBox height)
        {
            SystemsEngApproachAircraftDesign AircraftDesign = new SystemsEngApproachAircraftDesign();
            AircraftDesign.CalcAtmos(height.Text);
            if (combo.Text == "m/s")
            {
                textBox.Text = vel.ToString();
            }
            else if (combo.Text == "knots")
            {
                textBox.Text = Math.Round((vel * 0.51444), 2).ToString();
            }
            else if (combo.Text == "ft/s")
            {
                textBox.Text = Math.Round((vel * 0.3042), 2).ToString();
            }
            else if (combo.Text == "mach")
            {
                textBox.Text = Math.Round((vel * AircraftDesign.a_h), 2).ToString();
            }
        }

        public static void ConvertToVelFts(ComboBox combo, TextBox textBox, Double vel, TextBox height)
        {
            SystemsEngApproachAircraftDesign AircraftDesign = new SystemsEngApproachAircraftDesign();
            AircraftDesign.CalcAtmos(height.Text);
            if (combo.Text == "ft/s")
            {
                textBox.Text = vel.ToString();
            }
            else if (combo.Text == "knots")
            {
                textBox.Text = Math.Round((vel * 1.688), 2).ToString();
            }
            else if (combo.Text == "m/s")
            {
                textBox.Text = Math.Round((vel / 0.3048), 2).ToString();
            }
            else if (combo.Text == "mach")
            {
                textBox.Text = Math.Round((vel * AircraftDesign.a_h)/0.3048, 2).ToString();
            }
        }

        public static void ConvertWeighttoLbs(ComboBox combo, TextBox textBox, Double weight)
        {
            if (combo.Text == "lbs")
            {
                textBox.Text = weight.ToString();
            }
            else if (combo.Text == "kg")
            {
                textBox.Text = Math.Round((weight * 0.453592), 2).ToString();
            }
            else if (combo.Text == "N")
            {
                textBox.Text = Math.Round((weight * 0.453592 * 10), 2).ToString();
            }
        }

        public static void ConvertWeighttoKg(ComboBox combo, TextBox textBox, Double weight)
        {
            if (combo.Text == "kg")
            {
                textBox.Text = weight.ToString();
            }
            else if (combo.Text == "lbs")
            {
                textBox.Text = Math.Round((weight / 0.453592), 2).ToString();
            }
            else if (combo.Text == "N")
            {
                textBox.Text = Math.Round((weight / 9.81), 2).ToString();
            }
        }

        public static void ConvertAreatoFt2(ComboBox combo, TextBox textBox, Double area)
        {
            if (combo.Text == "ft^2")
            {
                textBox.Text = area.ToString();
            }
            else if (combo.Text == "m^2")
            {
                textBox.Text = Math.Round((area * 10.76391), 2).ToString();
            }            
        }

        public static void ConvertAreatoM2(ComboBox combo, TextBox textBox, Double area)
        {
            if (combo.Text == "m^2")
            {
                textBox.Text = area.ToString();
            }
            else if (combo.Text == "ft^2")
            {
                textBox.Text = Math.Round((area / 10.76391), 2).ToString();
            }
        }
    }
}
