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
            _dbContext.Accounts.Add(new Account { Id = Guid.Empty, Name = "admin" });
            _dbContext.SaveChanges();
        }
        return _dbContext.Accounts.Where(a => a.Name.Contains(filter.ToLower())).Take(10).ToList();
    }
}