using System.Diagnostics.CodeAnalysis;
using Shop.Core.Classes;

namespace Shop.Classes
{
    public class Product:ProductBase
    {
        public override string Production => "TOV EUProducts";
        public const string Currency = "EUR";
        public readonly DateTime Expiration = DateTime.Now.AddMonths(1);
        public int Weight { get; set; }

        

        public override string GetCount()
        {
            return $"{Count} Items";
        }

        public new decimal GetPrice()
        {
            return base.GetPrice() + 1000;
        }

        public override string GetProductTitle()
        {
            return $"Product title: {Title}";
        }
    }
}
