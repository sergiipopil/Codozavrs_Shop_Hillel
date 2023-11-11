using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    internal class Basket
    {        
        public List<Product> BasketItems { get; private set; }
        public Basket()
        {
            BasketItems = new List<Product>();
        }
    }
}
