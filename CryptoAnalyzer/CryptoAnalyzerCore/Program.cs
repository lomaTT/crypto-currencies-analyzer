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
        // var configuration = new ConfigurationBuilder()
        //     .SetBasePath(Directory.GetCurrentDirectory())
        //     .AddYamlFile("config.yaml")
        //     .Build();
        
        // Test XMLimport
        // XMLimport str = new XMLimport();
        // str.exportFromCSV("D:/C#-projs/crypto-currencies-analyzer/archive");
        // Console.WriteLine("Test");

        var manager = new DatabaseManager();

        manager.ClearAllData();
        manager.FillDatabase();
    }
}