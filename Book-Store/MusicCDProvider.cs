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
    class MusicCDProvider
    {
        SqlConnection con;
        SqlCommand cmd;


        public MusicCDProvider() //Kurucu metot
        {
            Baglan();
        }

        public void Baglan()
        {
            con = new SqlConnection(@"Data Sourc=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        public void UpdateMusic(MusicCD oldMusic, MusicCD newMusic)
        {
            try
            {
                SqlConnection connect = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
                if (connect.State == System.Data.ConnectionState.Closed) connect.Open();

                SqlCommand kmt = new SqlCommand("Update Product Set [Name] = '" + newMusic.name + "',[Price] = " + (newMusic.price) + " Where [Product_ID] = " + (oldMusic.ID) + " ", connect);
                kmt.ExecuteNonQuery();
                connect.Close();

                if (connect.State == System.Data.ConnectionState.Closed) connect.Open();
                SqlCommand komut = new SqlCommand("Update MusicCD Set [Singer] = '" + (newMusic.Singer) + "',[Type] = '" + newMusic.Type + "' Where [MusicCD_ID] = " + (oldMusic.ID) + " ", connect);
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Successfully Update !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Updating is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void Delete_MusicCD(int deletedMusic)
        {
            //
            //   
            try
            {
                if (con.State == System.Data.ConnectionState.Closed) con.Open();
                SqlCommand cmd = new SqlCommand("Delete From MusicCD Where MusicCD_ID=" + deletedMusic, con);
                cmd.ExecuteNonQuery();


                cmd = new SqlCommand("Delete From Product Where Product_ID=" + deletedMusic, con);
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
