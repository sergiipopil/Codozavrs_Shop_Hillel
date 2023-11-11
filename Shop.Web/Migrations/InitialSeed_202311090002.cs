using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Shop.Migrations
{
    [Migration(202311090002)]
    public class InitialSeed_202311090002 : Migration
    {
        public override void Up()
        {
            var sql = @"INSERT INTO [dbo].[Product]
           ([Title]
           ,[Price]
           ,[Count]
           ,[Weight]
           ,[Production]
           ,[Expiration])
     VALUES
           ('Milk', 111, 10, 1500, 'Molokia', '2024-01-20'),
		   ('Coffee', 222, 20, 1000, 'Lavazza', '2024-04-22'),
		   ('Tea', 333, 30, 300, 'Grinfield', '2024-03-22'),
		   ('Chocolate', 444, 40, 200, 'EP', '2024-05-22'),
		   ('Water', 555, 50, 2000, 'MR', '2024-03-22')
GO";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DELETE FROM Product");
        }
    }
}
