using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Book_Store
{
    class Customer
    {
        private int CustomerID;
        public int customerid
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }

        private string Name;
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        private string Adress;
        public string adress
        {
            get { return Adress; }
            set { Adress = value; }
        }

        private string Email;
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }

        private string Username;
        public string username
        {
            get { return Username; }
            set { Username = value; }
        }

        private string Password;
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }
        public Customer() { }
        public List<Customer> printCustomerDetails()
        {

            try
            {
                List<Customer> Customerlist = new List<Customer>();
                SqlConnection con = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("SELECT customerID,Name,Adress,Email,Username,Password from Customer", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Customer c = new Customer();
                    c.customerid = int.Parse(dr[0].ToString());
                    c.name = dr[1].ToString();
                    c.adress = dr[2].ToString();
                    c.email = (dr[3].ToString());
                    c.username = (dr[4].ToString());
                    c.password = dr[5].ToString();
                    Customerlist.Add(c);
                }
                con.Close();
                return Customerlist;
            }
            catch
            {
                MessageBox.Show("Customer List isn't shown !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw;
            }

        }

        public void printCustomerPurchases()
        {


        }
        public void saveCustomer(Customer cst)
        {
            SqlConnection con = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Customer (Name,Adress,Email,Username,Password) Values(@name,@adres,@email,@user,@password)", con);
            cmd.Parameters.AddWithValue("@name", cst.name);
            cmd.Parameters.AddWithValue("@adres", cst.adress);
            cmd.Parameters.AddWithValue("@email", cst.email);
            cmd.Parameters.AddWithValue("@user", cst.username);
            cmd.Parameters.AddWithValue("@password", cst.password);

            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
