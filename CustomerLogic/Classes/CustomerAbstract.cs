using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace CustomerLogic.Classes
{


    public abstract class CustomerAbstract
    {


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required long NumberPhone { get; set; }
        public abstract int Age { get; }

        public virtual string CustomerAdult()
        {
            return "You aren`t adult!";
        }
        public void GetFullName()
        {
            Console.WriteLine($"Your full name is: {FirstName} {LastName}");
        }
        public abstract int GetAge();
    }
}
