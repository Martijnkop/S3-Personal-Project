using barboek.Data;
using barboek.Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace barboek.Services;

public class ItemService
{
    private DataStore _dbContext;
    private FinanceService _financeService;

    public ItemService(DataStore dbContext, FinanceService financeService)
    {
        _dbContext = dbContext;
        _financeService = financeService;
    }

    public List<Item> GetItems()
    {
        //temp

        if (_dbContext.Items.IsNullOrEmpty())
        {
            AddItem("test", Guid.Empty);
        }

        List<Item> items = _dbContext.Items.Include("Prices").ToList();

        items.ForEach(item => item.CurrentPrice = item.Prices
        .Where(price => price.EndTime == DateTime.MinValue || price.EndTime > DateTime.UtcNow)
        .Where(price => price.BeginTime == DateTime.MinValue || price.BeginTime < DateTime.UtcNow)
        .OrderByDescending(price => price.CreatedDate).First().Amount);
        items.ForEach(item => item.Prices = null);

        return items;
    }

    public void AddItem(string name, Guid priceId)
    {
        if (string.IsNullOrEmpty(name) || _dbContext.Items.Any(item => item.Name == name)) return;
        if (priceId.Equals(Guid.Empty)) return;

        Price price = _financeService.GetPriceById(priceId);
        if (price == null) return;

        List<Price> priceList = new List<Price>{
            price
        };

        _dbContext.Items.Add(new Item { Id = Guid.NewGuid(), Name = name, Prices = priceList });
        _dbContext.SaveChanges();

    }

    internal Item GetItemById(Guid itemId)
    {
        if (itemId == null || itemId.Equals(Guid.Empty)) return null;
        Item item = _dbContext.Items.FirstOrDefault(x => x.Id == itemId);
        return item;
    }
}