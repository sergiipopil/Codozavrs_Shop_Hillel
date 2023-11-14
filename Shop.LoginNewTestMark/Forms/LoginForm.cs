using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Shop.Login.Forms
{
    public class LoginForm
    {
        private const string ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=Hyllel_Migrations;Integrated Security=True";

        public bool TryLogin()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                do
                {
                    Console.WriteLine("Enter your first name:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Enter your password:");
                    string password = Console.ReadLine();

                    if (TryLogin(firstName, password, connection))
                    {
                        Console.WriteLine($"Login successful. Hello {firstName}");

                        Console.WriteLine("Would you like to change your password or username \"password / username\"?");
                        string userPassword = Console.ReadLine();

                        if (userPassword == "password")
                        {
                            ResetPassword(firstName, connection);
                        }

                        if (userPassword == "username")
                        {
                            ResetUserName(firstName, connection);
                        }

                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Login failed. Try again.");
                    }
                } while (true);
            }
        }

        public class UserInfo
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }

        private bool TryLogin(string firstName, string password, SqlConnection connection)
        {
            var user = connection.QueryFirstOrDefault<UserInfo>("SELECT Name, Password FROM SignupTableMigration WHERE Name = @Name AND Password = @Password",
                new { Name = firstName, Password = password });

            return user != null;
        }

        private void ResetPassword(string firstName, SqlConnection connection)
        {
            const int MaxAttemptsForReset = 5;

            for (int resetAttempts = MaxAttemptsForReset; resetAttempts > 0; resetAttempts--)
            {
                string newPassword;
                do
                {
                    Console.WriteLine("Enter your new password: ");
                    newPassword = Console.ReadLine();
                } while (!IsValidPassword(newPassword));

                var parameters = new
                {
                    NewPassword = newPassword,
                    UserId = firstName
                };

                int rowsAffected = connection.Execute("UPDATE SignupTableMigration SET Password = @NewPassword WHERE Name = @UserId", parameters);


                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Password for user {firstName} has been reset.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Password reset failed. You still have {resetAttempts - 1} chance(s)");
                }
            }
        }

        private void ResetUserName(string firstName, SqlConnection connection)
        {
            const int MaxAttemptsForReset = 5;

            for (int resetAttempts = MaxAttemptsForReset; resetAttempts > 0; resetAttempts--)
            {
                string newName;
                Console.WriteLine("Enter your new name: ");
                newName = Console.ReadLine();

                var parameters = new
                {
                    NewName = newName,
                    UserId = firstName
                };

                int rowsAffected = connection.Execute("UPDATE SignupTableMigration SET Name = @NewName WHERE Name = @UserId", parameters);

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Name for user {firstName} has been reset.");
                    break;
                }
                else
                {
                    Console.WriteLine($"Name reset failed. You still have {resetAttempts - 1} chance(s)");
                }
            }
        }

        private static bool IsValidPassword(string password)
        {
            // Ваша логика проверки пароля
            return !string.IsNullOrEmpty(password);
        }
    }
}
