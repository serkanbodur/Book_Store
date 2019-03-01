using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Book_Store
{
    class ShoppingCard
    {
        private int CustomerID;
        public int customerid
        {
            get { return CustomerID; }
            set { CustomerID = value; }
        }
        private int PaymentAmount;
        public int paymentamount
        {
            get { return PaymentAmount; }
            set { PaymentAmount = value; }
        }
        private string PaymentType;
        public string paymenttype
        {
            get { return PaymentType; }
            set { PaymentType = value; }
        }
        private ArrayList itemsToPurchase = new ArrayList();
        public ArrayList ItemsToPurchase
        {
            get { return itemsToPurchase; }
            set { itemsToPurchase.Add(value); }
        }

        public string printProduct()
        {
            return itemsToPurchase.ToString();
        }
        public void addProduct(string item)
        {
            itemsToPurchase.Add(item);
        }
        public void removeProduct(string item)
        {
            itemsToPurchase.Remove(item);
        }
        public void placeOrder()
        {
            MessageBox.Show("Payment is completing and sending mail to your e-mail.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sendInvoidcebyEmail();
        }
        public void cancelOrder()
        {
            MessageBox.Show("Order is cancelled", "Success !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void sendInvoidcebyEmail()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("mailAdress", "Name");
            smtp.Port = 587;
            smtp.Host = "smtp.live.com";
            smtp.EnableSsl = true;

            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("mailAdress", "Name");
            ePosta.To.Add("mailAdress");
            ePosta.Subject = "Purchase";
            ePosta.IsBodyHtml = true;

            if (PaymentType == "Credit Card")
            {
                ePosta.Body = "The shopping you have done has been completed with a credit card payment.";
            }
            else if (PaymentType == "Bank Card")
            {
                ePosta.Body = "The shopping you have done has been completed with a bank card payment.";
            }
            else
            {
                ePosta.Body = "The shopping you have done has been completed with a cash payment.";
            }
            //object userState = ePosta;

            smtp.SendCompleted += (s, e) =>
            {
                MailMessage mail = (MailMessage)e.UserState;

                if (e.Cancelled)
                {
                    MessageBox.Show("[{0}] E-mail is cancelled." + mail.Subject);
                }

                if (e.Error != null)
                {
                    MessageBox.Show("[{0}] Error sending e-mail! Error message: [{1}]" + mail.Subject, e.Error.ToString());
                }
                else
                {
                    MessageBox.Show("[{0}] E-mail is sent on." + mail.Subject);
                }

            };
            try
            {
                smtp.Send(ePosta);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.Message, "E-MAIL SENDING ERROR !", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        public ShoppingCard() { }
    }
}
