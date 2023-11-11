using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskShop.Classes
{
    public class Seller
    {
        public readonly string Name;
        public const string ShopName = "Codozavrs shop";
        public required int Age { get; init; }
        public Seller(string name)
        {
            Name = name;
        }
    }
}