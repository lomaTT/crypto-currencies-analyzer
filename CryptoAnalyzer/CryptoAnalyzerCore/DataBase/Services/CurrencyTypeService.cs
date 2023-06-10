using CryptoAnalyzerCore.Model;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class CurrencyTypeService
{
    public CurrencyType GetCurrencyType(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.CurrencyTypes.FirstOrDefault(c => c.TypeName == name);
            }
        }
    }

    public CurrencyType GetCurrencyType(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.ReadUncommitted}))
            {
                return dbContext.CurrencyTypes.FirstOrDefault(c => c.Id == id);
            }
        }
    }

    public string GetTypeName(int id)
        => GetCurrencyType(id)?.TypeName;

    public void AddCurrencyType(CurrencyType currencyType)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                dbContext.CurrencyTypes.Add(currencyType);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }

    public void RemoveCurrencyType(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                       new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
            {
                var currencyType = dbContext.CurrencyTypes.FirstOrDefault(c => c.Id == id);

                if (currencyType != null)
                {
                    dbContext.CurrencyTypes.Remove(currencyType);
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    throw new Exception($"Currency type with ID '{id}' not found.");
                }
            }
        }
    }
    
    public void ClearCurrencyTypes()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                dbContext.CurrencyTypes.RemoveRange(dbContext.CurrencyTypes);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }
}