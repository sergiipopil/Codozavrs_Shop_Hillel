using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    public class ShopManager
    {
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
            Console.WriteLine($"Store {shop.Name} is opened at the address {Shop.Location}, " +
                $"StoreId:{shop.ShopID}, Shop opened at: {openTime}");
        }

        public void Close(Shop shop)
        {
            try
            {
                shop.IsOpened = false;
                Console.WriteLine($"Store {shop.Name} is closed at the address {Shop.Location}, StoreId:{shop.ShopID}");
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
    }
}
