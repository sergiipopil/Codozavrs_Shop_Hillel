﻿using CustomerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Shop.Web;

namespace Shop.Classes
{
    internal class CustomerManager : CustomerManagerAbstract<CustomerAbstract>
    {
        Basket basket = new Basket();

        
        public void GetStoreCard(int storeCard)
        {
            Console.WriteLine($"Your store card - {storeCard}!");
        }

        public void PutDownInBasket(Product product, int productCount)
        {

            Product existingProductInBasket = basket.BasketItems.FirstOrDefault(p => p.Id == product.Id);
            if (existingProductInBasket != null)
            {
                existingProductInBasket.Count = existingProductInBasket.Count + productCount;
            }
            else
            {
                Product copyProduct = new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Count = productCount,
                    Price = product.Price,
                    Weight = product.Weight
                };
                basket.BasketItems.Add(copyProduct);
                product.Count -= productCount;
            }
        }

        public void BuyProduct(ProductManager product)
        {
            int attempts = 0;
            int maxAttempts = 3;

           product.ShowProductsList();
            try
            {
                try
                {
                    while (attempts < maxAttempts)
                    {
                        Console.Write("Enter Id of product which you want to buy: ");
                        int productId = int.Parse(Console.ReadLine());
                        bool isCorrectProductId = product.ProductList.FirstOrDefault(x => x.Id == productId) != null;
                        // bool isCorrectProductId = int.TryParse(Console.ReadLine(), out int productId);
                        // isCorrectProductId = product.ProductList.FirstOrDefault(x => x.Id == productId) != null;

                        if (isCorrectProductId)
                        {
                            Product selectedProduct = product.GetProduct(productId);
                            Console.Write("Enter count: ");
                            bool isCorrectCount = int.TryParse(Console.ReadLine(), out int productCount);
                            isCorrectCount = selectedProduct.Count >= productCount;

                            if (isCorrectCount)
                                PutDownInBasket(selectedProduct, productCount);
                            else
                            {
                                Console.WriteLine("There is not enough product in the store");
                                break;
                            }
                            GetBasketItems();
                            return;
                        }
                        ++attempts;
                        Console.WriteLine($"Error! Attempt {attempts} of {maxAttempts}.");
                    }
                }
                catch (ArgumentNullException ArgNullEx)
                {
                    Console.WriteLine(ArgNullEx.StackTrace);
                    Console.WriteLine(ArgNullEx.Message);
                    throw;
                }
                catch (KeyNotFoundException KeyFEx)
                {
                    Console.WriteLine(KeyFEx.StackTrace);
                    Console.WriteLine(KeyFEx.Message);
                    throw KeyFEx;
                }
            }catch(Exception Ex) 
            {
                Console.WriteLine(Ex.StackTrace);
                Console.WriteLine(Ex.Message);
            }
            
        }
        

        public void GetBasketItems()
        {
            Console.WriteLine();
            if (basket.BasketItems != null && basket.BasketItems.Count > 0)
            {
                Console.WriteLine("Products in basket\n");
                foreach (var product in basket.BasketItems)
                {
                    Console.WriteLine($"ID:{product.Id}\tTitle:{product.Title}\tCount:{product.Count}\tPrice:{product.Price}\t" +
                    $"Weight(grams):{product.Weight}\tExpiration:{product.Expiration:d}\n");
                }
            }
            else { Console.WriteLine("Basket is empty!"); }


        }

        public void GetBasketItems(string title)
        {
            var productsInBasket = basket.BasketItems.Where(x => x.Title.ToLower() == title.ToLower()).ToList();

            Console.WriteLine("Products in basket\n");

            if (productsInBasket.Count > 0)
            {
                foreach (var product in productsInBasket)
                {
                    Console.WriteLine($"ID:{product.Id}\tTitle:{product.Title}\tCount:{product.Count}\tPrice:{product.Price}\t" +
                    $"Weight(grams):{product.Weight}\tExpiration:{product.Expiration:d}\n");
                }
            }
            else
            {
                Console.WriteLine("Basket doesn't have this product!");
            }
        }


        public void DeleteProductFromBasket(ProductManager product)
        {
            int attempts = 0;
            int maxAttempts = 3;
            if (!basket.BasketItems.Any())
                return;

            while (attempts < maxAttempts)
            {
                Console.Write("Enter product`s id if you want to delete from basket: \n");
                bool isCorrectProductId = int.TryParse(Console.ReadLine(), out int productId);
                isCorrectProductId = basket.BasketItems.FirstOrDefault(x => x.Id == productId) != null;
                if (isCorrectProductId)
                {
                    Product deleteProductFromBasket = basket.BasketItems.FirstOrDefault(p => p.Id == productId);

                    Console.Write("Enter count: ");
                    bool isCorrectCount = int.TryParse(Console.ReadLine(), out int productCount);
                    isCorrectCount = deleteProductFromBasket.Count >= productCount;

                    Product productStore = product.ProductList.FirstOrDefault(p => p.Id == productId);

                    if (deleteProductFromBasket != null && isCorrectCount)
                    {
                        deleteProductFromBasket.Count -= productCount;
                        if (deleteProductFromBasket.Count == 0)
                            basket.BasketItems.Remove(deleteProductFromBasket);
                        productStore.Count+= productCount;
                    }if (!isCorrectCount)
                    {
                        Console.WriteLine("Error! Incorrect product count.");
                        break;
                    }
                    GetBasketItems();
                    return;
                }
                ++attempts;
                Console.WriteLine($"Error! Attempt {attempts} of {maxAttempts}.");

            }
        }

        public void GetCustomerById(int Id, string connectionStr)
        {

            using (var connection = new SqlConnection(connectionStr))
            {
                var sql = $@"EXEC hillel.GetCustomerById @Id={Id}";
                var result = connection.Query<Customer>(sql);
                foreach (var customer in result)
                {
                    Console.WriteLine($"Id: {customer.Id},Firstname: {customer.FirstName}, Lastname: {customer.LastName}");

                }
            }
        }

        async public Task CreateCustomerAsync(Customer customer, string connectionStr)
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                var parameters = new
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    BirthDay = customer.BirthDay,
                    Age = customer.Age,
                    Cash = customer.Cash
                };

                var sql = $@"EXEC hillel.CreateCustomer @FirstName = {parameters.FirstName}, @LastName={parameters.LastName}, @PhoneNumber={parameters.PhoneNumber},@BirthDay={parameters.BirthDay},@Age={parameters.Age},@Cash={parameters.Cash} ";

                var result =connection.Execute(sql, parameters);
                
            }
        }

        async public Task DeleteCustomerAsync(int Id, string connectionStr)
        {
            using (var connection = new SqlConnection(connectionStr))
            {
                var sql = $@"EXEC hillel.DeleteCustomerById @Id={Id}";
                var result = connection.Query(sql);
                
            }
        }

        public async Task UpDateCustomerByIdAsync(int id, string firstName, string lastName, string phoneNumber, DateTime birthDay, int? age, int? cash)
        {
            using (var connection = StartConfig._dbContext.CreateRemoteConnection())
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@ID", id }
                };

                var sql = "EXEC hillel.UpDateCustomerById @ID = @ID";

                if (!string.IsNullOrEmpty(firstName))
                {
                    parameters.Add("@FirstName", firstName);
                    sql += ", @FirstName = @FirstName";
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    parameters.Add("@LastName", lastName);
                    sql += ", @LastName = @LastName";
                }

                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    parameters.Add("@PhoneNumber", phoneNumber);
                    sql += ", @PhoneNumber = @PhoneNumber";
                }

                if (birthDay != DateTime.MinValue)
                {
                    parameters.Add("@BirthDay", birthDay);
                    sql += ", @BirthDay = @BirthDay";
                }

                if (age != null)
                {
                    parameters.Add("@Age", age);
                    sql += ", @Age = @Age";
                }

                if (cash != null)
                {
                    parameters.Add("@Cash", cash);
                    sql += ", @Cash = @Cash";
                }

                connection.Execute(sql, parameters);
            }
        }

    }
}
