using barboek.Interface.Models.API;

namespace barboek.Interface.IServices;

public interface IUserService
{
    public List<User> GetAll();
    public List<User> GetWithFilter(string filter);
    public User GetById(Guid id);
    public User Add(string name);
    public User Update(Guid id, string name);
    public User AddBalance(Guid id, float balanceToAdd);

    // dont allow for duplicate names
}