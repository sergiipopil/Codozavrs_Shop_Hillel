using PhoneNumbers;
using Shop.Login.Forms.BackLogic;
using Shop.Login.Forms.BackLogic.Validation;
using Shop.Login.Forms.BackLogic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shop.Login.Forms
{
    public class RegistrationForm
    {
        private const int MaxAttempts = 3;


        public void NewRegistrationForm()
        {
            int attempts = MaxAttempts;

            string userFirstName = ValidationHelper.GetValidInput("First Name", ValidationHelper.IsValidUserName);

            string userLastName = ValidationHelper.GetValidInput("Last Name", ValidationHelper.IsValidUserName);

            string userEmail = ValidationHelper.GetValidInput("Email", ValidationHelper.IsValidUserEmail);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Email is valid.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter your Password | " +
                              "It must contain: number, capital letter, symbol and be at least 8 characters:");
            string userValueInput = Console.ReadLine();
            string userPassword = ValidationHelper.IsValidUserPassword(userValueInput);

            string userCountryCode, userPhoneNumber;
            bool isPhoneNumberValid = ValidationHelper.GetNewValidPhoneNumber(out userCountryCode, out userPhoneNumber);

            //string phoneResult = "+" + userCountryCode + userPhoneNumber;

            if (isPhoneNumberValid)
            {
                RegistrationLogic user = new RegistrationLogic(
                    firstName: userFirstName,
                    lastName: userLastName,
                    email: userEmail,
                    password: userPassword,
                    //phoneNumber: phoneResult
                    phoneNumber: userCountryCode + userPhoneNumber
                );
                user.SaveUserData();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("All attempts have been exhausted. Registration failed.");
            }
        }
    }
}
