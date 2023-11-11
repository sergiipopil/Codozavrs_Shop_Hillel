using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Login.Inheritances;
using Shop.Login.Forms.BackLogic.Validation;

namespace Shop.Login.Forms.BackLogic
{
    internal abstract class LoginLogic : JsonFiles
    {
        public static bool TryLogin(string firstName, string password)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    throw new FileNotFoundException($"File {FileName} not found.");
                }

                string jsonFromFile = File.ReadAllText(FileName);
                List<RegistrationLogic> userList = JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromFile);

                RegistrationLogic user = userList.Find(u =>
                    (u.FirstName == firstName || u.PhoneNumber == firstName) &&
                    u.Password == password
                );
                // RegistrationLogic user = userList.Find(u => u.FirstName == firstName && u.Password == password);

                return user != null;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
                Console.WriteLine("Call stack:");
                Console.WriteLine(ex.StackTrace);
                SaveStackTrace(ex.StackTrace);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                SaveStackTrace(ex.StackTrace);
                return false;
            }
        }

        public static bool TryLogin(string firstName)
        {
            return TryLogin(firstName, "");
        }

        public abstract string GetNewPassword(string password);

        public abstract string AdditionalProperty { get; }

        private static void SaveStackTrace(string stackTrace)
        {
            string fileName = "StackTraceLog.txt";

            try
            {
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName)) { }
                }

                File.WriteAllText(fileName, stackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the stack trace: {ex.Message}");
            }
        }
    }
}
