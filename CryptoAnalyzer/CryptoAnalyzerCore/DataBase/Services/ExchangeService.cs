using CryptoAnalyzerCore.Model;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class ExchangeService
{
    public Exchange GetExchange(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbContext.Exchanges.FirstOrDefault(c => c.ServiceName == name);
            }
        }
    }

    public Exchange GetExchange(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbContext.Exchanges.FirstOrDefault(c => c.Id == id);
            }
        }
    }

    public void AddExchange(Exchange exchange)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                dbContext.Exchanges.Add(exchange);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }

    public void RemoveExchange(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var exchange = dbContext.Exchanges.FirstOrDefault(c => c.Id == id);

                if (exchange != null)
                {
                    dbContext.Exchanges.Remove(exchange);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    throw new Exception($"Exchange with ID '{id}' not found.");
                }
            }
        }
    }

    public List<Exchange> GetAllExchanges()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbContext.Exchanges.ToList();
            }
        }
    }
    
    public void ClearExchanges()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var exchanges = dbContext.Exchanges.ToList();
                dbContext.Exchanges.RemoveRange(exchanges);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }
}