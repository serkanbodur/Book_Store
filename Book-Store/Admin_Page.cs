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
    public partial class Admin_Page : Form
    {
        BookProvider bookprovide = new BookProvider();
        CustomerProvider customerprovide = new CustomerProvider();
        MusicCDProvider musicProvide = new MusicCDProvider();
        Book B = new Book();
        public Admin_Page()
        {
            InitializeComponent();
        }

        public SqlConnection baglanti;
        private void button1_Click(object sender, EventArgs e)
        {
            Login_Page login = new Login_Page();
            login.Show();
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            grpBxMagazine.Visible = false;
            GrpBxMusic.Visible = false;
            GrpBxBook.Visible = true;
            B.printProperties(dataGridView1);
            txtAddNewBook.Show();
            button6.Show();
            button7.Show();
        }


        public static string usr;
        private void Admin_Page_Load(object sender, EventArgs e)
        {
            MessageBox.Show(usr, "Welcome !", MessageBoxButtons.OK, MessageBoxIcon.None);
            GrpBxBook.Visible = false;
            GrpBxMusic.Visible = false;
            grpBxMagazine.Visible = false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (txtIsbn.Text != "" && txtName.Text != "" && txtAuthor.Text != "" && txtPage.Text != "" && txtPublisher.Text != "" && txtPrice.Text != "")
            {
                Book book = new Book();
                book.isbn = long.Parse(txtIsbn.Text);
                book.name = txtName.Text;
                book.Author = txtAuthor.Text;
                book.Publisher = txtPublisher.Text;
                book.Page = int.Parse(txtPage.Text);
                book.price = int.Parse(txtPrice.Text);
                bookprovide.Insert(book);
                B.printProperties(dataGridView1);
            }
            else
            {
                MessageBox.Show("Please fill in the blank fields", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Magazine magazin = new Magazine();
            GrpBxMusic.Visible = false;
            GrpBxBook.Visible = false;
            grpBxMagazine.Visible = true;
            magazin.printProperties(dataGridView1);
            txtAddNewBook.Show();
            button6.Show();
            button7.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Book b = new Book();
            b.isbn = long.Parse(dataGridView1.CurrentRow.Cells["ISBNno"].Value.ToString());
            b.ID = int.Parse(dataGridView1.CurrentRow.Cells["Product_ID"].Value.ToString());
            try
            {
                bookprovide.Delete_Book(b.isbn, b.ID);
                B.printProperties(dataGridView1);
            }
            catch (Exception)
            {
                throw;
            }
        }
        Book newbook = new Book();
        private void button7_Click(object sender, EventArgs e)
        {

            newbook.isbn = long.Parse(txtIsbn.Text);
            newbook.name = txtName.Text;
            newbook.Author = txtAuthor.Text;
            newbook.Publisher = txtPublisher.Text;
            newbook.Page = int.Parse(txtPage.Text);
            newbook.price = int.Parse(txtPrice.Text);
            bookprovide.Update(oldbook, newbook);
            B.printProperties(dataGridView1);
        }

        private void btnInsert_Click(object sender, EventArgs e) { }
        private void btnUpdate_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) { }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) { }

        Book oldbook = new Book();
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrpBxBook.Visible == true && GrpBxMusic.Visible == false &&grpBxMagazine.Visible==false)
            {
                int index = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[index];
                int a = int.Parse(row.Cells[0].Value.ToString());
                txtName.Text = row.Cells[1].Value.ToString();
                txtAuthor.Text = row.Cells[2].Value.ToString();
                txtIsbn.Text = row.Cells[3].Value.ToString();
                txtPage.Text = row.Cells[4].Value.ToString();
                txtPublisher.Text = row.Cells[5].Value.ToString();
                txtPrice.Text = row.Cells[6].Value.ToString();
                oldbook.ID = a;
                oldbook.isbn = long.Parse(txtIsbn.Text);
                oldbook.name = txtName.Text;
                oldbook.Author = txtAuthor.Text;
                oldbook.Publisher = txtPublisher.Text;
                oldbook.Page = int.Parse(txtPage.Text);
                oldbook.price = int.Parse(txtPrice.Text);
            }

            if (GrpBxBook.Visible == false && GrpBxMusic.Visible == true && grpBxMagazine.Visible == false)
            {
                int index = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[index];
                int a = int.Parse(row.Cells[0].Value.ToString());
                txtMusicName.Text = row.Cells[1].Value.ToString();
                cmBxType.Text = row.Cells[2].Value.ToString();
                txtSinger.Text = row.Cells[3].Value.ToString();
                txtMusicPrice.Text = row.Cells[4].Value.ToString();
                oldMusic.ID = a;
                oldMusic.name = txtMusicName.Text;
                oldMusic.Singer = txtSinger.Text;
                oldMusic.price = int.Parse(txtMusicPrice.Text);
                oldMusic.Type = cmBxType.Text;
            }
            if (GrpBxBook.Visible == false && GrpBxMusic.Visible == false && grpBxMagazine.Visible == true)
            {
                int index = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[index];
                int a = int.Parse(row.Cells[0].Value.ToString());
                txtMagazinName.Text = row.Cells[1].Value.ToString();
                cmBxType.Text = row.Cells[2].Value.ToString();
                txtIssue.Text = row.Cells[3].Value.ToString();
                txtMagazinePrice.Text = row.Cells[4].Value.ToString();
                oldmagazine.ID = a;
                oldmagazine.name = txtMusicName.Text;
                oldmagazine.Issue = int.Parse(txtIssue.Text);
                oldmagazine.price = int.Parse(txtMagazinePrice.Text);
                oldmagazine.Type = cmbMagazineType.Text;
            }
        }

        public void getcustomer()
        {
            Customer c = new Customer();
            DtgrdviewCustomer.DataSource = c.printCustomerDetails();
        }

        private void btnviewCustomer_Click(object sender, EventArgs e)
        {
            getcustomer();
        }

        bool state;
        void ctrl(TextBox kullanici)
        {
            baglanti = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");


            baglanti.Open();

            SqlCommand sorgu = new SqlCommand("Select * from Customer Where Username=@kullaniciadi", baglanti);
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
            baglanti.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "" && txtAdress.Text != "" && txtMail.Text != "" && txtUser.Text != "" && txtPass.Text != "")
            {
                ctrl(txtUser);
                if (state == true)
                {
                    Customer cstmr = new Customer();
                    cstmr.name = txtCustomerName.Text;
                    cstmr.adress = txtAdress.Text;
                    cstmr.email = txtMail.Text;
                    cstmr.username = txtUser.Text;
                    cstmr.password = txtPass.Text;
                    cstmr.saveCustomer(cstmr);
                    MessageBox.Show("Successfull Register", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baglanti.Close();
                    getcustomer();
                }
                else
                {
                    MessageBox.Show("There is another user with the same username", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    baglanti.Close();
                }
            }
            else
            {
                MessageBox.Show("Please fill in the blank fields", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Customer cst = new Customer();
            cst.customerid = int.Parse(DtgrdviewCustomer.CurrentRow.Cells[cst.customerid].Value.ToString());
            try
            {
                customerprovide.Delete_Customer(cst.customerid);
                getcustomer();
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void DtgrdviewCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        Customer csnew = new Customer();
        Customer csold = new Customer();
        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            csnew.name = txtCustomerName.Text;
            csnew.adress = txtAdress.Text;
            csnew.email = txtMail.Text;
            csnew.username = txtUser.Text;
            csnew.password = txtPass.Text;
            customerprovide.Update_Customer(csold, csnew);
            getcustomer();
        }

        private void DtgrdviewCustomer_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = DtgrdviewCustomer.Rows[index];
            int a = int.Parse(row.Cells[0].Value.ToString());
            txtCustomerName.Text = row.Cells[1].Value.ToString();
            txtAdress.Text = row.Cells[2].Value.ToString();
            txtMail.Text = row.Cells[3].Value.ToString();
            txtUser.Text = row.Cells[4].Value.ToString();
            txtPass.Text = row.Cells[5].Value.ToString();

            csold.customerid = a;
            csold.name = txtCustomerName.Text;
            csold.adress = txtAdress.Text;
            csold.email = txtMail.Text;
            csold.username = txtUser.Text;
            csold.password = txtPass.Text;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");

            string name = txtSearch.Text;
            //combining two tables and searching these tables
            if (GrpBxBook.Visible == true)
            {
                string sorgu = "SELECT Product.Product_ID,Product.Name,Book.Author,Book.ISBNno,Book.Page,Book.Publisher,Product.Price FROM Product Full Join Book on(Book.Book_ID = Product.Product_ID) where Name Like '" + name + "'";


                SqlDataAdapter adap = new SqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();

                adap.Fill(ds, "sorgu");

                this.dataGridView1.DataSource = ds.Tables[0];

                baglanti.Close();
            }
            if (GrpBxMusic.Visible == true)
            {
                string sorgu = "SELECT Product.Product_ID,Product.Name,MusicCD.Type,MusicCD.Singer,Product.Price FROM Product Full Join MusicCD on(MusicCD.MusicCD_ID = Product.Product_ID) where Name Like '" + name + "'";


                SqlDataAdapter adap = new SqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();

                adap.Fill(ds, "sorgu");

                this.dataGridView1.DataSource = ds.Tables[0];

                baglanti.Close();
            }
            if (grpBxMagazine.Visible == true)
            {
                string sorgu = "SELECT Product.Product_ID,Product.Name,Magazine.Type,Magazine.Issue,Product.Price FROM Product Full Join Magazine on(Magazine.Magazine_ID = Product.Product_ID) where Name Like '" + name + "'";


                SqlDataAdapter adap = new SqlDataAdapter(sorgu, baglanti);

                DataSet ds = new DataSet();

                adap.Fill(ds, "sorgu");

                this.dataGridView1.DataSource = ds.Tables[0];

                baglanti.Close();
            }

        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
            string name = txtSearchCustomer.Text;
            string sorgu = "Select * from Customer where Name Like '" + name + "'";
            SqlDataAdapter adap = new SqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            adap.Fill(ds, "sorgu");
            this.DtgrdviewCustomer.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void DtgrdviewCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            grpBxMagazine.Visible = false;
            GrpBxMusic.Visible = true;
            GrpBxBook.Visible = false;
            MusicCD m = new MusicCD();
            m.printProperties(dataGridView1);
        }

        private void btnAddMusic_Click(object sender, EventArgs e)
        {
            if (txtMusicName.Text != "" && txtSinger.Text != "" && cmBxType.Text != "" && txtMusicPrice.Text != "")
            {
                GrpBxBook.Visible = false;
                GrpBxMusic.Visible = true;
                MusicCD music = new MusicCD();
                music.name = txtMusicName.Text;
                music.Singer = txtSinger.Text;
                music.price = int.Parse(txtMusicPrice.Text);
                music.Type = cmBxType.Text;
                music.InsertMusic(music);
                music.printProperties(dataGridView1);
            }
            else
            {
                MessageBox.Show("Please fill in the blank fields", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        MusicCD m = new MusicCD();
        MusicCD newMusic = new MusicCD();
        MusicCD oldMusic = new MusicCD();
        private void btnUpdateMusic_Click(object sender, EventArgs e)
        {
            newMusic.price = int.Parse(txtMusicPrice.Text);
            newMusic.name = txtMusicName.Text;
            newMusic.Singer = txtSinger.Text;
            newMusic.Type = cmBxType.Text;
            musicProvide.UpdateMusic(oldMusic, newMusic);
            m.printProperties(dataGridView1);
        }

        private void btnRemoveMusic_Click(object sender, EventArgs e)
        {
            MusicCD m = new MusicCD();
            m.ID = int.Parse(dataGridView1.CurrentRow.Cells["Product_ID"].Value.ToString());
            try
            {
                musicProvide.Delete_MusicCD(m.ID);
                m.printProperties(dataGridView1);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cmBxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMagazineType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMagazineAdd_Click(object sender, EventArgs e)
        {
            if (txtMagazinName.Text != "" && txtIssue.Text != "" && cmbMagazineType.Text != "" && txtMagazinePrice.Text != "")
            {
                GrpBxBook.Visible = false;
                GrpBxMusic.Visible = false;
                grpBxMagazine.Visible = true;
                Magazine magazin = new Magazine();
                magazin.name = txtMagazinName.Text;
                magazin.Issue =int.Parse(txtIssue.Text);
                magazin.price = int.Parse(txtMagazinePrice.Text);
                magazin.Type = cmbMagazineType.Text;
                magazin.InsertMagazine(magazin);
                magazin.printProperties(dataGridView1);
            }
            else
            {
                MessageBox.Show("Please fill in the blank fields", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        Magazine newmagazine = new Magazine();
        Magazine oldmagazine = new Magazine();
        MagazineProvider magazineProvide = new MagazineProvider();
        Magazine magazin = new Magazine();
        private void btnUpdateMagazine_Click(object sender, EventArgs e)
        {
            newmagazine.price = int.Parse(txtMagazinePrice.Text);
            newmagazine.name = txtMagazinName.Text;
            newmagazine.Issue = int.Parse(txtIssue.Text);
            newmagazine.Type = cmbMagazineType.Text;
            magazineProvide.UpdateMagazine(oldmagazine, newmagazine);
            magazin.printProperties(dataGridView1);
        }

        private void btnRemoveMagazine_Click(object sender, EventArgs e)
        {
            Magazine m = new Magazine();
            m.ID = int.Parse(dataGridView1.CurrentRow.Cells["Product_ID"].Value.ToString());
            try
            {
                magazineProvide.Delete_Magazine(m.ID);
                m.printProperties(dataGridView1);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
