
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Shop.Classes.forms;

namespace Shop.Classes.account
{
    //  Этот класс реализует проверяет полученные данные
    //  из LogginForm.cs и сопостовляет с данными которые я храню в json
    //  файле, если все ок и данные совпадают, то в LogginForm.cs пишу что 
    //  вход в аккаунт успешный
    //  ->

    internal class Loggin
    {
        //  Говорю праграмме какой файл искать
        private const string FileName = "UserData.json";

        public static bool TryLogin(string firstName, string password)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    throw new FileNotFoundException($"File {FileName} not found.");
                }

                string jsonFromFile = File.ReadAllText(FileName);
                List<Registration> userList = JsonConvert.DeserializeObject<List<Registration>>(jsonFromFile);

                //  Сверяю данные, после можна будет добовлять менять параметры.
                Registration user = userList.Find(u => u.FirstName == firstName && u.Password == password);

                return user != null;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool TryLogin(string firstName)
        {
            return TryLogin(firstName, "");
        }
    }
}
