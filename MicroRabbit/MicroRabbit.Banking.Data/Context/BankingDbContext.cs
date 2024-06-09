using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext() { }
        public BankingDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=BankingDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
 
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
