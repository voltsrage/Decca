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
    public partial class BaseWindow : Form
    {

        Random rnd = new Random();

        public BaseWindow()
        {
            InitializeComponent();   
            
            //this.Size = new Size(800, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
        }       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pb_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            
        }

        private void pb_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BaseWindow_Load(object sender, EventArgs e)
        {
            
        }
    }
}
