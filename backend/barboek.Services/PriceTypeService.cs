using barboek.Data;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using System;

namespace barboek.Services;

public class PriceTypeService : IPriceTypeService, IDbPriceTypeService
{
    private DataStore _dbContext;
    public PriceTypeService(DataStore dataStore)
    {
        _dbContext = dataStore;
    }

    public PriceType Create(string name)
    {
        DbPriceType dbPriceType = new DbPriceType
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        _dbContext.PriceTypes.Add(dbPriceType);
        _dbContext.SaveChanges();

        return MapDbToApi(dbPriceType);
    }

    public List<PriceType> GetAll()
    {
        List<DbPriceType> dbPriceTypes = _dbContext.PriceTypes.ToList();
        return MapDbToApi(dbPriceTypes);
    }

    public PriceType GetById(Guid id)
    {
        return MapDbToApi(GetDbById(id));
    }

    public PriceType ChangeName(Guid id, string name)
    {
        DbPriceType dbPriceType = _dbContext.PriceTypes.FirstOrDefault(priceType => priceType.Id == id) ?? new DbPriceType();

        if (dbPriceType.Id != Guid.Empty)
        {
            _dbContext.PriceTypes.Update(dbPriceType);
            dbPriceType.Name = name;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbPriceType);
    }

    public PriceType SetActive(Guid id, bool active)
    {
        DbPriceType dbPriceType = _dbContext.PriceTypes.FirstOrDefault(priceType => priceType.Id == id) ?? new DbPriceType();

        if (dbPriceType.Id != Guid.Empty)
        {
            _dbContext.PriceTypes.Update(dbPriceType);
            dbPriceType.Active = active;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbPriceType);
    }

    public PriceType MapDbToApi(DbPriceType dbPriceType)
    {
        PriceType priceType = new PriceType()
        {
            Id = dbPriceType.Id,
            Name = dbPriceType.Name,
            Active = dbPriceType.Active,
        };
        return priceType;
    }

    public List<PriceType> MapDbToApi(List<DbPriceType> dbPriceTypes)
    {
        return dbPriceTypes.Select(dbPriceType => new PriceType()
        {
            Id = dbPriceType.Id,
            Name = dbPriceType.Name,
            Active = dbPriceType.Active,
        }).ToList();
    }

    public DbPriceType GetDbById(Guid id)
    {
        return _dbContext.PriceTypes.FirstOrDefault(priceType => priceType.Id == id) ?? new DbPriceType();
    }
}