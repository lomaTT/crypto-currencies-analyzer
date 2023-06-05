using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase
{
    public class DatabaseService
    {
        // Get currency from the database by name
        public Currency GetCurrency(string name)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                using (var dbContext = new CurrencyDbContext())
                {
                    var currencyEntity = dbContext.Currencies.Find(name);

                    if (currencyEntity != null)
                    {
                        var currency = new Currency(
                            currencyEntity.Name,
                            currencyEntity.Date,
                            currencyEntity.Open,
                            currencyEntity.High,
                            currencyEntity.Low,
                            currencyEntity.Close,
                            currencyEntity.IsCrypto
                        );

                        transactionScope.Complete();
                        return currency;
                    }
                }

                throw new Exception($"Currency with name '{name}' not found.");
            }
        }

        // Add currency to the database
        public void AddCurrency(Currency currency)
        {
            // TransactionScope is used to ensure that the operation is safe
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                using (var dbContext = new CurrencyDbContext())
                {
                    try
                    {
                        dbContext.Currencies.Add(new CurrencyEntity
                        (
                            currency.Name,
                            currency.Date,
                            currency.Open,
                            currency.High,
                            currency.Low,
                            currency.Close,
                            currency.IsCrypto
                        ));

                        dbContext.SaveChanges();

                        // If no exception occurs, the transaction will be committed
                        transactionScope.Complete();
                    }
                    catch
                    {
                        // If an exception occurs, the transaction will be rolled back
                        transactionScope.Dispose();
                        throw;
                    }
                }
            }
        }
        
        // Remove currency from the database
        public void RemoveCurrency(string name)
        {
            // TransactionScope is used to ensure that the operation is safe
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                using (var dbContext = new CurrencyDbContext())
                {
                    var currencyEntity = dbContext.Currencies.Find(name);

                    if (currencyEntity != null)
                    {
                        dbContext.Currencies.Remove(currencyEntity);
                        dbContext.SaveChanges();

                        // If no exception occurs, the transaction will be committed
                        transactionScope.Complete();
                    }
                    else
                    {
                        throw new Exception($"Currency with name '{name}' not found.");
                    }
                }
            }
        }
    }
}