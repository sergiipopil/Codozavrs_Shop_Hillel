using Newtonsoft.Json;
using Shop.Login.Forms.BackLogic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Login.Forms.BackLogic.AdditionalLogic
{
    class PasswordResetLogic : LoginLogic
    {
        public string Password { get; set; }

        public static bool IsMatch(string password)
        {
            Console.WriteLine("Please re-enter your password");
            string newPassword = Console.ReadLine();

            return !string.IsNullOrEmpty(password) && password == newPassword;
        }

        public override string GetNewPassword(string password)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Your password {password} is match with {password}");
            return Password;
        }

        public override string AdditionalProperty => "please don't tell anyone your password";

        private static void SaveUserList(List<RegistrationLogic> userList)
        {
            File.WriteAllText(FileName, JsonConvert.SerializeObject(userList, Newtonsoft.Json.Formatting.Indented));
        }

        private static List<RegistrationLogic> LoadUserList()
        {
            if (!File.Exists(FileName))
            {
                throw new FileNotFoundException($"File {FileName} not found.");
            }
            string jsonFromFile = File.ReadAllText(FileName);

            return JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromFile);
        }

        public static bool ResetPassword(string firstName, string newPassword)
        {
            try
            {
                try
                {
                    List<RegistrationLogic> userList = LoadUserList();

                    RegistrationLogic user = userList.Find(u => u.FirstName == firstName);

                    if (user != null && IsMatch(newPassword))
                    {
                        user.Password = newPassword;
                        SaveUserList(userList);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"User with first name: \"{firstName}\" not found.");
                        return false;
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured: {ex}");
                return false;
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }
        }
    }
}
