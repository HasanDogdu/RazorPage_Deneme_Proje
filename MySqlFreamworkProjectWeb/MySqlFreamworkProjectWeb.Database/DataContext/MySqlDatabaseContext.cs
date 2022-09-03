using Microsoft.EntityFrameworkCore;
using MySqlFreamworkProjectWeb.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlFreamworkProjectWeb.Database.DataContext
{
    public class MySqlDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseMySQL(@"server=arbilyazilim.com; user=15_testdb; password=Arb..21; database=15_testdb;",
               builder => builder.CommandTimeout(100));
            
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
