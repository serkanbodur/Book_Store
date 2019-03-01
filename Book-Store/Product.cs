using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Book_Store
{
    abstract class Product
    {
        public string name;
        public int ID;
        public int price;
        abstract public void printProperties(DataGridView a);
    }
}