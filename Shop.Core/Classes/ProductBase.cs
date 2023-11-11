using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Classes
{
    public abstract class ProductBase
    {   
        public abstract string Production { get; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public abstract string GetProductTitle();
        public virtual string GetCount()
        {
            return Count.ToString();
        }
        public decimal GetPrice()
        {
            return Price;
        }
    }
}
