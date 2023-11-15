using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Login.Connection
{
    public static class CommonUtility
    {
        public static string GetDatabaseConnectionString()
        {
            InitializeConfiguration();
            return configuration.GetConnectionString("DefaultConnection");
        }

        private static IConfiguration configuration;

        private static void InitializeConfiguration()
        {
            string projectDirectory = GetProjectDirectory();
            string jsonFilePath = Path.Combine(projectDirectory, "Connection", "myappsettings.json");

            configuration = new ConfigurationBuilder()
                .AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true)
                .Build();
        }

        private static string GetProjectDirectory()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        }
    }
}
