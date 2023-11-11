using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskShop.Classes;

namespace Shop.Classes.Extensions
{
    public static class InfoSellerExtensions
    {
        public static void GetInfo(this Seller seller)
        {
            Console.WriteLine($"\nSeller information:\n\n" +
               $"Name - {seller.Name}\n" +
               $"Age - {seller.Age}\n");
        }
    }
}
