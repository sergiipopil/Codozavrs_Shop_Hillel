using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(202311150013)]
    internal class InitialUSD_202311150013 : Migration
    {
        public override void Down()
        {
            var sql = @"DROP PROCEDURE IF EXISTS UpDateCustomerById";
            Execute.Sql(sql);
        }

        public override void Up()
        {
            var sql = @"
                IF NOT EXISTS 
                (
                    SELECT 1
                    FROM INFORMATION_SCHEMA.ROUTINES
                    WHERE ROUTINE_NAME = 'UpDateCustomerById' AND ROUTINE_TYPE = 'PROCEDURE'
                 )
                 BEGIN
                   EXEC('
                    CREATE PROCEDURE hillel.UpDateCustomerById
                        @Id int,
                        @FirstName varchar (15)= NULL,
                        @LastName varchar (15)= NULL,
                        @PhoneNumber nvarchar (13)= NULL,
                        @BirthDay datetime= NULL,
                        @Age int= NULL,
                        @Cash decimal (18,2)= NULL
                    AS
                    BEGIN

                        UPDATE Customer
                        SET FirstName = ISNULL(@FirstName, FirstName),
                                LastName = ISNULL(@LastName, LastName),
                                PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber),
                                BirthDay = ISNULL(@BirthDay, BirthDay),
                                Age = ISNULL(@Age, Age),
                                Cash = ISNULL(@Cash, Cash)
                        WHERE Id=@Id

                    END');
                 END";
            Execute.Sql(sql);
        }
    }
}
