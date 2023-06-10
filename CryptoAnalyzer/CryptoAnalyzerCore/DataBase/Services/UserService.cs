using CryptoAnalyzerCore.Model;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace CryptoAnalyzerCore.DataBase.Services;

public class UserService
{
    public User GetUser(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbContext.Users.FirstOrDefault(u => u.Id == id);
            }
        }
    }

    public void AddUser(User user)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }

    public void RemoveUser(int id)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    dbContext.Users.Remove(HashPassword(user));
                    dbContext.SaveChanges();
                    scope.Complete();
                }
                else
                {
                    throw new Exception($"User with id '{id}' not found.");
                }
            }
        }
    }

    public List<User> GetAllUsers()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                return dbContext.Users.ToList();
            }
        }
    }
        
    private User HashPassword(User user)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            user.Password = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return user;
        }
    }
    
    public void ClearUsers()
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
            {
                var users = dbContext.Users.ToList();
                dbContext.Users.RemoveRange(users);
                dbContext.SaveChanges();
                scope.Complete();
            }
        }
    }
}