using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Shop.Classes;
using Moq;

namespace Shop.Tests
{
    [TestFixture]
    public class ProductManagerTest
    {
        [Test]
        public void CheckGetProduct()
        {

            //Arrage
            ProductManager productManager = new ProductManager();
            Product product = new Product()
            {
                Count = 1,
                Id = 1,
                Price = 1,
                Title = "Title",
                Weight = 1,
            };
            var productList = new List<Product>{ product };
            productManager.ProductList = productList;

            //Act
            var productTest = productManager.GetProduct(1);

            //Assert
            Assert.IsNotNull(productTest);
            Assert.AreEqual(productTest.Title, product.Title);

        }
        [Test]
        public void CheckDeleteProduct() {
            ProductManager productManager = new ProductManager();
            Product product = new Product()
            {
                Count = 1,
                Id = 1,
                Price = 1,
                Title = "Title",
                Weight = 1,
            };
            var productList = new List<Product> { product };
            productManager.ProductList = productList;

            //Act
            bool result = productManager.DeleteProduct(1);

            //Assert
            Assert.AreEqual(result, true);
        }
        [Test]
        public void CheckDeleteProductByTitle() {
            ProductManager productManager = new ProductManager();
            Product product = new Product()
            {
                Count = 1,
                Id = 1,
                Price = 1,
                Title = "Title",
                Weight = 1,
            };
            var productList = new List<Product> { product };
            productManager.ProductList = productList;

            //Act
            bool result = productManager.DeleteProduct("Title");

            //Assert
            Assert.AreEqual(result, true);
        }
    }
}
