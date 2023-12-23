using CustomerLogic.Classes;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Classes
{
    public class Customer : CustomerAbstract
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
        public Customer(string firstName, string lastName, string phoneNumber, decimal cash)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Cash = cash;
        }
        private int age;
        public override int Age { get { return age; } set { age = value; } }
        CustomerRecord CustomerRecord { get; }

        public override int GetAge()
        {
            DateTime today = new DateTime(2023, 12, 21);
            age = today.Year - BirthDay.Year;
            return age;
        }
        public override bool CustomerAdult()
        {
            return Age < 18 ? false : true;
        }

        public new void GetFullName()
        {
            Console.WriteLine($"You are {LastName} {FirstName}");
        }
    }
}
