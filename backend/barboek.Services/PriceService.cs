using barboek.Data;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using Microsoft.Extensions.Logging.Abstractions;

namespace barboek.Services;

public class PriceService : IPriceService, IDbPriceService
{
    private DataStore _dbContext;
    private IDbPriceTypeService _dbPriceTypeService;
    public PriceService(DataStore dataStore, IDbPriceTypeService priceTypeService)
    {
        _dbContext = dataStore;

        _dbPriceTypeService = priceTypeService;
    }

    public DbPrice GetDbById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Price? MapDbToApi(DbPrice? dbPrice)
    {
        if (dbPrice == null) return null;
        return new Price
        {
            Id = dbPrice.Id,
            Amount = dbPrice.Price,
            CreatedTime = dbPrice.CreatedTime,
            StartTime = dbPrice.StartTime,
            EndTime = dbPrice.EndTime,
            PriceType = _dbPriceTypeService.MapDbToApi(dbPrice.PriceType)
        };
    }

    public List<Price> MapDbToApi(List<DbPrice> dbPrices)
    {
        return dbPrices.Select(dbPrice => new Price
        {
            Id = dbPrice.Id,
            Amount = dbPrice.Price,
            CreatedTime = dbPrice.CreatedTime,
            StartTime = dbPrice.StartTime,
            EndTime = dbPrice.EndTime,
            PriceType = _dbPriceTypeService.MapDbToApi(dbPrice.PriceType)
        }).ToList();
    }
}