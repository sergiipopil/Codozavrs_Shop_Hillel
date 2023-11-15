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
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public bool IsOpened { get; set; }

        public Shop()
        {
            IsOpened = true;
        }
    }
}
