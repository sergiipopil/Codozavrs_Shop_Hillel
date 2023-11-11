using Newtonsoft.Json;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Login.Forms.BackLogic.Validation
{
    public static class ValidationHelper
    {
        private const string MyFileName = "UserData.json";
        private const int MaxAttempts = 3;
        private static readonly PhoneNumberUtil PhoneNumberUtil = PhoneNumberUtil.GetInstance();


        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => !char.IsLetterOrDigit(c));
        }


        public static bool IsValidUserName(string userName)
        {
            return !string.IsNullOrEmpty(userName) &&
                   char.IsUpper(userName[0]) &&
                   userName.Substring(1).All(char.IsLetter) &&
                   userName.Length > 0 &&
                   userName.Any(char.IsUpper);
        }


        public static bool IsValidNewUserName(string userName)
        {
            return userName.Length > 0 && userName.Any(char.IsUpper);
        }


        public static bool GetNewValidPhoneNumber(out string countryCode, out string phoneNumber)
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
                    PhoneNumber number = PhoneNumberUtil.Parse(countryCode + userPhoneNumberNew, null);
                    string testPhone = countryCode + userPhoneNumberNew;
                    if (number.NationalNumber.ToString().Length < 7)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid phone number. It's too short. Please try again.");
                        continue;
                    }

                    if (File.Exists("UserData.json"))
                    {
                        string jsonFromEmail = File.ReadAllText("UserData.json");
                        List<RegistrationLogic> registrationLogic = JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromEmail);

                        bool phoneExists = registrationLogic.Exists(item => item.PhoneNumber == testPhone);

                        if (phoneExists)
                        {
                            throw new EmailAlreadyExistException("Email already exists, please try another.");
                            break;
                        }

                        phoneNumber = userPhoneNumberNew;
                        Console.WriteLine("Phone number: " + phoneNumber);
                        isPhoneNumberValid = true;
                        break;
                    }
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

        public static bool IsValidUserEmail(string userEmail)
        {
            if (userEmail == null) return false;

            var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            if (!Regex.IsMatch(userEmail, pattern))
            {
                // throw new Exception("Invalid email format");
                throw new InvalidEmailException("Invalid email format");
            }

            if (File.Exists("UserData.json"))
            {
                string jsonFromEmail = File.ReadAllText(MyFileName);
                List<RegistrationLogic> registrationLogic = JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromEmail);
                RegistrationLogic myUser = registrationLogic.Find(item => item.Email == userEmail);


                if (myUser != null)
                {
                    // throw new Exception("Email already exists, please try letter.");
                    throw new EmailAlreadyExistException("Email already exists, please try letter.");
                }
            }

            return Regex.IsMatch(userEmail, pattern);
        }


        public static string GetValidInput(string fieldName, Func<string, bool> validator)
        {
            string userInput;
            do
            {
                Console.WriteLine($"Please enter your {fieldName}");
                userInput = Console.ReadLine();
            } while (!validator(userInput));

            return userInput;
        }


        public static string IsValidUserPassword(string userPassword)
        {
            bool isPasswordValid = false;

            if (IsValidPassword(userPassword))
            {
                Console.WriteLine("Password is valid.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid password. Please try again.");

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
            }

            return userPassword;
        }


        public static bool GetValidPhoneNumber(out string countryCode, out string phoneNumber)
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
                    PhoneNumber number = PhoneNumberUtil.Parse(countryCode + userPhoneNumberNew, null);

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

        public class InvalidEmailException : Exception
        {
            public InvalidEmailException(string message) : base(message)
            {

            }
        }

        public class EmailAlreadyExistException : Exception
        {
            public EmailAlreadyExistException(string message) : base(message)
            {

            }
        }
    }
}
