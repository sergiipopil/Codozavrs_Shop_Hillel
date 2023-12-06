using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Data;

namespace Shop.Classes
{
    public class ShopManager
    {
        DapperORM dapper = new DapperORM();
        public void Open(Shop shop)
        {
            try
            {
                try
                {
                    shop.IsOpened = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Inner FormatException: " + ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("Inner ArgumentNullException: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Outer General Exception: " + ex.Message);
            }
        }

        public void Open(Shop shop, string openTime)
        {
            shop.IsOpened = true;
            Console.WriteLine($"Store {shop.Name} is opened at the address {shop.Location}, " +
                $"StoreId:{shop.ID}, Shop opened at: {openTime}");
        }

        public void Close(Shop shop)
        {
            try
            {
                shop.IsOpened = false;
                Console.WriteLine($"Store {shop.Name} is closed at the address {shop.Location}, StoreId:{shop.ID}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Close FormatException: " + ex.Message);
                //Saving StackTrace
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Close General Exception: " + ex.Message);
                //Griding StackTrace
                throw ex;
            }
        }

        public async Task DisplayShopInfo()
        {
            Console.Write("Enter shop ID: ");
            if (int.TryParse(Console.ReadLine(), out int shopId))
            {
                var shop = await GetShopById(shopId);
                if (shop != null)
                {
                    Console.WriteLine($"ID: {shop.ID}, Name: {shop.Name}, Location: {shop.Location}, IsOpened: {shop.IsOpened}");
                }
                else
                {
                    Console.WriteLine("Shop not found.");
                }
            }
            else
            {
                Console.WriteLine("Incorrect ID. Please, try again.");
            }
        }

        public async Task<Shop> GetShopById(int ID)
        {
            Shop shop = await dapper.ExecReturnObject<Shop>("GetShopById", new { ShopId = ID });
            return shop;
        }

        public async Task DeleteShopById(int ID)
        {
            await dapper.ExecWithoutReturn("DeleteShopById", new { ShopId = ID });
        }

        public void DeleteShopInfo()
        {
            Console.Write("Enter shop ID: ");
            if (int.TryParse(Console.ReadLine(), out int shopId))
            {
                DeleteShopById(shopId);
                Console.WriteLine("Shop deleted");
            }
            else
            {
                Console.WriteLine("Incorrect ID. Please, try again.");
            }
        }

        public async Task UpdateShop()
        {
            Console.Write("Enter shop ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int shopId))
            {
                var existingShop = await GetShopById(shopId);

                if (existingShop != null)
                {
                    Console.Write("Enter new shop name (press Enter to keep the existing value): ");
                    string newName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        newName = existingShop.Name;
                    }

                    Console.Write("Enter new shop location (press Enter to keep the existing value): ");
                    string newLocation = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newLocation))
                    {
                        newLocation = existingShop.Location;
                    }

                    await dapper.ExecWithoutReturn("UpdateShopById",
                        new
                        {
                            ShopId = shopId,
                            Name = newName,
                            Location = newLocation
                        });

                    Console.WriteLine("Shop updated successfully");
                }
                else
                {
                    Console.WriteLine("Shop not found.");
                }
            }
            else
            {
                Console.WriteLine("Incorrect ID. Please, try again.");
            }
        }

        public async Task CreateShop()
        {
            Console.Write("Enter new shop name: ");
            string name = Console.ReadLine();

            Console.Write("Enter shop location: ");
            string location = Console.ReadLine();

            Console.Write("Is the shop opened? (true/false): ");
            bool isOpened;
            string userInput = Console.ReadLine();

            if (bool.TryParse(userInput, out isOpened))
            {
                await dapper.ExecWithoutReturn("CreateShop",
                    new
                    {
                        Name = name,
                        Location = location,
                        IsOpened = isOpened
                    });

                Console.WriteLine("Shop created successfully");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
            }

        }
    }
}
