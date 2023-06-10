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
    
    public bool IsUsernameTaken(string login, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isolationLevel }))
            {
                return dbContext.Users.Any(u => u.Login == login);
            }
        }
    }

    public bool IsEmailTaken(string email, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isolationLevel }))
            {
                return dbContext.Users.Any(u => u.Email == email);
            }
        }
    }
    
    public User AuthenticateUser(string login, string password)
    {
        using (var dbContext = new CurrencyDbContext())
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                // Find the user with the provided login
                var user = dbContext.Users.FirstOrDefault(u => u.Login == login);

                if (user != null)
                {
                    // Hash the provided password
                    string hashedPassword = HashPassword(password);

                    // Compare the hashed password with the stored password
                    if (user.Password == hashedPassword)
                        return user;
                }

                return null; // Authentication failed
            }
        }
    }
        
    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            string hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return hashedPassword;
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