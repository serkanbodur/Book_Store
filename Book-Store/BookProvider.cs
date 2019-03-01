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
    class BookProvider
    {
        SqlConnection con;
        SqlCommand cmd;
        

        public BookProvider() //Kurucu metot
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
     
        public void Insert(Book b)
        {
            try
            {   
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
                SqlCommand cmd=new SqlCommand ("Insert into Product (Name,Price) Values(@name,@price)",con);
                cmd.Parameters.AddWithValue("@name", b.name);
                cmd.Parameters.AddWithValue("@price", b.price);
                cmd.ExecuteNonQuery();
                con.Close();
              
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
                SqlCommand komut = new SqlCommand("Insert into Book (Book_ID,ISBNno,Author,Publisher,Page) Values((SELECT IDENT_CURRENT('Product')),@isbn,@author,@publisher,@page)", con);

                komut.Parameters.AddWithValue("@isbn", b.isbn);
                komut.Parameters.AddWithValue("@author", b.Author);
                komut.Parameters.AddWithValue("@publisher",b.Publisher);
                komut.Parameters.AddWithValue("@page", b.Page);
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Register !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Registor is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void Delete_Book(long deletedBook,int deletedProduct)
        {
            //
         //   
            try
            {
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand("Delete From Book Where ISBNno=" + deletedBook,con);
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("Delete From Product Where Product_ID=" +deletedProduct,con);
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


        public void Update(Book oldbook,Book newbook)
        {
            try
            {
            SqlConnection connect = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
                if (connect.State == System.Data.ConnectionState.Closed) connect.Open();

                SqlCommand kmt = new SqlCommand("Update Product Set [Name] = '" + newbook.name + "',[Price] = " +(newbook.price) + " Where [Product_ID] = " +(oldbook.ID)+ " ",connect);
                kmt.ExecuteNonQuery();
                connect.Close();

                if (connect.State == System.Data.ConnectionState.Closed) connect.Open();
                SqlCommand komut = new SqlCommand("Update Book Set [ISBNno] = " +(newbook.isbn) + ",[Author] = '" + newbook.Author + "',[Publisher] = '" + newbook.Publisher + "',[Page] = " + (newbook.Page) + " Where [Book_ID] = " + (oldbook.ID)+" ",connect);
                komut.ExecuteNonQuery();
                connect.Close();

                MessageBox.Show("Successfully Update !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Updating is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }

}

