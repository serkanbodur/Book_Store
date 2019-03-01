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
using System.Net.Mail;
using System.Collections;
namespace Book_Store
{
    public partial class Customer_Page : Form
    {
        public Customer_Page()
        {
            InitializeComponent();
        }
        ItemToPurchase item = new ItemToPurchase();
        ShoppingCard product = new ShoppingCard();
        public string isim;
        public int toplam = 0;
        Book b = new Book();
        Magazine magazin = new Magazine();
        MusicCD music = new MusicCD();
        private void button1_Click(object sender, EventArgs e)
        {
            Login_Page login = new Login_Page();
            login.Show();
            this.Hide();
        }
        public static string usr;
        private void Customer_Page_Load(object sender, EventArgs e)
        {
            cmbbxPaymentType.SelectedIndex = 0;
            lblCustomerName.Text = usr;
            MessageBox.Show(lblCustomerName.Text, "Welcome !", MessageBoxButtons.OK, MessageBoxIcon.None);
          

        }

        private void btnBuy_Click(object sender, EventArgs e)
        {



            try
            {
                MessageBox.Show("Selected item is adding to Shopping Card...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Boolean arama = false;
                isim = lblName.Text;

                for (int i = 0; i < ShoppingCard.RowCount; i++)
                {
                    if (ShoppingCard.Rows[i].Cells[1].Value.ToString() == isim)
                    {
                        item.Quantity = int.Parse(ShoppingCard.Rows[i].Cells[3].Value.ToString());
                        item.Quantity += int.Parse(txtQuantity.Text);
                        ShoppingCard.Rows[i].Cells[3].Value = item.Quantity;
                        arama = true;
                        break;
                    }
                }

                if (arama == false)
                {
                    if (dtgrdviewbooks.ColumnCount > 5)
                    {
                        product.addProduct(isim);
                        product.paymentamount = int.Parse(dtgrdviewbooks.CurrentRow.Cells[6].Value.ToString());
                        item.Quantity = int.Parse(txtQuantity.Text);
                        item.Product = isim;

                        ShoppingCard.Rows.Add(lbl_ID.Text, item.Product, product.paymentamount, item.Quantity);
                    }
                    else
                    {
                        product.addProduct(isim);
                        product.paymentamount = int.Parse(dtgrdviewbooks.CurrentRow.Cells[4].Value.ToString());
                        item.Quantity = int.Parse(txtQuantity.Text);
                        item.Product = isim;

                        ShoppingCard.Rows.Add(lbl_ID.Text, item.Product, product.paymentamount, item.Quantity);
                    }
                }

                lblName.Text = "";
                lbl_ID.Text = "";
                txtQuantity.Text = "";

                toplam = 0;
                for (int i = 0; i < ShoppingCard.RowCount; i++)
                {
                    toplam += (int.Parse(ShoppingCard.Rows[i].Cells[2].Value.ToString()) * int.Parse(ShoppingCard.Rows[i].Cells[3].Value.ToString()));
                }

                txtCost.Text = toplam.ToString();

                MessageBox.Show("Selected items are added to Shopping Card.", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Please check your selected items !", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnViewBook_Click(object sender, EventArgs e)
        {

            b.printProperties(dtgrdviewbooks);
        }

        private void btnViewMusicCD_Click(object sender, EventArgs e)
        {
            music.printProperties(dtgrdviewbooks);
        }

        private void btnViewMagazineList_Click(object sender, EventArgs e)
        {
            magazin.printProperties(dtgrdviewbooks);
        }

        private void dtgrdviewbooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lbl_ID.Text = dtgrdviewbooks.CurrentRow.Cells[0].Value.ToString();
            lblName.Text = dtgrdviewbooks.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnEndBuy_Click(object sender, EventArgs e)
        {

            if (int.Parse(ShoppingCard.RowCount.ToString()) < 1)
            {
                MessageBox.Show("Your list is empty !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            { 
                SqlConnection cnn = new SqlConnection(@"Data Source=Your PC Name;Initial Catalog=Book_Store;Integrated Security=True");
                cnn.Open();
                
                for (int i = 0; i < ShoppingCard.RowCount; i++)
                {
                    SqlCommand cmd = new SqlCommand("Insert into ShoppingCard (Customer_Name,Product_Name,Product_Quantity) Values(@gelen,@name,@quantity)", cnn);
                    cmd.Parameters.AddWithValue("@gelen", lblCustomerName.Text);
                    cmd.Parameters.AddWithValue("@name", ShoppingCard.Rows[i].Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(ShoppingCard.Rows[i].Cells[3].Value.ToString()));
                    cmd.ExecuteNonQuery();
                }                               
                cnn.Close();
                product.paymenttype = cmbbxPaymentType.Text;
                product.placeOrder();
                ShoppingCard.Rows.Clear();
                product.ItemsToPurchase.Clear();
                txtCost.Text = "0";
                MessageBox.Show("Your purchase is successfull", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductList.Items.Clear();
            }
        }

        private void btnClrShoppingcard_Click(object sender, EventArgs e)
        {
            if (int.Parse(ShoppingCard.RowCount.ToString()) < 1)
            {
                MessageBox.Show("Your shopping card is already clear !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                product.ItemsToPurchase.Clear();
                ShoppingCard.Rows.Clear();
                txtCost.Text = "0";
                MessageBox.Show("Your shopping card is cleared.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private void btnClearSCItem_Click(object sender, EventArgs e)
        {

            try
            {
                isim = lblDeleted.Text;

                MessageBox.Show("Selected product removing from shopping card...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (int.Parse(ShoppingCard.CurrentRow.Cells[3].Value.ToString()) > 1)
                {
                    if (txtDeletedQuantity.Text == ShoppingCard.CurrentRow.Cells[3].Value.ToString())
                    {
                        product.removeProduct(isim);
                        int selectedIndex = ShoppingCard.CurrentCell.RowIndex;
                        if (selectedIndex > -1)
                        {
                            ShoppingCard.Rows.RemoveAt(selectedIndex);
                            ShoppingCard.Refresh();
                        }
                        MessageBox.Show("The product is removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (int.Parse(txtDeletedQuantity.Text) > int.Parse(ShoppingCard.CurrentRow.Cells[3].Value.ToString()))
                    {
                        MessageBox.Show("Product numbers are greater than in Shopping Card  !", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        item.Quantity = int.Parse(ShoppingCard.CurrentRow.Cells[3].Value.ToString());
                        item.Quantity = item.Quantity - int.Parse(txtDeletedQuantity.Text);
                        ShoppingCard.CurrentRow.Cells[3].Value = item.Quantity;
                        MessageBox.Show("The product is removed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    product.removeProduct(isim);
                    int selectedIndex = ShoppingCard.CurrentCell.RowIndex;
                    if (selectedIndex > -1)
                    {
                        ShoppingCard.Rows.RemoveAt(selectedIndex);
                        ShoppingCard.Refresh();
                    }
                    MessageBox.Show("The product is removed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtDeletedQuantity.Text = "";
                lblDeleted.Text = "";

                toplam = 0;
                for (int i = 0; i < ShoppingCard.RowCount; i++)
                {
                    toplam += (int.Parse(ShoppingCard.Rows[i].Cells[2].Value.ToString()) * int.Parse(ShoppingCard.Rows[i].Cells[3].Value.ToString()));
                }
                txtCost.Text = toplam.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Please your sure that true product's name or product's quantities !", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPrintProduct_Click(object sender, EventArgs e)
        {
            ProductList.Items.Clear();

            foreach (var i in product.ItemsToPurchase)
            {
                ProductList.Items.Add(i);
            }
        }

        private void ShoppingCard_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblDeleted.Text = ShoppingCard.CurrentRow.Cells[1].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
