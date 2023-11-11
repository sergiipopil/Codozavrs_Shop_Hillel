using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerLogic.Classes
{
    public class CustomerManagerAbstract<T> where T : CustomerAbstract
    {
        public void GetBasicInformationCustomer(T customer)
        {
            Console.WriteLine($"\nCustomer information:\n");
            customer.GetFullName();
            Console.WriteLine($"Age - {customer.Age}\n" +
               $"Phone number - {customer.NumberPhone}\n");
        }
    }
}
