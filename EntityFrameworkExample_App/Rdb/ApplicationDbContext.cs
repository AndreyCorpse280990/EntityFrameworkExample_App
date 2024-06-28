using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.Rdb
{
    internal class ApplicationDbContext : DbContext 
    {
        public DbSet<RdbStudent> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string useConnection = ConfigurationManager.AppSettings["UseConnection"] ?? "DefaultDbConnection";
            string connectionString = ConfigurationManager.ConnectionStrings[useConnection].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
