using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Login.Forms.BackLogic
{
    public class RegistrationLogic
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        private static readonly List<RegistrationLogic> users = new List<RegistrationLogic>();

        public string Notification { get; set; }

        public RegistrationLogic() { }

        public RegistrationLogic(string firstName, string lastName, string email, string password, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public void SaveUserData()
        {
            try
            {
                var user = new
                {
                    FirstName,
                    LastName,
                    Email,
                    Password,
                    PhoneNumber
                };

                string json = JsonConvert.SerializeObject(user);

                if (!File.Exists("UserData.json"))
                {
                    File.WriteAllText("UserData.json", "[" + json + "]");
                    Console.WriteLine("Data saved successfully");
                }
                else
                {
                    string jsonFromFile = File.ReadAllText("UserData.json");
                    List<RegistrationLogic> userList = JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromFile);

                    if (userList.Any(u => u.Email == Email))
                    {
                        Console.WriteLine("User with this email already exists.");
                    }
                    else
                    {
                        userList.Add(new RegistrationLogic(FirstName, LastName, Email, Password, PhoneNumber));
                        string updatedJson = JsonConvert.SerializeObject(userList, Newtonsoft.Json.Formatting.Indented);
                        File.WriteAllText("UserData.json", updatedJson);
                        Console.WriteLine("Data appended successfully");
                    }

                    userList.ForEach(u =>
                    {
                        Console.WriteLine("Data from user.json:");
                        Console.WriteLine($"Name: {u.FirstName}");
                        Console.WriteLine($"Ilchenko: {u.Email}");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred, please try later: {ex.Message}");
            }
        }

        public string GetUserData(string value)
        {
            Console.WriteLine($"Please enter your {value}");
            return Console.ReadLine();
        }

        public static List<RegistrationLogic> GetUsers() => users;

        public void PrintUsers()
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.FirstName}, Email: {user.Email}");
            }
        }
    }
}
