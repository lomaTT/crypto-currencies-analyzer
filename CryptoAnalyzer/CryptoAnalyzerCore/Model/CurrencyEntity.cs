using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CryptoAnalyzerCore.Model;

public class CurrencyEntity
{
    // Table of currencies
    // [Key] is used for primary key
    [Key] public string Name { get; set; }
    public DateTime Date { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public bool IsCrypto { get; set; }

    public CurrencyEntity()
    {
    }

    public CurrencyEntity(string name, DateTime date, double open, double high, double low, double close, bool isCrypto)
    {
        Name = name;
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        IsCrypto = isCrypto;
    }
}