using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Shop.Login.Forms.BackLogic
{
    internal class LoginLogic
    {
        private const string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=Hyllel_Migrations;Integrated Security=True";

        public static bool TryLogin(string name, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var userList = connection.Query<RegistrationLogic>("SELECT * FROM SignUpTable").ToList();

                    RegistrationLogic user = userList.Find(u =>
                        u.Name == name &&
                        u.Password == password
                    );

                    return user != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool TryLoginWithDapper(string name, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Using Dapper to execute a parameterized query
                    var query = "SELECT * FROM SignUpTable WHERE Name = @Name AND Password = @Password";
                    var parameters = new { Name = name, Password = password };
                    var user = connection.QueryFirstOrDefault<RegistrationLogic>(query, parameters);

                    return user != null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
