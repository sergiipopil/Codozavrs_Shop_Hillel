using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace Shop.Migrations
{
    [Migration(202311090001)]
    public class InitialTable_202311090001:Migration
    {
        public override void Up()
        {
            var sql = @"DROP TABLE IF EXISTS Product
CREATE TABLE [Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Price] [decimal](19, 5) NOT NULL,
	[Count] [int] NOT NULL,
	[Weight] [int] NULL,
	[Production] [nvarchar](50) NULL,
	[Expiration] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE IF EXISTS Product");
        }
    }
}
