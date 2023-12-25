using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace CustomerLogic.Classes
{


    public abstract class CustomerAbstract
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public abstract int Age { get; set; }
        public DateTime BirthDay { get; set; }

        public virtual bool CustomerAdult()
        {
            return true;
        }
        public void GetFullName()
        {
            Console.WriteLine($"Your full name is: {FirstName} {LastName}");
        }
        public abstract int GetAge();
    }
}
