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

namespace Shop.Login.Forms.BackLogic
{
    public class RegistrationLogic
    {
        private readonly string connectionString;

        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DateofBirth { get; set; }

        public RegistrationLogic() 
        {
            
        }

        public RegistrationLogic(string name, string email, string password, string dataofBirth)
        {
            Password = password;
            Name = name;
            Email = email;
            Password = password;
            DateofBirth = dataofBirth;
        }

        public void SaveUserData()
        {
            try
            {
                string publicConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=Hyllel_Migrations;Integrated Security=True";

                using (var connection = new SqlConnection(publicConnectionString))
                {
                    connection.Open();

                    // Вызов хранимой процедуры для вставки пользователя
                    connection.Execute("dbo.CreateUser",
                        new
                        {
                            Password,
                            Name,
                            Email,
                            DateofBirth
                        },
                        commandType: CommandType.StoredProcedure);

                    Console.WriteLine("Data appended successfully");

                    // Вывод данных из базы

                    var userList = connection.Query<RegistrationLogic>("SELECT Name, Email FROM SignupTableMigration").ToList();
                    userList.ForEach(u =>
                    {
                        Console.WriteLine("Data from SignupTableMigration:");
                        Console.WriteLine($"Name: {u.Name}, Email: {u.Email}");
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred, please try later: {ex.Message}");
            }
        }
    }
}
