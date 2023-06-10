using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CryptoAnalyzerCore.Model;

public class Exchange
{
    [Key] public int Id { get; set; }
    public string ServiceName { get; set; }

    public Exchange()
    {
    }

    public Exchange(int id, string serviceName)
    {
        Id = id;
        ServiceName = serviceName;
    }
}