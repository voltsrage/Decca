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
    public partial class RoskamFuselageDesign : Form
    {
        Double Lf_Df, Lfc_Df, theta_fc, Cab_len, Cab_wth, Cab_Hgt, S_Abrt, Ais_Lgth;        

        Double df, th_f;

        public RoskamFuselageDesign()
        {
            InitializeComponent();
        }

        private void RoskamFuselageDesign_Load(object sender, EventArgs e)
        {
            LoadFuselageThickness();
        }

        private void LoadFuselageThickness()
        {
            cmb_Struc_Thick.Items.Add("Small Commercial Aircraft");
            cmb_Struc_Thick.Items.Add("Figthers and Trainers");
            cmb_Struc_Thick.Items.Add("Large Transports");
        }

        private void cmb_Struc_Thick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Struc_Thick.Text == "Small Commercial Aircraft")
            {
                th_f = 1.5 / 12;
            }
            else if (cmb_Struc_Thick.Text == "Figthers and Trainers")
            {
                th_f = 2 / 12;
            }
            else if (cmb_Struc_Thick.Text == "Large Transports")
            {
                th_f = 0.02*df + 1/12;
            }
        }

        private void GetFuselageDetails()
        {
            Lf_Df = Double.Parse(txt_Lf_Df.Text);
            Lfc_Df = Double.Parse(txt_Lfc_Df.Text);
            theta_fc = Double.Parse(txt_theta_fc.Text);
            Cab_Hgt = Double.Parse(txt_Cab_Max_Hgt.Text);
            Cab_len = Double.Parse(txt_Cab_Len.Text);
            Cab_wth = Double.Parse(txt_Cab_Max_Wth.Text);
            S_Abrt = Double.Parse(txt_Seat_Abrt.Text);
            Ais_Lgth = Double.Parse(txt_Ais_Wdth.Text);
        }
    }
}
