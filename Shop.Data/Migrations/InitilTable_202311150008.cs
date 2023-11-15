using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(2023111500008)]
    public class InitialTables_2023111500008 : Migration
    {
        public override void Down()
        {
            var sql = "DROP TABLE Customer";
            Execute.Sql(sql);
        }

        public override void Up()
        {
            var sql = @"IF NOT EXISTS 
                       ( 
                        SELECT 1 FROM INFORMATION_SCHEMA.TABLES
                         WHERE TABLE_SCHEMA = 'dbo'
                            AND TABLE_NAME = 'Customer'
                       )
                        BEGIN
                            CREATE TABLE [dbo].Customer
                            (
                                Id int IDENTITY (1,1) PRIMARY KEY not null,
	                            FirstName varchar (15) null,
	                            LastName varchar (15) null,
	                            NumberPhone nvarchar(13) null,
	                            BirhtDay datetime null,
	                            Age int null,
	                            Cash decimal(18,2) null
                            )
                        END;
";
            Execute.Sql(sql);
        }
    }
}
