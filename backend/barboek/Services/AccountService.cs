using barboek.Data;
using barboek.Interface;
using barboek.Interface.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace barboek.Services;

public class AccountService
{
    private DataStore _dbContext;

    public AccountService(DataStore dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Account> GetFromFilter(string filter)
    {
        //temp
        if (_dbContext.Accounts.IsNullOrEmpty())
        {
            AddUser();
        }
        return _dbContext.Accounts.Where(a => a.Name.ToLower().Contains(filter.ToLower())).Take(10).ToList();
    }

    public void AddUser(string name = "admin")
    {
        if (string.IsNullOrEmpty(name) || _dbContext.Accounts.Any(account => account.Name == name)) return;
        _dbContext.Accounts.Add(new Account { Id = Guid.NewGuid(), Name = name });
        _dbContext.SaveChanges();
    }
}