using CurrencyConverter.Core.Utilities;
using CurrencyConverter.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Repositories.General
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CurrencyLog> CurrencyLogs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.GetConnectionString("Default"));
            optionsBuilder.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
