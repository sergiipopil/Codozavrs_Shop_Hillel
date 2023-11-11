using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Classes.Extensions;
using Shop.Core.Classes;

namespace Shop.Classes
{
    public class ProductManager:ProductManagerBase<ProductBase>, IProductManager
    {
        public List<Product> ProductList { get; set; }
        public void ShowProductsList()
        {
            foreach (var product in ProductList)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"ID:{product.Id}\tTitle:{product.Title}\tCount:{product.Count}\tPrice:{product.Price}\t" +
                        $"Weight:{product.Weight.ToGrams()}\tExpiration:{product.Expiration:d}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        public void GetProductDetail(string title)
        {
            var productDetail = ProductList.Where(x => x.Title.ToLower() == title.ToLower()).ToList();
            if (productDetail.Count > 0)
            {
                foreach (var product in productDetail)
                {
                    Console.WriteLine($"ID:{product.Id}\tTitle:{product.Title}\tCount:{product.Count}\tPrice:{product.Price}\t" +
                        $"Weight:{product.Weight.ToKg()}\tExpiration:{product.Expiration:d}");
                }
            }
            else
            {
                Console.WriteLine($"We don`t have product with name {title}");
            }
        }

        public void GetProductDetail(int id)
        {
            var productDetail = ProductList.FirstOrDefault(x => x.Id == id);
            if (productDetail != null)
            {
                Console.WriteLine($"ID:{productDetail.Id}\tTitle:{productDetail.Title}\tCount:{productDetail.Count}\tPrice:{productDetail.Price}\t" +
                        $"Weight:{productDetail.Weight.ToKg()}\tExpiration:{productDetail.Expiration:d}");
            }
            else
            {
                Console.WriteLine($"We don`t have product with id {id}");
            }
        }
        
        public void AddNewProduct(Product product)
        {
            int id = ProductList.Max(x => x.Id);
            product.Id = ++id;
            ProductList.Add(product);
        }
        public Product? GetProduct(int productId)
        {
            return ProductList.FirstOrDefault(x => x.Id == productId);
        }
        public void DeleteProduct(int productId)
        {
            if (ProductList != null && ProductList.Count > 0)
            {
                Product deletedProduct = ProductList.FirstOrDefault(x => x.Id == productId);
                if (deletedProduct != null)
                {
                    ProductList.Remove(deletedProduct);
                }
            }
        }
        public void DeleteProduct(string title)
        {
            if (ProductList != null && ProductList.Count > 0)
            {
                var deletedProducts = ProductList.Where(x => x.Title == title);
                if (deletedProducts != null)
                {
                    foreach (var item in deletedProducts)
                    {
                        ProductList.Remove(item);
                    }
                }
            }
        }
        public void DeleteExpirationProducts()
        {
            if (ProductList != null && ProductList.Count > 0)
            {
                var deletedProducts = ProductList.Where(x => x.Expiration < DateTime.Now);
                if (deletedProducts != null)
                {
                    foreach (var item in deletedProducts)
                    {
                        ProductList.Remove(item);
                    }
                }
            }
        }
    }
}
