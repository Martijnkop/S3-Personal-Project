using barboek.Data;
using barboek.Data.Migrations;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace barboek.Services;

public class ItemCategoryService : IItemCategoryService, IDbItemCategoryService
{
    private DataStore _dbContext;
    public ItemCategoryService(DataStore dataStore)
    {
        _dbContext = dataStore;
    }

    public List<ItemCategory> GetAll()
    {
        return MapDbToApi(_dbContext.ItemCategories.OrderBy(ItemCategory => ItemCategory.Order).ThenBy(ItemCategory => ItemCategory.Name).ToList());
    }

    public ItemCategory GetById(Guid id)
    {
        return MapDbToApi(GetDbById(id));
    }

    public ItemCategory Create(string name, string icon)
    {
        DbItemCategory dbItemCategory = new DbItemCategory
        {
            Id = Guid.NewGuid(),
            Name = name,
            Icon = icon,
            Active = true
        };

        _dbContext.ItemCategories.Add(dbItemCategory);
        _dbContext.SaveChanges();

        return MapDbToApi(dbItemCategory);
    }

    public ItemCategory Edit(Guid id, string name, string icon)
    {
        DbItemCategory dbItemCategory = _dbContext.ItemCategories.FirstOrDefault(itemCategory => itemCategory.Id == id) ?? new DbItemCategory();

        if (dbItemCategory.Id != Guid.Empty)
        {
            _dbContext.ItemCategories.Update(dbItemCategory);
            dbItemCategory.Name = name;
            dbItemCategory.Icon = icon;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbItemCategory);
    }

    public ItemCategory SetActive(Guid id, bool active)
    {
        DbItemCategory dbItemCategory = _dbContext.ItemCategories.FirstOrDefault(itemCategory => itemCategory.Id == id) ?? new DbItemCategory();

        if (dbItemCategory.Id != Guid.Empty)
        {
            _dbContext.ItemCategories.Update(dbItemCategory);
            dbItemCategory.Active = active;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbItemCategory);
    }

    public ItemCategory UpdateOrder(Guid id, int order)
    {
        DbItemCategory dbItemCategory = _dbContext.ItemCategories.FirstOrDefault(itemCategory => itemCategory.Id == id) ?? new DbItemCategory();

        if (dbItemCategory.Id != Guid.Empty)
        {
            _dbContext.ItemCategories.Update(dbItemCategory);
            dbItemCategory.Order = order;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbItemCategory);
    }

    public List<ItemCategory> MassUpdateOrders(List<ItemCategory> itemCategories)
    {
        List<DbItemCategory> dbItemCategories = _dbContext.ItemCategories.ToList();

        foreach (ItemCategory itemCategory in itemCategories)
        {
            DbItemCategory dbItemCategory = dbItemCategories.FirstOrDefault(dbItemCategory => dbItemCategory.Id == itemCategory.Id) ?? new DbItemCategory();
            if (dbItemCategory.Id != Guid.Empty)
            {
                _dbContext.ItemCategories.Update(dbItemCategory);
                dbItemCategory.Order = itemCategory.Order;
                _dbContext.SaveChanges(true);
            }

        }

        return MapDbToApi(dbItemCategories);
    }

    public ItemCategory MapDbToApi(DbItemCategory dbItemCategory)
    {
        return new ItemCategory()
        {
            Id = dbItemCategory.Id,
            Name = dbItemCategory.Name,
            Active = dbItemCategory.Active,
            Icon = dbItemCategory.Icon,
            Order = dbItemCategory.Order,
        };
    }

    public List<ItemCategory> MapDbToApi(List<DbItemCategory> dbItemCategory)
    {
        return dbItemCategory.Select(dbItemCategory => new ItemCategory()
        {
            Id = dbItemCategory.Id,
            Name = dbItemCategory.Name,
            Active = dbItemCategory.Active,
            Icon = dbItemCategory.Icon,
            Order = dbItemCategory.Order,
        }).ToList();
    }

    public DbItemCategory GetDbById(Guid id)
    {
        return _dbContext.ItemCategories.FirstOrDefault(itemCategory => itemCategory.Id == id) ?? new DbItemCategory();
    }
}