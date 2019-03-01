using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Book_Store
{

    public partial class Login_Page : Form
    {

        public Login_Page()
        {
            InitializeComponent();
        }

        private void Customer_Login_Load(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            singletondSql sorgu = singletondSql.getinstance();

            try
            {
                lbltarih.Text = DateTime.Now.ToString();
                sorgu.Login(textBox1, textBox2, this);               

            }
            catch (Exception)
            {
                MessageBox.Show("The database could not be reached.", "ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp_Page sign = new SignUp_Page();
            sign.Show();
            linkLabel1.LinkVisited = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    //Singled Designed Pattern
    public class singletondSql
    {
        private static singletondSql initial;
        private static singletondSql _initial
        {
            get
            {
                if (initial == null)
                {
                    initial = new singletondSql();
                }
                return initial;
            }
            set { initial = value; }
        }
        private singletondSql() { }
        public static singletondSql getinstance()
        {
            return _initial;
        }
        string sorgu = "Select * from Customer Where Username=@kullaniciadi AND Password=@sifre";
        public void Login(TextBox user, TextBox pass, Form frm)
        {
            SqlCommand cmd = new SqlCommand(sorgu, baglan);
            cmd.Parameters.AddWithValue("@kullaniciadi", user.Text);
            cmd.Parameters.AddWithValue("@sifre", pass.Text);
            baglan.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {              
                if (dr["Access"].ToString() == "1")
                {
              
                    Customer_Page customer = new Customer_Page();             
                    Customer_Page.usr = user.Text;                 
                    customer.Show();
                }
                else
                {
                    Admin_Page admin = new Admin_Page();
                    Admin_Page.usr = user.Text;
                    admin.Show();
                    frm.Hide();
                }

            }
            else
            {
                MessageBox.Show("Incorrect username or password", "ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            baglan.Close();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
        bool state;
        public void ctrl(TextBox kullanici)
        {
            baglan.Open();
            SqlCommand sorgu = new SqlCommand("Select * from Customer Where Username=@kullaniciadi", baglan);
            sorgu.Parameters.AddWithValue("@kullaniciadi", kullanici.Text);
            SqlDataReader dr = sorgu.ExecuteReader();

            if (dr.Read())
            {
                state = false;
            }
            else
            {
                state = true;
            }
            baglan.Close();
        }
        public void Insert(TextBox name, TextBox adress, TextBox email, TextBox user, TextBox password, Label date)
        {
            ctrl(user);
            if (state == true)
            {
                SqlCommand komut = new SqlCommand("Insert into Customer (Name,Adress,Email,Username,Password,Register_Time) Values(@name,@address,@email,@user, @pass,@time)", baglan);
                komut.Parameters.AddWithValue("@name", name.Text);
                komut.Parameters.AddWithValue("@address", adress.Text);
                komut.Parameters.AddWithValue("@email", email.Text);
                komut.Parameters.AddWithValue("@User", user.Text);
                komut.Parameters.AddWithValue("@pass", password.Text.ToString());
                komut.Parameters.AddWithValue("@time", Convert.ToDateTime(date.Text));
                if (baglan.State == System.Data.ConnectionState.Closed) baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Successful Registration");
                name.Text = "";
                adress.Text = "";
                email.Text = "";
                user.Text = "";
                password.Text = "";
                Customer_Page cstmr = new Customer_Page();
                cstmr.Show();
                SignUp_Page sign = new SignUp_Page();
                sign.Hide();

            }
            else
            {
                MessageBox.Show("There is another user with the same username", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglan.Close();
            }
        }
    }
}
