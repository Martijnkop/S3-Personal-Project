using barboek.Data;
using barboek.Data.Migrations;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace barboek.Services;

public class ItemService : IItemService, IDbItemService
{
    private DataStore _dbContext;
    private IDbItemCategoryService _itemCategoryService { get; set; }
    private IDbTaxTypeService _taxTypeService { get; set; }
    private IDbPriceTypeService _priceTypeService { get; set; }
    private IDbPriceService _priceService { get; set; }

    public ItemService(DataStore dbContext, IDbPriceService priceService, IDbItemCategoryService itemCategoryService, IDbTaxTypeService taxTypeService, IDbPriceTypeService priceTypeService)
    {
        _dbContext = dbContext;

        _priceService = priceService;
        _itemCategoryService = itemCategoryService;
        _taxTypeService = taxTypeService;
        _priceTypeService = priceTypeService;
    }

    public Item Create(string name, string path, Dictionary<Guid, float> itemPrices, Guid categoryId, Guid taxTypeId)
    {
        DbItemCategory dbItemCategory = _itemCategoryService.GetDbById(categoryId);
        if (dbItemCategory.Id == Guid.Empty) return new Item();

        DbTaxType dbTaxType = _taxTypeService.GetDbById(taxTypeId);
        if (dbTaxType == null) return new Item();

        List<DbPrice> prices = new List<DbPrice>();

        foreach (var item in itemPrices)
        {
            DbPriceType dbPriceType = _priceTypeService.GetDbById(item.Key);
            DbPrice dbPrice = new DbPrice
            {
                Id = Guid.NewGuid(),
                Price = item.Value,
                CreatedTime = DateTime.UtcNow,
                PriceType = dbPriceType,
            };

            prices.Add(dbPrice);
        }

        DbItem dbItem = new DbItem
        {
            Id = Guid.NewGuid(),
            Name = name,
            FilePath = path,
            ItemCategory = dbItemCategory,
            TaxType = dbTaxType,
            Prices = prices,
        };

        _dbContext.Items.Add(dbItem);
        int a = _dbContext.SaveChanges();

        return MapDbToApi(dbItem);
    }

    public List<Item> GetAllWithActivePrice(Guid priceTypeId)
    {
        DbPriceType dbPriceType = _priceTypeService.GetDbById(priceTypeId);

        List<DbItem> dbItems =  _dbContext.Items
            .Include(dbItem => dbItem.Prices)
                .ThenInclude(dbPrice => dbPrice.PriceType)
            .Include(dbItem => dbItem.ItemCategory)
            .Include(dbItem => dbItem.TaxType)
                .ThenInclude(dbTaxType => dbTaxType.Instances)
            .ToList();

        List<Item> items = dbItems.Select(dbItem =>
        {
            DbPrice activePrice = dbItem.Prices
                .Where(dbPrice => dbPrice.PriceType == dbPriceType)
                .Where(dbPrice => dbPrice.StartTime == null || dbPrice.StartTime <= DateTime.UtcNow)
                .Where(dbPrice => dbPrice.EndTime == null || dbPrice.EndTime >= DateTime.UtcNow)
                .OrderByDescending(dbPrice => dbPrice.CreatedTime)
                .FirstOrDefault(new DbPrice());



            return new Item
            {
                Id = dbItem.Id,
                Name = dbItem.Name,
                FilePath = dbItem.FilePath,
                ItemCategoryId = _itemCategoryService.MapDbToApi(dbItem.ItemCategory),
                TaxType = _taxTypeService.MapDbToApi(dbItem.TaxType),
                ActivePrice = _priceService.MapDbToApi(activePrice)
            };
        }).ToList();

        return items;
    }

    public List<Item> GetAll()
    {
        List<DbItem> dbItems = _dbContext.Items
            .Include(dbItem => dbItem.Prices)
                .ThenInclude(dbPrice => dbPrice.PriceType)
            .Include(dbItem => dbItem.ItemCategory)
            .Include(dbItem => dbItem.TaxType)
                .ThenInclude(dbTaxType => dbTaxType.Instances)
            .ToList();

        return MapDbToApi(dbItems);
    }

    public Item GetById(Guid id)
    {
        return MapDbToApi(GetDbById(id));
    }

    public Item MapDbToApi(DbItem dbItem, DbPrice? activePrice = null, DbTaxTypeInstance? activeInstance = null)
    {
        return new Item

        {
            Id = dbItem.Id,
            Name = dbItem.Name,
            FilePath = dbItem.FilePath,
            ItemCategoryId = _itemCategoryService.MapDbToApi(dbItem.ItemCategory),
            TaxType = _taxTypeService.MapDbToApi(dbItem.TaxType),
            Prices = _priceService.MapDbToApi(dbItem.Prices),
            ActivePrice = _priceService.MapDbToApi(activePrice),
            ActiveInstance = _taxTypeService.MapInstanceToApi(activeInstance)
        };
    }

    public List<Item> MapDbToApi(List<DbItem> dbItems, DbPrice? activePrice = null)
    {
        List<Item> items = dbItems.Select(dbItem => new Item
        {
            Id = dbItem.Id,
            Name = dbItem.Name,
            FilePath = dbItem.FilePath,
            ItemCategoryId = _itemCategoryService.MapDbToApi(dbItem.ItemCategory),
            TaxType = _taxTypeService.MapDbToApi(dbItem.TaxType),
            Prices = _priceService.MapDbToApi(dbItem.Prices),
            ActivePrice = _priceService.MapDbToApi(activePrice)
        }).ToList();

        return items;
    }

    public DbItem GetDbById(Guid id)
    {
        DbItem dbItem = _dbContext.Items
            .Include(dbItem => dbItem.Prices)
                .ThenInclude(dbPrice => dbPrice.PriceType)
            .Include(dbItem => dbItem.ItemCategory)
            .Include(dbItem => dbItem.TaxType)
                .ThenInclude(dbTaxType => dbTaxType.Instances)
            .Where(dbItem => dbItem.Id == id)
            .FirstOrDefault() ?? new DbItem();

        return dbItem;
    }

    public List<Item> GetByCategory(Guid categoryId, Guid priceTypeId)
    {
        DbPriceType dbPriceType = _priceTypeService.GetDbById(priceTypeId);

        List<DbItem> dbItems = _dbContext.Items
            .Include(dbItem => dbItem.Prices)
                .ThenInclude(dbPrice => dbPrice.PriceType)
            .Include(dbItem => dbItem.ItemCategory)
            .Include(dbItem => dbItem.TaxType)
                .ThenInclude(dbTaxType => dbTaxType.Instances)
            .Where(dbItem => dbItem.ItemCategory.Id == categoryId)
            .ToList();

        List<Item> items = dbItems.Select(dbItem =>
        {
            DbPrice activePrice = dbItem.Prices
                .Where(dbPrice => dbPrice.PriceType == dbPriceType)
                .Where(dbPrice => dbPrice.StartTime == null || dbPrice.StartTime <= DateTime.UtcNow)
                .Where(dbPrice => dbPrice.EndTime == null || dbPrice.EndTime >= DateTime.UtcNow)
                .OrderByDescending(dbPrice => dbPrice.CreatedTime)
                .FirstOrDefault(new DbPrice());



            return new Item
            {
                Id = dbItem.Id,
                Name = dbItem.Name,
                FilePath = dbItem.FilePath,
                ItemCategoryId = _itemCategoryService.MapDbToApi(dbItem.ItemCategory),
                TaxType = _taxTypeService.MapDbToApi(dbItem.TaxType),
                ActivePrice = _priceService.MapDbToApi(activePrice)
            };
        }).ToList();

        return items;
    }
}