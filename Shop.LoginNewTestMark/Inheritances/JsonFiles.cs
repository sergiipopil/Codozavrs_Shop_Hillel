using Newtonsoft.Json;
using Shop.Login.Forms.BackLogic.Validation;
using Shop.Login.Forms.BackLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Login.Inheritances
{
    public class JsonFiles
    {
        protected const string FileName = "UserData.json";

        protected static bool JsonConfiguretion(string name, string newName)
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    throw new FileNotFoundException($"File {FileName} not found.");
                }

                string jsonFromFile = File.ReadAllText(FileName);
                List<RegistrationLogic> userList = JsonConvert.DeserializeObject<List<RegistrationLogic>>(jsonFromFile);

                RegistrationLogic user = userList.Find(u => u.FirstName == name);

                if (newName != null && ValidationHelper.IsValidNewUserName(newName))
                {

                    user.FirstName = newName;

                    File.WriteAllText(FileName, JsonConvert.SerializeObject(userList, Newtonsoft.Json.Formatting.Indented));

                    return true;
                }
                else
                {
                    Console.WriteLine($"User with first name: \"{name}\" not found.");
                    return false;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (JsonException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
