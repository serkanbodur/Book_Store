using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace Book_Store
{
    class Book : Product
    {
        private long ISBN;
        public long isbn
        {
            get { return ISBN; }
            set { ISBN = value; }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        private int page;
        public int Page
        {
            get { return page; }
            set { page = value; }
        }
        public Book() { }
        public override void printProperties(DataGridView data)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=Your PC Name ;Initial Catalog=Book_Store;Integrated Security=True");

            baglanti.Open();

            string query = "SELECT Product.Product_ID,Product.Name,Book.Author,Book.ISBNno,Book.Page,Book.Publisher,Product.Price FROM Book Full Join Product on(Product.Product_ID = Book.Book_ID) Where Page !=' '";
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

    }
}