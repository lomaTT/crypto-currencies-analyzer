using System;
using System.Linq;
using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

public class CurrencyTypeService
{
    // Get currency type by name
    public CurrencyType GetCurrencyType(string name)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.CurrencyTypes.FirstOrDefault(c => c.TypeName == name);
        }
    }
    
    public CurrencyType GetCurrencyType(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.CurrencyTypes.FirstOrDefault(c => c.Id == id);
        }
    }

    // Add currency type to the database
    public void AddCurrencyType(CurrencyType currencyType)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            dbContext.CurrencyTypes.Add(currencyType);
            dbContext.SaveChanges();
        }
    }
}
