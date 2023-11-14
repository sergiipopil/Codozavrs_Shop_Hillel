using FluentMigrator;


[Migration(20231112120001)]
public class InitialMigration : Migration
{
    public override void Up()
    {
        // Ваши операции создания таблиц и другие миграционные шаги
        Execute.Sql("CREATE TABLE SignupTableMigration (Id INT NOT NULL PRIMARY KEY IDENTITY, Password NVARCHAR(100) NOT NULL, Name NVARCHAR(100) NOT NULL, Email NVARCHAR(100) NOT NULL UNIQUE, DateofBirth NVARCHAR(100) NOT NULL);");

    }

    public override void Down()
    {
        // Ваши операции удаления таблиц и другие миграционные шаги для отката
        Execute.Sql("DROP TABLE SignupTableMigration;");
    }
}