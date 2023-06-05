using Microsoft.EntityFrameworkCore;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

// Context for working with database
// It is used to configure connection to database and configure tables
public class CurrencyDbContext : DbContext
{
    // Table of currencies
    public DbSet<CurrencyEntity> Currencies { get; set; }

    // Configure connection to database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=127.0.0.1;port=8111;user=root;password=123;database=Currencies");
    }

    // Configure table of currencies
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CurrencyEntity>().ToTable("currency");
    }
}