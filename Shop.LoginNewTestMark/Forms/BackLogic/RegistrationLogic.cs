using Newtonsoft.Json;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Shop.Login.Connection;

namespace Shop.Login.Forms.BackLogic
{
    public class RegistrationLogic
    {
        private readonly string connectionString;

        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateofBirth { get; set; }

        public RegistrationLogic() { }

        public RegistrationLogic(string name, string email, string password, string dataofBirth)
        {
            Password = password;
            Name = name;
            Email = email;
            Password = password;
            DateofBirth = dataofBirth;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public void SaveUserData(string name, string email, string password, string dateOfBirth)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(dateOfBirth))
                {
                    Console.WriteLine("Invalid input data");
                    return;
                }

                string databaseConnection = CommonUtility.GetDatabaseConnectionString();

                using (var connection = new SqlConnection(databaseConnection))
                {
                    connection.Open();

                    // Хеширование пароля
                    string hashedPassword = HashPassword(password);

                    connection.Execute("hillel.CreateUser",
                        new
                        {
                            Password = hashedPassword,
                            Name = name,
                            Email = email,
                            DateofBirth = dateOfBirth
                        },
                        commandType: CommandType.StoredProcedure);

                    Console.WriteLine("Data appended successfully");

                    var userList = connection.Query<RegistrationLogic>("SELECT Name, Email FROM hillel.SignUpTable").ToList();
                    userList.ForEach(u =>
                    {
                        Console.WriteLine("Data from SignUpTable:");
                        Console.WriteLine($"Name: {u.Name}, Email: {u.Email}");
                    });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred, please try later: {ex.Message}");
            }
        }

    }
}
