using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(202311150010)]
    internal class InitialUSP_202311150010 : Migration
    {
        public override void Down()
        {
            var sql = @"DROP PROCEDURE IF EXISTS GetCustomerById";
            Execute.Sql(sql);
        }

        public override void Up()
        {
            var sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1
                    FROM INFORMATION_SCHEMA.ROUTINES
                    WHERE ROUTINE_NAME = 'GetCustomerById' AND ROUTINE_TYPE = 'PROCEDURE'
                 )
                 BEGIN
                   EXEC('
                    CREATE PROCEDURE hillel.GetCustomerById
                        @Id int
                    AS
                    BEGIN
                        SELECT *
                        FROM Customer
                        WHERE Id = @Id
                    END');
                 END";
            Execute.Sql(sql);
        }
    }
}
