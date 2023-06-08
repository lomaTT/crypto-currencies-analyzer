using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoAnalyzerCore.Model;

public class Exchange
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string BaseCurrency { get; set; }
    public string QuoteCurrency { get; set; }
    public double Rate { get; set; }

    [ForeignKey("BaseCurrency")]
    public Currency BaseCurrencyObject { get; set; } // Navigation property for BaseCurrency

    [ForeignKey("QuoteCurrency")]
    public Currency QuoteCurrencyObject { get; set; } // Navigation property for QuoteCurrency
}