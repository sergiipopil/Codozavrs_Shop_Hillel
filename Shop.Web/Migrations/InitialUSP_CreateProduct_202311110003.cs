using FluentMigrator;

namespace Shop.Web.Migrations
{
    [Migration(202311090003)]
    public class InitialUSP_CreateProduct_202311110003 : Migration
    {
        public override void Up()
        {
            var sql =
                @"CREATE OR ALTER PROCEDURE CreateProduct
	@Title nvarchar(50),
	@Price money,
	@Count int,
	@Weight int = null,
	@Production nvarchar(50) = null,
	@Expiration datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Product
	VALUES (@Title, @Price, @Count, @Weight, @Production, @Expiration);
END";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP PROCEDURE CreateProduct");
        }
    }
}
