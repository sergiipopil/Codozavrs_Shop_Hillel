using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes.Extensions
{
    public static class WeigthExtensions
    {
        public static string ToGrams(this int weightInGrams)
        {
            return $"{weightInGrams} grams";
        }
        public static string ToKg(this int weightInGrams)
        {
            return $"{(decimal)weightInGrams / 1000} kgs";
        }
    }
}
