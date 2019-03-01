using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Book_Store
{
    public partial class Entrance : Form
    {
        public Entrance()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = Convert.ToString(Convert.ToInt32(label3.Text) - 1);
            progressBar1.PerformStep();
            if (label3.Text == "1")
            {
                label4.Text = "second...";
            }
            if (label3.Text == "0")
            {
                this.Hide();
                Login_Page sist = new Login_Page();
                sist.ShowDialog();
                this.Hide();
                
            }
        }

        private void Entrance_Load(object sender, EventArgs e)
        {

        }
    }
}
