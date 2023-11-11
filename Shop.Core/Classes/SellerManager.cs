using Shop.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskShop.Classes
{
    public class SellerManager
    {
        private readonly Seller _seller;
        private readonly ProductManager _productManager;

        public SellerManager(Seller seller, ProductManager productManager)
        {
            _seller = seller;
            _productManager = productManager;
        }

        public void SoldProduct(int productId, int productCount)
        {
            if (_productManager.ProductList != null && _productManager.ProductList.Count > 0)
            {
                Product? productExist = _productManager.ProductList.FirstOrDefault(x => x.Id == productId);
                if (productExist != null)
                {
                    if (productExist.Count > productCount)
                    {
                        productExist.Count -= productCount;
                        Console.WriteLine($"Total price for  {productCount}  {productExist.Title} = {productExist.Price * productCount}");
                        Console.WriteLine("Success!");
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, but this quantity of {productExist.Title} is not available at the moment.\n" +
                            $"Available only {productExist.Count}\n");
                    }
                }
            }
        }

        public void SoldProduct(string productName, int productCount)
        {
            var product = _productManager.ProductList.FirstOrDefault(x => x.Title == productName);
            if (product is not null)
            {
                SoldProduct(product.Id, productCount);
            }
            else
            {
                Console.WriteLine($"Sorry, but a such product by entered '{productName}' product name does not exist.");
            }
        }

        public void SoldProduct(Product product)
        {
            SoldProduct(product.Id, product.Count);
        }

    }
}