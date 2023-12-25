using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shop.Classes;

namespace Shop.Tests
{
    [TestFixture]
    internal class CustomerTests
    {
        [Test]
        public void PutDownInBasketTest()
        {
            // Arrange
            var cusomerManager = new CustomerManager();
            Product initialProduct = new Product { Id = 1, Title = "TestProduct", Count = 25, Price = 0, Weight = 0 };

            // Act
            cusomerManager.PutDownInBasket(initialProduct, 5);
            var resBasket = cusomerManager.GetCountItemById(initialProduct.Id);

            // Assert
            Assert.AreEqual(20, initialProduct.Count);
            Assert.AreEqual(5, resBasket);
        }

        [Test]
        public void GetAgeTest()
        {
            //Arrange
            var customer = new Customer("Ivan", "Ivanchenko", "+3806661177", 5000);
            var birthday = new DateTime(2002, 11, 19);

            //Act
            customer.BirthDay = birthday;
            var act = customer.GetAge();

            //Assert
            Assert.AreEqual(21, act);
        }

        [Test]
        [TestCase(17, false)]
        [TestCase(18, true)]
        [TestCase(19, true)]
        public void CustomerAdultTest(int age, bool expected)
        {
            //Arrange
            var customer = new Customer("Ivan", "Ivanchenko", "+3806661177", 5000);
            customer.Age = age;

            //Act
            var result = customer.CustomerAdult();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
