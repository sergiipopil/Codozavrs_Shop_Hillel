using FluentMigrator;

namespace Shop.Web.Migrations
{
    [Migration(202311090006)]
    public class InitialUSP_GetProductId_202311110006:Migration
    {
        public override void Up()
        {
            var sql =
                @"CREATE OR ALTER PROCEDURE GetProductById
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id, Title, Price, Count, Weight, Production, Expiration from Product where Id = @Id
END";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP PROCEDURE GetProductById");
        }
    }
}
