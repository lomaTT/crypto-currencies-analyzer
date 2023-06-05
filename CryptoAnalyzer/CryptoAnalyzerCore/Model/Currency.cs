using Microsoft.EntityFrameworkCore;

namespace CryptoAnalyzerCore.Model;

// This class is used to store data from XML file
// Fields are readonly because they are not changed after initialization by the safety reasons
public struct Currency
{
    public readonly string Name;
    public readonly DateTime Date;
    public readonly double Open;
    public readonly double High;
    public readonly double Low;
    public readonly double Close;
    public readonly bool IsCrypto;
    
    public Currency(string name, DateTime date, double open, double high, double low, double close, bool isCrypto)
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