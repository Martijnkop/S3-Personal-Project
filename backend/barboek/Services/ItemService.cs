using barboek.Data;
using barboek.Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            AddItem("test");
        }

        return _dbContext.Items.ToList();
    }

    public void AddItem(string name = "test")
    {
        if (string.IsNullOrEmpty(name) || _dbContext.Items.Any(item => item.Name == name)) return;
        _dbContext.Items.Add(new Item { Id = Guid.NewGuid(), Name = name });
        _dbContext.SaveChanges();

    }
}