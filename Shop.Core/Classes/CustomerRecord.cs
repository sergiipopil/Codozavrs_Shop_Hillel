using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    internal record class CustomerRecord
    {
        public DateTime BirthDay { get; init; }
        public int Discount { get; init; }

        public CustomerRecord(DateTime birthDay, int discount)
        {
            this.BirthDay = birthDay;
            this.Discount = discount;
        }
    }
}
