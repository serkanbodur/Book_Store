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
    class Magazine:Product
    {
        private int issue;
        public int Issue
        {
            set { issue = value; }
            get { return issue; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public Magazine() { }
        SqlConnection baglanti = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
        public override void printProperties(DataGridView data)
        {

            if (baglanti.State == System.Data.ConnectionState.Closed) baglanti.Open();

            string query = "SELECT Product.Product_ID,Product.Name,Magazine.Type,Magazine.Issue,Product.Price FROM Product Full Join Magazine on(Magazine.Magazine_ID = Product.Product_ID)  Where Issue !=' '";
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
        public void InsertMagazine(Magazine m)
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
                SqlCommand komut = new SqlCommand("Insert into Magazine(Magazine_ID,Issue,Type) Values((SELECT IDENT_CURRENT('Product')),@issue,@type)", baglanti);

                komut.Parameters.AddWithValue("@issue", m.Issue);
                komut.Parameters.AddWithValue("@type", m.type);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Successfully Register !", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Registor is failed !", "Failed !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}
