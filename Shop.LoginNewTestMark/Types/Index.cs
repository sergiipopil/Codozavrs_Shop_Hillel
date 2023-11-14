using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Login.Types
{
    internal class Index
    {
        public class UserRegistration
        {
            public string Password { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string DateofBirth { get; set; }
        }
    }
}
