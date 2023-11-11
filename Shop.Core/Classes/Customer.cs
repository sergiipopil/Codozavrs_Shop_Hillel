using CustomerLogic.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    internal class Customer : CustomerAbstract
    {
        private decimal _cash;
        public decimal Cash
        {
            get
            { return _cash; }
            set
            { _cash = value; }
        }
        public const int storeCard = 123456789;

        [SetsRequiredMembers]
        public Customer(string firstName, string lastName, long numberPhone, decimal cash, CustomerRecord customerRecord)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.NumberPhone = numberPhone;
            this.Cash = cash;
            this.CustomerRecord = customerRecord;
        }
        private int age;
        public override int Age { get { return age; } }
        CustomerRecord CustomerRecord { get; }

        public override int GetAge()
        {
            DateTime today = DateTime.Today;
            age = today.Year - CustomerRecord.BirthDay.Year;
            return age;
        }
        public override string CustomerAdult()
        {
            return Age < 18 ? "You aren`t adult!" : "You are adult!";
        }

        public new void GetFullName()
        {
            Console.WriteLine($"You are {LastName} {FirstName}");
        }
    }
}
