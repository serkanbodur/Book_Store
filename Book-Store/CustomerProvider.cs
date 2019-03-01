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
    class CustomerProvider
    {
        SqlConnection con;
        SqlCommand cmd;


        public CustomerProvider() //Kurucu metot
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        
      

        public void Update_Customer(Customer oldcustom, Customer newcustom)
        {
            try
            {
                SqlConnection connect = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
                if (connect.State == System.Data.ConnectionState.Closed) connect.Open();

                SqlCommand kmt = new SqlCommand("Update Customer Set [Name] = '" + newcustom.name + "',[Adress] = '" + newcustom.adress + "',[Email] = '" + newcustom.email + "',[Username] = '" + newcustom.username + "',[Password] = '" + newcustom.password + "' Where [customerID] = " + Convert.ToInt32(oldcustom.customerid)+ " ", connect);
                kmt.ExecuteNonQuery();
                connect.Close();

                MessageBox.Show("Successfully Update !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Updating is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public void Delete_Customer(int delete)
        {
        
            try
            {
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
               // int srg = int.Parse(DtgrdviewCustomer.CurrentRow.Cells[cst.customerid].Value.ToString());
                SqlCommand cmd = new SqlCommand("Delete From Customer Where customerId=" + delete,con);
                /*                 
                cmd.CommandText = "Delete From Customer Where customerID=@customerid";
                cmd.Parameters.AddWithValue("@customerid", deleted);
                */
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully Deletion !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Deletion is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


    }
}
