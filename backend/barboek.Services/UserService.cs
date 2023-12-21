using barboek.Data;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Services;

public class UserService : IUserService
{
    private DataStore _dbContext;
    public UserService(DataStore dataStore)
    {
        _dbContext = dataStore;
    }

    public User Add(string name)
    {
        DbUser dbUser = new DbUser
        {
            Id = Guid.NewGuid(),
            Name = name,
            Balance = 0,
        };

        _dbContext.Users.Add(dbUser);
        _dbContext.SaveChanges();
        
        return MapDbUserToUser(dbUser);
    }

    public User AddBalance(Guid id, float balanceToAdd)
    {
        DbUser dbUser = _dbContext.Users.FirstOrDefault(x => x.Id == id, new DbUser());
        if (dbUser.Id == Guid.Empty) return new User();

        _dbContext.Users.Update(dbUser);
        dbUser.Balance += balanceToAdd;
        _dbContext.SaveChanges();

        return MapDbUserToUser(dbUser);
    }

    public List<User> GetAll()
    {
        List<DbUser> dbUsers = _dbContext.Users.OrderBy(user => user.Name).Take(10).ToList();

        return MapDbUsersToUsers(dbUsers);
    }

    public User GetById(Guid id)
    {
        DbUser dbUser = _dbContext.Users.FirstOrDefault(u => u.Id == id, new DbUser());

        return MapDbUserToUser(dbUser);
    }

    public List<User> GetWithFilter(string filter)
    {
        List<DbUser> dbUsers = _dbContext.Users.Where(user => user.Name.Contains(filter)).OrderBy(user => user.Name).Take(10).ToList();
        
        return MapDbUsersToUsers(dbUsers);
    }

    public User Update(Guid id, string name)
    {
        DbUser dbUser = _dbContext.Users.FirstOrDefault(x => x.Id == id, new DbUser());
        if (dbUser.Id == Guid.Empty) return new User();

        _dbContext.Users.Update(dbUser);
        dbUser.Name = name;
        _dbContext.SaveChanges();

        return MapDbUserToUser(dbUser);
    }

    private List<User> MapDbUsersToUsers(List<DbUser> dbUsers)
    {
        return dbUsers.Select(dbUser => new User() {
            Id = dbUser.Id,
            Name = dbUser.Name
        }).ToList();
    }

    private List<DbUser> MapUsersToDbUsers(List<User> users)
    {
        return users.Select(user => new DbUser()
        {
            Id = user.Id,
            Name = user.Name,
        }).ToList();
    }

    private User MapDbUserToUser(DbUser dbUser)
    {
        return new User()
        {
            Id = dbUser.Id,
            Name = dbUser.Name,
        };
    }

    private DbUser MapUserToDbUser(User user)
    {
        return new DbUser()
        {
            Id = user.Id,
            Name = user.Name,
        };
    }
}