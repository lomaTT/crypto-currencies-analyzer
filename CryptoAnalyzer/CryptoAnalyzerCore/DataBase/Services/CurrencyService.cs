using CryptoAnalyzerCore.Model;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class CurrencyService
{
    public Currency GetCurrency(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.Currencies.FirstOrDefault(c => c.Id == id);
            }
        }
    }
    
    public Currency GetCurrency(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.Currencies.FirstOrDefault(c => c.Name == name);
            }
        }
    }

    public IEnumerable<Currency> GetAllCurrencies()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.Currencies.ToList();
            }
        }
    }

    public IEnumerable<Currency> GetCurrenciesByType(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.Currencies.Where(c => c.TypeId == id).ToList();
            }
        }
    }

    public void AddCurrency(Currency currency)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                dbContext.Currencies.Add(currency);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }

    public void RemoveCurrency(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                var currency = dbContext.Currencies.FirstOrDefault(c => c.Id == id);

                if (currency != null)
                {
                    dbContext.Currencies.Remove(currency);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    throw new Exception($"Currency with id '{id}' not found.");
                }
            }
        }
    }
    
    public void ClearCurrencies()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                dbContext.Currencies.RemoveRange(dbContext.Currencies);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }
}