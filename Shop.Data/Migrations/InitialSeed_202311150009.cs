using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Migrations
{
    [Migration(202311150009)]
    public class InitialSeed_202311150009 : Migration
    {
        public override void Up()
        {
            var sql = @"INSERT INTO [hillel].[Customer]
           (   [FirstName]
               ,[LastName]
               ,[PhoneNumber]
               ,[BirthDay]
               ,[Age]
               ,[Cash])
            VALUES
                ('Lubov', 'Coco', '+380998886633', '1990/12/12', 33, 1700),
		        ('Alex', 'Grey', '+380668885511', '1990/12/12', 13, 50),
		        ('Eygen', 'Drinkwater', '+380773336644', '2005/10/10', 18, 500),
		        ('Osman', 'Goroz', '+380229994411', '2001/10/10', 22, 1000),
		        ('Pierre-Emerick', 'Aubameyang', '+380963286475', '1989/06/18', 34, 1000000),
                ('Johann', 'Gudmundsson', '+380736548536', '1990/10/27', 33, 300000),
                ('Ryan', 'Gravenberch', '+380664781535', '2002/05/16', 21, 150000),
                ('Lazaros', 'Christodoulopou', '+380897418563', '1986/12/19', 37, 9000),
                ('Caleb', 'Chukwuemeka', '+380659632457', '2001/08/25', 21, 21700),
                ('Malcom', 'Disasi', '+380129632873', '1999/12/12', 24, 1),
                ('Alex', 'Disasi', '+380129632873', '1998/07/18', 25, 30700),
                ('Alex', 'DisasiOne', '+380129632873', '1998/07/18', 25, 30700),
                ('Borja', 'Mayoral', '+38099888662', '1997/04/05', 26, 875632)
            GO";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            Execute.Sql("DELETE FROM Customer");
        }
    }
}
