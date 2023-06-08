using System;
using System.Linq;
using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

public class ExchangeService
{
    // Get exchange rate by id
    public Exchange GetExchangeRate(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.Exchanges.FirstOrDefault(e => e.Id == id);
        }
    }

    // Add exchange rate to the database
    public void AddExchangeRate(Exchange exchange)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            dbContext.Exchanges.Add(exchange);
            dbContext.SaveChanges();
        }
    }

    // Remove exchange rate by id
    public void RemoveExchangeRate(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            var exchange = dbContext.Exchanges.FirstOrDefault(e => e.Id == id);

            if (exchange != null)
            {
                dbContext.Exchanges.Remove(exchange);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Exchange rate with id '{id}' not found.");
            }
        }
    }
}