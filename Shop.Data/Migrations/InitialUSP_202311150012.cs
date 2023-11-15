using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(202311150012)]
    internal class InitialUSP_202311150012 : Migration
    {
        public override void Down()
        {
            var sql = @"DROP PROCEDURE IF EXISTS DeleteCustomerById";
            Execute.Sql(sql);
        }

        public override void Up()
        {
            var sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1
                    FROM INFORMATION_SCHEMA.ROUTINES
                    WHERE ROUTINE_NAME = 'DeleteCustomerById' AND ROUTINE_TYPE = 'PROCEDURE'
                 )
                 BEGIN
                   EXEC('
                    CREATE PROCEDURE hillel.DeleteCustomerById
                        @Id int
                    AS
                    BEGIN

                        DELETE
                        FROM hillel.Customer
                        WHERE Id=@Id

                    END');
                 END";
            Execute.Sql(sql);
        }
    }
}
