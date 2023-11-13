using FluentMigrator;

namespace Shop.Web.Migrations
{
    [Migration(202311090004)]
    public class InitialUSP_DeleteProductBy_202311110004:Migration
    {
         public override void Up()
        {
            var sql =
                @"CREATE OR ALTER PROCEDURE DeleteProductById
	                @Id int
                    AS
                BEGIN
	            SET NOCOUNT ON;
	            DELETE FROM Product WHERE Id=@Id
                END";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP PROCEDURE DeleteProductById");
        }
    }
}
