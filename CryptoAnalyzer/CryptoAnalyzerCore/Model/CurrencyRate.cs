using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoAnalyzerCore.Model;

public class CurrencyRate
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int BaseCurrencyId { get; set; }
    public int QuoteCurrencyId { get; set; }
    public double Rate { get; set; }

    [ForeignKey("BaseCurrencyId")]
    public Currency BaseCurrencyObject { get; set; } // Navigation property for BaseCurrency

    [ForeignKey("QuoteCurrencyId")]
    public Currency QuoteCurrencyObject { get; set; } // Navigation property for QuoteCurrency
}