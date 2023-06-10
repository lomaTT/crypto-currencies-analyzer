using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoAnalyzerCore.Model;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Password { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}