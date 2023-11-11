using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    internal record Record_Seller
    {
        public int id { get; init; }
        public string name { get; init; }
        public DateTime HireDate { get; init; }

        public Record_Seller(int id, string name, DateTime hireDate)
        {
            this.id = id;
            this.name = name;
            this.HireDate = hireDate;
        }
    }
}
