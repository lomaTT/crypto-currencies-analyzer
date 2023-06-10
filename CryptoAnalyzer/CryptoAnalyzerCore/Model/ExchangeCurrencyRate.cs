using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoAnalyzerCore.Model;

public class ExchangeCurrencyRate
{
    [Key] 
    public int Id { get; set; }
    public int CurrencyRateId { get; set; }
    public int ExchangeId { get; set; }
    
    [ForeignKey("CurrencyRateId")] 
    public CurrencyRate CurrencyRate { get; set; } // Navigation property for CurrencyType
    
    [ForeignKey("ExchangeId")] 
    public Exchange Exchange { get; set; } // Navigation property for CurrencyType
}