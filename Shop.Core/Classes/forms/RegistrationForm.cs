using PhoneNumbers;
using Shop.Classes.account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Classes.forms
{
    //  В этом классе я передаю данные в Registration.cs где после они будут добавленные 
    //  в файл json где я буду хранить информацию о пользователе, тут также данные будут
    //  валидироватся, перед отправкой в Registration.cs

    internal class RegistrationForm
    {
        private const int MaxAttempts = 3;
        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        //Validations
        private static bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => !char.IsLetterOrDigit(c));
        }

        private static bool IsValidEmail(string email)
        {
            if (email == null)
                return false;

            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        private static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && char.IsUpper(name[0]) && name.Substring(1).All(char.IsLetter);
        }




        //Registration Form
        public void NewRegistrationForm()
        {
            int attempts = MaxAttempts;

            string userFirstName = GetValidInput("First Name", IsValidName);

            string userLastName = GetValidInput("Last Name", IsValidName);

            string userEmail = GetValidInput("Email", IsValidEmail);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Email is valid.");

            string userPassword = GetValidPassword();

            string userCountryCode, userPhoneNumber;
            bool isPhoneNumberValid = GetValidPhoneNumber(out userCountryCode, out userPhoneNumber);

            if (isPhoneNumberValid)
            {
                Registration user = new Registration(
                    firstName: userFirstName,
                    lastName: userLastName,
                    email: userEmail,
                    password: userPassword,
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




        private string GetValidInput(string fieldName, Func<string, bool> validator)
        {
            string userInput;
            do
            {
                Console.WriteLine($"Please enter your {fieldName}");
                userInput = Console.ReadLine();
            } while (!validator(userInput));

            return userInput;
        }

        private string GetValidPassword()
        {
            string userPassword;
            bool isPasswordValid = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter your Password | " +
                                  "It must contain: number, capital letter, symbol and be at least 8 characters:");
                userPassword = Console.ReadLine();

                if (IsValidPassword(userPassword))
                {
                    isPasswordValid = true;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Password is valid.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid password. Please try again.");
                }
            } while (!isPasswordValid);

            return userPassword;
        }

        private bool GetValidPhoneNumber(out string countryCode, out string phoneNumber)
        {
            int attempts = MaxAttempts;

            bool isPhoneNumberValid = false;
            countryCode = string.Empty;
            phoneNumber = string.Empty;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please enter your Country Code | For example '+380':");
                countryCode = Console.ReadLine();

                Console.WriteLine("Please enter your Phone Number | For example '500000000':");
                string userPhoneNumberNew = Console.ReadLine();

                try
                {
                    PhoneNumber number = phoneNumberUtil.Parse(countryCode + userPhoneNumberNew, null);

                    if (number.NationalNumber.ToString().Length < 7)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid phone number. It's too short. Please try again.");
                        continue;
                    }

                    phoneNumber = userPhoneNumberNew;
                    Console.WriteLine("Phone number: " + phoneNumber);
                    isPhoneNumberValid = true;
                    break;
                }
                catch (NumberParseException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error in parsing number: " + e.Message);
                    attempts--;
                    Console.WriteLine("You have " + attempts + " attempt(s) left.");
                    isPhoneNumberValid = false;
                }
            } while (attempts > 0);

            return isPhoneNumberValid;
        }
    }
}
