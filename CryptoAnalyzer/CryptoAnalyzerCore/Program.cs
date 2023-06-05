using CryptoAnalyzerCore.DataBase;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore;

public class Program
{
    public static void Main(string[] args)
    {
        // Test XMLimport
        // XMLimport str = new XMLimport();
        // str.exportFromCSV("D:/C#-projs/crypto-currencies-analyzer/archive");
        // Console.WriteLine("Test");
        
        var databaseManager = new DatabaseService();

        // Test database
        // Get a currency
        Currency currency = databaseManager.GetCurrency("Ethereum");
        Console.WriteLine($"Currency: {currency.Name}, Date: {currency.Date}, Close: {currency.Close}");

        // Add a new currency
        var newCurrency = new Currency
        (
            "Bitcoin",
            DateTime.Now,
           2500,
            2600,
            2400,
            2550,
            true
        );

        databaseManager.AddCurrency(newCurrency);
    }
}