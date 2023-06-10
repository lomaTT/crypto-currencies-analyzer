using CryptoAnalyzerCore.Model;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class CurrencyRateService
{
    public CurrencyRate GetCurrencyRate(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.CurrenciesRate.FirstOrDefault(e => e.Id == id);
            }
        }
    }
    
    public CurrencyRate GetCurrencyRatesByCurrencyId(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.CurrenciesRate.FirstOrDefault(e => e.BaseCurrencyId == id);
            }
        }
    }

    public IEnumerable<CurrencyRate> GetAllCurrencyRates()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.CurrenciesRate.ToList();
            }
        }
    }

    public void AddCurrencyRate(CurrencyRate currencyRate, Exchange exchange = null)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                dbContext.CurrenciesRate.Add(currencyRate);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }

    public void RemoveCurrencyRate(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                var currencyRate = dbContext.CurrenciesRate.FirstOrDefault(e => e.Id == id);

                if (currencyRate != null)
                {
                    dbContext.CurrenciesRate.Remove(currencyRate);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    throw new Exception($"Currency rate with id '{id}' not found.");
                }
            }
        }
    }
    
    public void ClearCurrencyRates()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                dbContext.CurrenciesRate.RemoveRange(dbContext.CurrenciesRate);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }
}