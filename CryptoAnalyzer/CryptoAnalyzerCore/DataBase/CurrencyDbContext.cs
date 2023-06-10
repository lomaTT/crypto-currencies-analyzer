using Microsoft.EntityFrameworkCore;
using CryptoAnalyzerCore.Model;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CryptoAnalyzerCore.DataBase;

// Context for working with database
// It is used to configure connection to database and configure tables
public class CurrencyDbContext : DbContext
{
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyType> CurrencyTypes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CurrencyRate> CurrenciesRate { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<ExchangeCurrencyRate> ExchangesCurrenciesRate { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=127.0.0.1;port=8111;user=root;password=123;database=Currencies");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().ToTable("currency");
        modelBuilder.Entity<CurrencyType>().ToTable("currency_type");
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<CurrencyRate>().ToTable("currency_rate");
        modelBuilder.Entity<Exchange>().ToTable("exchange");
        modelBuilder.Entity<ExchangeCurrencyRate>().ToTable("exchange_currency_rate");
    }
}