using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    public class Shop
    {
        public required string Name = "Codozavrs Shop";
        public const string Location = "Kyiv, Khreshchatyk 54";
        public readonly int ShopID = 1;
        public bool IsOpened { get; set; }

        [SetsRequiredMembers]
        public Shop()
        {
            IsOpened = true;
        }
    }
}
