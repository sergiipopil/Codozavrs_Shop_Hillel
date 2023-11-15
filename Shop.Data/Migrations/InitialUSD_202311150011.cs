using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(202311150011)]
    internal class InitialUSD_202311150011 : Migration
    {

        public override void Down()
        {
            var sql = @"DROP PROCEDURE IF EXISTS CreateCustomer";
            Execute.Sql(sql);
        }

        public override void Up()
        {
            var sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1
                    FROM INFORMATION_SCHEMA.ROUTINES
                    WHERE ROUTINE_NAME = 'CreateCustomer' AND ROUTINE_TYPE = 'PROCEDURE'
                 )
                 BEGIN
                   EXEC('
                    CREATE PROCEDURE hillel.CreateCustomer
                        @FirstName varchar (15),
                        @LastName varchar (15),
                        @PhoneNumber nvarchar (13),
                        @BirthDay datetime,
                        @Age int,
                        @Cash decimal (18,2)
                    AS
                    BEGIN
                        INSERT INTO hillel.Customer (FirstName, LastName, PhoneNumber, BirthDay, Age, Cash)
                        VALUES(@FirstName, @LastName, @PhoneNumber, @BirthDay, @Age, @Cash)

                    END');
                 END";
            Execute.Sql(sql);
        }
    }
}
