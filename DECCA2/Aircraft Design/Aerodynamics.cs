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
    public partial class Aerodynamics : Form
    {
        public Aerodynamics()
        {
            InitializeComponent();
        }

        private void Aerodynamics_Load(object sender, EventArgs e)
        {
            CalculatePoints();
        }

        Double C_d, C_l, C_n, C_a, Cm_c4;

        List<Double> x_ac;
        List<Double> x_cp;
        
        List<Double> alpha_rad;
        List<Double> alpha_deg;
        

        private void CalculatePoints()
        {
            Double alpha = -5.0;

            alpha_deg = new List<double>();
            alpha_rad = new List<double>();
            x_cp = new List<double>();
            x_ac = new List<double>();

            while(alpha < 10)
            {
                alpha_deg.Add(alpha);
                Double alp_rad = alpha / 57.3;
                alpha_rad.Add(Math.Round(alpha / 57.3,4));

                C_l = 6 * alp_rad + 0.2;
                C_d = 0.2 * Math.Pow(alp_rad, 2) + 0.006;
                Cm_c4 = -0.05 - 0.01 * alp_rad;

                x_cp.Add(Math.Round(0.25 - (Cm_c4 / (C_l * Math.Cos(alp_rad) + C_d * Math.Sin(alp_rad))), 5));



                alpha++;
            }

            listBox1.DataSource = alpha_deg;
            listBox2.DataSource = alpha_rad;
            listBox4.DataSource = x_cp;
        }
    }
}
