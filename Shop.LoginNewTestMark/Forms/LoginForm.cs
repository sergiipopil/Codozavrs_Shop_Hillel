using Shop.Login.Extensions;
using Shop.Login.Forms.BackLogic.AdditionalLogic;
using Shop.Login.Forms.BackLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Login.Forms.BackLogic.Validation;


namespace Shop.Login.Forms
{
    public record UserInfo(string FirstName, string Password);

    public class LoginForm
    {
        private const int MaxAttempts = 3;
        private readonly int MaxAttemptsForReset = 5;

        private const int NewMaxAttempts = 3;

        private int attempts;
        private int newAttempts;

        public LoginForm()
        {
            attempts = MaxAttempts;
            newAttempts = NewMaxAttempts;
        }

        private static bool IsValidPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && ValidationHelper.IsValidPassword(password);
        }



        public bool TryLogin()
        {
            do
            {
                Console.WriteLine("Enter your first name:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                if (LoginLogic.TryLogin(firstName, password))
                {
                    var fullNameConst = $"{firstName} Your password was right: {password}";
                    Console.WriteLine($"Login successful. Hello {fullNameConst}");

                    Console.WriteLine("Would you like to change your password or username \"password / username\"?");
                    string userPassword = Console.ReadLine();

                    if (userPassword == "password")
                    {
                        for (int resetAttempts = MaxAttemptsForReset; resetAttempts > 0; resetAttempts--)
                        {
                            string newPassword;
                            do
                            {
                                Console.WriteLine("Enter your password: ");
                                newPassword = Console.ReadLine();
                            } while (!IsValidPassword(newPassword));

                            var logic = new PasswordResetLogic();
                            bool success = PasswordResetLogic.ResetPassword(firstName, newPassword);

                            if (success)
                            {
                                Console.WriteLine($"Password for user {firstName} has been reset.");
                                string userPasswordAdd = logic.GetNewPassword(newPassword);
                                Console.WriteLine($"AdditionalProperty: {firstName}, {logic.AdditionalProperty}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Your passwords do not match, you still have {resetAttempts - 1} chance(s)");
                            }

                        }
                    }

                    if (userPassword == "username")
                    {
                        for (int resetAttempts = MaxAttemptsForReset; resetAttempts > 0; resetAttempts--)
                        {
                            string newName;
                            Console.WriteLine("Enter your name: ");
                            newName = Console.ReadLine();

                            var logic = new NameResetLogic();
                            bool success = NameResetLogic.ResetUserName(firstName, newName);

                            if (success)
                            {
                                Console.WriteLine($"Name for user {firstName} has been reset.");
                                string userPasswordAdd = logic.GetNewPassword(newName);
                                Console.WriteLine($"AdditionalProperty: {firstName}, {logic.AdditionalProperty}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Your passwords do not match, you still have {resetAttempts - 1} chance(s)");
                            }
                        }
                    }

                    return true;
                }
                else
                {
                    attempts--;
                    if (attempts > 0)
                    {
                        Console.WriteLine($"You have {attempts} attempts remaining.");
                    }
                    else
                    {
                        Console.WriteLine("Login failed. Try again later.");
                    }
                }
            } while (attempts > 0);

            return false;
        }


        public bool TryLogin(string firstName, string password)
        {
            attempts = MaxAttempts;

            LoginLogic.TryLogin(firstName, password);

            return true;
        }

        public void ResetAttempts()
        {
            attempts = MaxAttemptsForReset;
        }
    }
}
