using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

public class DatabaseManager
{
    public CurrencyService CurrencyService { get; }
    public CurrencyTypeService CurrencyTypeService { get; }
    public ExchangeService ExchangeService { get; }
    public UserService UserService { get; }

    public DatabaseManager()
    {
        CurrencyService = new CurrencyService();
        CurrencyTypeService = new CurrencyTypeService();
        ExchangeService = new ExchangeService();
        UserService = new UserService();
    }
}