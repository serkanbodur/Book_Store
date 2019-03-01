using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store
{
    class ItemToPurchase
    {
        private string product;
        public string Product
        {
            get { return product; }
            set { product = value; }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public ItemToPurchase() { }

    }

}

