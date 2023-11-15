using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data;

namespace Shop.Web
{
    public static class StartConfig
    {
        private static IConfigurationRoot config;
        public static DatabaseContext _dbContext;
        private static void InitConfig()
        {
            var builder = new ConfigurationBuilder();
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            config = builder.Build();
        }
        public static void InitialConfig()
        {
            InitConfig();
            _dbContext = new DatabaseContext(config);
            var database = new Database(_dbContext);
            database.CreateDatabase("CodoZavrs_Shop");

            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                RunMigration(scope.ServiceProvider);
            }
        }
        private static void RunMigration(IServiceProvider serviceProvider)
        {
            var migrationService = serviceProvider.GetRequiredService<IMigrationRunner>();
            migrationService.ListMigrations();
            migrationService.MigrateUp();
        }
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2012()
                    .WithGlobalConnectionString(config.GetConnectionString("RemoteConnection"))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .BuildServiceProvider(false);
        }
    }
}
