using FluentMigrator;

namespace Shop.Web.Migrations
{
    [Migration(202311090005)]
    public class InitialUSP_GetAllProducts_202311110005:Migration
    {
        public override void Up()
        {
            var sql =
                @"CREATE OR ALTER PROCEDURE GetAllProducts
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Title, Price, Count, Weight, Production, Expiration from Product
END";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP PROCEDURE GetAllProducts");
        }
    }
}
