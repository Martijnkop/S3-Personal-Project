using barboek.Data;
using barboek.Interface.Models;
using Microsoft.IdentityModel.Tokens;

namespace barboek.Services;

public class ItemService
{
    private DataStore _dbContext;

    public ItemService(DataStore dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Item> GetItems()
    {
        //temp

        if (_dbContext.Items.IsNullOrEmpty())
        {
            _dbContext.Items.Add(new Item { Id = Guid.NewGuid(), Name = "test"});
            _dbContext.Items.Add(new Item { Id = Guid.NewGuid(), Name = "test2"});
            _dbContext.SaveChanges();
        }

        return _dbContext.Items.ToList();
    }
}