using System;
using System.Linq;
using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;
    
public class CurrencyService
{
    // Get currency from the database by name
    public Currency GetCurrency(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.Currencies.FirstOrDefault(c => c.Name == name);
        }
    }

    // Add currency to the database
    public void AddCurrency(Currency currency)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            dbContext.Currencies.Add(currency);
            dbContext.SaveChanges();
        }
    }

    // Remove currency from the database
    public void RemoveCurrency(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            var currency = dbContext.Currencies.FirstOrDefault(c => c.Name == name);

            if (currency != null)
            {
                dbContext.Currencies.Remove(currency);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Currency with name '{name}' not found.");
            }
        }
    }
}
