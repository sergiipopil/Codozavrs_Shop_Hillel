using Dapper;
using Microsoft.Extensions.Configuration;
using Shop.Login.Connection;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Shop.Login.Forms.BackLogic
{
    internal class LoginLogic
    {
        public static bool TryLogin(string name, string password)
        {
            try
            {
                string databaseConnection = CommonUtility.GetDatabaseConnectionString();

                using (var connection = new SqlConnection(databaseConnection))
                {
                    connection.Open();

                    var userList = connection.Query<RegistrationLogic>("SELECT * FROM hillel.SignUpTable WHERE Name = @Name AND Password = @Password",
                        new { Name = name, Password = password }).ToList();

                    return userList.Any();
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
