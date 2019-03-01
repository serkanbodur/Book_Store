using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace Book_Store
{
    class MusicCD : Product
    {


        private string singer;

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

    
        public string Singer
        {
            get { return singer; }
            set { singer = value; }
        }

        public MusicCD()
        { }
     
        SqlConnection baglanti = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
        public override void printProperties(DataGridView data)
        {

            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            string query = "SELECT Product.Product_ID,Product.Name,MusicCD.Type,MusicCD.Singer,Product.Price FROM Product Full Join MusicCD on(MusicCD.MusicCD_ID = Product.Product_ID)  Where Singer !=' '";
            // Bağlantı açıldığında çalışacak sql sorgusu için cmd nesnesi oluşturulur:

            SqlCommand cmd = new SqlCommand(query, baglanti);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            // DataTable türündeki dtable nesnesi oluşturulur:
            DataTable dtable = new DataTable();

            adp.Fill(dtable);

            // dataGridView'ımız verileri dtable'dan alır ve gösterir:
            data.DataSource = dtable;

            // Bağlantı kapatılır:
            baglanti.Close();

        }

        public void InsertMusic(MusicCD m)
        {
            try
            {
                if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();
                SqlCommand cmd = new SqlCommand("Insert into Product (Name,Price) Values(@name,@price)", baglanti);
                cmd.Parameters.AddWithValue("@name", m.name);
                cmd.Parameters.AddWithValue("@price", m.price);
                cmd.ExecuteNonQuery();
                baglanti.Close();

                if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();
                SqlCommand komut = new SqlCommand("Insert into MusicCD(MusicCD_ID,Singer,Type) Values((SELECT IDENT_CURRENT('Product')),@singer,@type)", baglanti);

                komut.Parameters.AddWithValue("@singer", m.singer);
                komut.Parameters.AddWithValue("@type",m.type);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Successfully Register !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Registor is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                throw;
            }
        }


      
    }
}
