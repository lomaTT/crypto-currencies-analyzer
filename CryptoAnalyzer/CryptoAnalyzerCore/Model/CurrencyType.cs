using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CryptoAnalyzerCore.Model
{
    public class CurrencyType
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }

        public CurrencyType()
        {
        }

        public CurrencyType(int id, string typeName)
        {
            Id = id;
            TypeName = typeName;
        }
    }
}