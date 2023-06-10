using System.Transactions;
using CryptoAnalyzerCore.DataBase.Services;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

public class DatabaseManager
{
    public CurrencyService CurrencyService { get; }
    public CurrencyTypeService CurrencyTypeService { get; }
    public CurrencyRateService CurrencyRateService { get; }
    public UserService UserService { get; }
    public ExchangeService ExchangeService { get; }
    public ExchangeCurrencyRateService ExchangeCurrencyRateService { get; }

    public DatabaseManager()
    {
        CurrencyService = new CurrencyService();
        CurrencyTypeService = new CurrencyTypeService();
        CurrencyRateService = new CurrencyRateService();
        UserService = new UserService();
        ExchangeService = new ExchangeService();
        ExchangeCurrencyRateService = new ExchangeCurrencyRateService();
    }

    public void FillDatabase()
    {
        using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   new TransactionOptions {IsolationLevel = IsolationLevel.Serializable}))
        {
            // Add example data for CurrencyTypeService
            var currencyType1 = new CurrencyType {Id = 1, TypeName = "Fiat"};
            var currencyType2 = new CurrencyType {Id = 2, TypeName = "Crypto"};
            CurrencyTypeService.AddCurrencyType(currencyType1);
            CurrencyTypeService.AddCurrencyType(currencyType2);

            // Add example data for CurrencyService
            var currency1 = new Currency("USD", DateTime.Now, 1.0, 1.2, 0.8, 1.1, 1);
            var currency2 = new Currency("EUR", DateTime.Now, 1.2, 1.5, 1.0, 1.3, 1);
            CurrencyService.AddCurrency(currency1);
            CurrencyService.AddCurrency(currency2);

            // Add example data for CurrencyRateService
            var currencyRate1 = new CurrencyRate {Id = 1};
            var currencyRate2 = new CurrencyRate {Id = 2};
            CurrencyRateService.AddCurrencyRate(currencyRate1);
            CurrencyRateService.AddCurrencyRate(currencyRate2);

            // Add example data for UserService
            var user1 = new User
            {
                Id = 1, Password = "password1", Login = "user1", Email = "user1@example.com",
                RegistrationDate = DateTime.Now
            };
            var user2 = new User
            {
                Id = 2, Password = "password2", Login = "user2", Email = "user2@example.com",
                RegistrationDate = DateTime.Now
            };
            UserService.AddUser(user1);
            UserService.AddUser(user2);

            // Add example data for ExchangeService
            var exchange1 = new Exchange {Id = 1, ServiceName = "Exchange1"};
            var exchange2 = new Exchange {Id = 2, ServiceName = "Exchange2"};
            ExchangeService.AddExchange(exchange1);
            ExchangeService.AddExchange(exchange2);

            // Add example data for ExchangeCurrencyRateService
            var exchangeCurrencyRate1 = new ExchangeCurrencyRate {ExchangeId = 1, CurrencyRateId = 1};
            var exchangeCurrencyRate2 = new ExchangeCurrencyRate {ExchangeId = 2, CurrencyRateId = 2};
            ExchangeCurrencyRateService.AddExchangeCurrencyRate(exchangeCurrencyRate1);
            ExchangeCurrencyRateService.AddExchangeCurrencyRate(exchangeCurrencyRate2);

            scope.Complete();
        }
    }

    public void ClearAllData()
    {
        CurrencyService.ClearCurrencies();
        CurrencyTypeService.ClearCurrencyTypes();
        CurrencyRateService.ClearCurrencyRates();
        UserService.ClearUsers();
        ExchangeService.ClearExchanges();
        ExchangeCurrencyRateService.ClearExchangeCurrencyRates();
    }
}