using FluentMigrator;

namespace Shop.Web.Migrations
{
    [Migration(202311090007)]
    public class InitialUSP_UpdateProductById_202311110007:Migration
    {
        public override void Up()
        {
            var sql =
                @"CREATE OR ALTER PROCEDURE UpdateProductById
	@Id int,
	@Title nvarchar(50),
	@Price money,
	@Count int,
	@Weight int = null,
	@Production nvarchar(50) = null,
	@Expiration datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	UPDATE Product
	SET Price=@Price, Title=@Title, [Count]=@Count, [Weight]=@Weight, [Production]=@Production, [Expiration]=@Expiration
	WHERE Id=@Id
END";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP PROCEDURE UpdateProductById");
        }
    }
}
