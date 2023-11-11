using System;
using System.Collections.Generic;

using Shop.Login.Forms.BackLogic.Validation;
using Shop.Login.Forms.BackLogic.Validation;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shop.Login.Forms.BackLogic.AdditionalLogic
{
    class NameResetLogic : LoginLogic
    {
        private string? UserName { get; set; }

        public override string GetNewPassword(string name)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Your name has been successfully changed.");
            return UserName;
        }

        public override string AdditionalProperty => "please don't tell your new information";

        private static bool GetNewUserNameCheck(string name, string newName)
        {
            while (!ValidationHelper.IsValidNewUserName(newName))
            {
                Console.WriteLine("Please enter your new name");
                newName = Console.ReadLine();
            }

            JsonConfiguretion(name, newName);
            return true;
        }

        public static bool ResetUserName(string name, string newName)
        {
            GetNewUserNameCheck(name, newName);
            return true;
        }
    }
};
