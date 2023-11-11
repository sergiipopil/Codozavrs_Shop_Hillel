using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes.Extensions
{
    public static class OpenExtensions
    {
        public static string GetStatusMessage(this Shop shop)
        {
            if (shop.IsOpened)
            {
                return $"Store {shop.Name} is open at {Shop.Location}.";
            }
            else
            {
                return $"Store {shop.Name} is closed at {Shop.Location}.";
            }
        }
    }
}
