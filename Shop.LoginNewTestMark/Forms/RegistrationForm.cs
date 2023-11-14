using PhoneNumbers;
using Shop.Login.Forms.BackLogic;
using Shop.Login.Forms.BackLogic.Validation;
using Shop.Login.Forms.BackLogic.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator;
using Microsoft.Extensions.Configuration;

namespace Shop.Login.Forms
{
    
    public class RegistrationForm
    {
        private const int MaxAttempts = 3;

        private IConfiguration configuration;
        private static string connectionString;


        public RegistrationForm()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Проверка на существование "appsettings.json"
        private void InitializeConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configuration = configBuilder.Build();

            if (!File.Exists("appsettings.json"))
            {
                CreateDefaultAppSettings();
            }
        }

        private void CreateDefaultAppSettings()
        {
            string defaultContent = @"
            {
              ""ConnectionStrings"": {
                ""DefaultConnection"": ""Data Source=.\\sqlexpress;Initial Catalog=Hyllel_Migrations;Integrated Security=True""
              }
            }";

            File.WriteAllText("appsettings.json", defaultContent);
            Console.WriteLine("appsettings.json file created with default content.");
        }


        public void NewRegistrationForm()
        {
            int attempts = MaxAttempts;

            var serviceProvider = CreateServices();

            // Применение миграций
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            InitializeConfiguration();

            //Код программы
            string userName = ValidationHelper.GetValidInput("First Name", ValidationHelper.IsValidUserName);

            string userEmail = ValidationHelper.GetValidInput("Email", ValidationHelper.IsValidUserEmail);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Email is valid.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please enter your Password | " +
                              "It must contain: number, capital letter, symbol and be at least 8 characters:");
            string userValueInput = Console.ReadLine();
            string userPassword = ValidationHelper.IsValidUserPassword(userValueInput);
            Console.WriteLine("Enter your date of birth");
            string userDateofBirth = Console.ReadLine();


            if (userDateofBirth != "")
            {
                RegistrationLogic user = new RegistrationLogic(
                    name: userName,
                    email: userEmail,
                    password: userPassword,
                    dataofBirth: userDateofBirth
                );
                user.SaveUserData();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("All attempts have been exhausted. Registration failed.");
            }

        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
