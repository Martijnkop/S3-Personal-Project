using barboek.Data;
using barboek.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace barboek.Services;

public class FinanceService
{
    public DataStore _dbContext;

    public FinanceService(DataStore dbContext)
    {
        _dbContext = dbContext;
    }


    public PriceType CreatePriceType(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;
        if (_dbContext.PriceTypes.Any(x => x.Name == name)) return null;

        var priceType = new PriceType { Id = Guid.NewGuid(), Name = name };

        _dbContext.PriceTypes.Add(priceType);
        _dbContext.SaveChanges();

        return priceType;
    }

    public PriceType GetPriceTypeById(Guid id)
    {
        if (id == null || id.Equals(Guid.Empty)) return null;
        PriceType priceType = _dbContext.PriceTypes.FirstOrDefault(x => x.Id == id);
        return priceType;
    }

    internal Price CreatePrice(double priceAmount, DateTime? beginDate, DateTime? endDate, PriceType priceType, string? name)
    {
        if (priceAmount == null || priceType == null || priceType.Id.Equals(Guid.Empty)) return null;

        Price price = new Price
        {
            Id = Guid.NewGuid(),
            Amount = priceAmount,
            BeginTime = beginDate,
            EndTime = endDate,
            CreatedDate = DateTime.UtcNow,
            Name = name,
            PriceType = priceType
        };

        _dbContext.Prices.Add(price);
        _dbContext.SaveChanges();

        return price;
    }

    internal Price CreatePriceWithItem(double priceAmount, DateTime? beginDate, DateTime? endDate, PriceType priceType, string? name, Item item)
    {
        Price price = CreatePrice(priceAmount, beginDate, endDate, priceType, name);

        if (price == null) return null;

        _dbContext.Items.Update(item);
        item.Prices = new List<Price> { price };
        _dbContext.SaveChanges();

        return price;
    }

    internal Price GetPriceById(Guid id)
    {
        if (id.Equals(Guid.Empty)) return null;
        return _dbContext.Prices.FirstOrDefault(x => x.Id == id);
    }
}