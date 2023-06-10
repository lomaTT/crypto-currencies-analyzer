using CryptoAnalyzerCore.Model;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class ExchangeCurrencyRateService
{
    public ExchangeCurrencyRate GetExchangeCurrencyRate(int currencyId)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.ExchangesCurrenciesRate.FirstOrDefault(c => c.CurrencyRateId == currencyId);
        }
    }

    public void AddExchangeCurrencyRate(ExchangeCurrencyRate exchangeCurrencyRate)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (var newTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        dbContext.ExchangesCurrenciesRate.Add(exchangeCurrencyRate);
                        dbContext.SaveChanges();
                        newTransaction.Commit();
                    }
                    catch
                    {
                        newTransaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }


    public void RemoveExchangeCurrencyRate(ExchangeCurrencyRate exchangeCurrencyRate)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    dbContext.ExchangesCurrenciesRate.Remove(exchangeCurrencyRate);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

    public List<ExchangeCurrencyRate> GetExchangeCurrencyRatesByExchangeId(int exchangeId)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                return dbContext.ExchangesCurrenciesRate
                    .Include(e => e.CurrencyRate)
                    .Include(e => e.Exchange)
                    .Where(e => e.ExchangeId == exchangeId)
                    .ToList();
            }
        }
    }

    public void ClearExchangeCurrencyRates()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var transaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    dbContext.ExchangesCurrenciesRate.RemoveRange(dbContext.ExchangesCurrenciesRate);
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}