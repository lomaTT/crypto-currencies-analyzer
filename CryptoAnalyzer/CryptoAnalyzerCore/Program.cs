using CryptoAnalyzerCore.DataBase;
using CryptoAnalyzerCore.Model;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.Yaml;

namespace CryptoAnalyzerCore;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddYamlFile("config.yaml")
            .Build();
        
        // Test XMLimport
        // XMLimport str = new XMLimport();
        // str.exportFromCSV("D:/C#-projs/crypto-currencies-analyzer/archive");
        // Console.WriteLine("Test");
        
        var databaseService = new DatabaseService();

        // Test database
        // Get a currency
        Currency currency = databaseService.GetCurrency("Ethereum");
        Console.WriteLine($"Currency: {currency.Name}, Date: {currency.Date}, Close: {currency.Close}");
        
        databaseService.RemoveCurrency("Ethereum");
        
        // Add a new currency
        var newCurrency = new Currency
        (
            "Ethereum",
            DateTime.Now,
           2500,
            2600,
            2400,
            2550,
            true
        );

        databaseService.AddCurrency(newCurrency);
    }
}