using System;
using System.Linq;
using System.Transactions;
using CryptoAnalyzerCore.Model;

namespace CryptoAnalyzerCore.DataBase;

public class UserService
{
    // Get user by id
    public User GetUser(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            return dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }

    // Add user to the database
    public void AddUser(User user)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
    }

    // Remove user by id
    public void RemoveUser(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"User with id '{id}' not found.");
            }
        }
    }
}