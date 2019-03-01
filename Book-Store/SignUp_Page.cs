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
    public partial class SignUp_Page : Form
    {
        public SignUp_Page()
        {
            InitializeComponent();
        }

        private void btnKyt_Click(object sender, EventArgs e)
        {
            singletondSql sorgu = singletondSql.getinstance();
            if (txtName.Text != "" && txtAdres.Text != "" && txtEmail.Text != "" && txtUser.Text != "" && txtPass.Text != "")
            {
                label6.Text = DateTime.Now.ToString();
                sorgu.Insert(txtName, txtAdres, txtEmail, txtUser, txtPass, label6);
                     
            }
            else
            {
                MessageBox.Show("Please fill in the blank fields", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login_Page login = new Login_Page();
            login.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
