using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoAnalyzerCore.Model
{
    public class Currency
    {
        [Key]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public CurrencyType Type { get; set; } // Navigation property for CurrencyType

        public Currency()
        {
        }

        public Currency(string name, DateTime date, double open, double high, double low, double close, int typeId)
        {
            Name = name;
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            TypeId = typeId;
        }
    }
}