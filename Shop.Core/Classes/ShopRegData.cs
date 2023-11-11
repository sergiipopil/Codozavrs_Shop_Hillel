using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    public record ShopRegData
    {
        public DateTime DateCreadeted { get; init; }
        public string RegNumber { get; init; }
        public string OwnerName { get; init; }
        public string OwnerSurName { get; init; }
    }
}
