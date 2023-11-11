using Newtonsoft.Json;
using System.IO;
using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Classes.account
{
    //  Этот класс реализует базовую логику регистрации пользователя,
    //  передавая программе данные о пользователе, которые находятся ниже.
    //  Обработка данных пользователя осуществляется в файле 'RegistrationForm.cs'.
    //  Здесь кратко описан процесс добавления наших данных в файл.json,
    //  можно сказать, что это аналог базы данных.
    //  ->

    internal class Registration
    {
        //  Базовые параметры которые принимает программа
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        private static readonly List<Registration> users = new List<Registration>();
        // ->


        //  Это на будущее, если у нас пользователь захочит получать уведомления о 
        //  скидках.... Крч на будущее
        public string Notification { get; set; }
        // ->

        public Registration() { } // Конструктор за замовчуванням

        public Registration(string firstName, string lastName, string email, string password, string phoneNumber)
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
                    List<Registration> userList = JsonConvert.DeserializeObject<List<Registration>>(jsonFromFile);

                    if (userList.Any(u => u.Email == Email))
                    {
                        Console.WriteLine("User with this email already exists.");
                    }
                    else
                    {
                        userList.Add(new Registration(FirstName, LastName, Email, Password, PhoneNumber));
                        string updatedJson = JsonConvert.SerializeObject(userList, Formatting.Indented);
                        File.WriteAllText("UserData.json", updatedJson);
                        Console.WriteLine("Data appended successfully");
                    }

                    // Тут я показываю кто у меня есть из пользователей в базе данных
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
            return Console.ReadLine(); // Повертає введене значення
        }

        public static List<Registration> GetUsers() => users;

        public void PrintUsers()
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.FirstName}, Email: {user.Email}");
            }
        }
    }
}
