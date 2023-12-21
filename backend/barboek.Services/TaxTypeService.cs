using barboek.Data;
using barboek.Interface.IServices;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace barboek.Services;

public class TaxTypeService : ITaxTypeService, IDbTaxTypeService
{
    private DataStore _dbContext;
    public TaxTypeService(DataStore dataStore)
    {
        _dbContext = dataStore;
    }

    public List<TaxType> GetAll()
    {
        return MapDbToApi(_dbContext.TaxTypes.Include(taxType => taxType.Instances).ToList());
    }

    public TaxType GetById(Guid id)
    {
        return MapDbToApi(GetDbById(id));
    }

    public TaxType Create(string name, float percentage, DateTime? beginTime, DateTime? endTime)
    {
        if (beginTime != null && beginTime == DateTime.MinValue) beginTime = null;
        if (endTime != null && endTime == DateTime.MinValue) endTime = null;

        DbTaxTypeInstance instance = new DbTaxTypeInstance
        {
            Id = Guid.NewGuid(),
            Percentage = percentage,
            BeginTime = beginTime,
            EndTime = endTime,
            CreatedTime = DateTime.UtcNow,
        };

        DbTaxType dbTaxType = new DbTaxType
        {
            Id = Guid.NewGuid(),
            Name = name,
            Instances = new List<DbTaxTypeInstance> { instance }
        };

        _dbContext.TaxTypes.Add(dbTaxType);
        _dbContext.SaveChanges();

        return MapDbToApi(dbTaxType);
    }

    public TaxType ChangeName(Guid id, string name)
    {
        DbTaxType dbTaxType = _dbContext.TaxTypes.FirstOrDefault(dbTaxType => dbTaxType.Id == id) ?? new DbTaxType();

        if (dbTaxType.Id != Guid.Empty)
        {
            _dbContext.TaxTypes.Update(dbTaxType);
            dbTaxType.Name = name;
            _dbContext.SaveChanges(true);
        }

        return MapDbToApi(dbTaxType);
    }

    public TaxType MapDbToApi(DbTaxType dbTaxType)
    {
        return new TaxType()
        {
            Id = dbTaxType.Id,
            Name = dbTaxType.Name,
            Instances = dbTaxType.Instances.Select(dbTaxTypeInstance => new TaxTypeInstance()
            {
                Id = dbTaxTypeInstance.Id,
                Percentage = dbTaxTypeInstance.Percentage,
                BeginTime = dbTaxTypeInstance.BeginTime,
                EndTime = dbTaxTypeInstance.EndTime,
                CreatedTime = dbTaxTypeInstance.CreatedTime,
            }).ToList()
        };
    }

    public List<TaxType> MapDbToApi(List<DbTaxType> dbTaxTypes)
    {
        return dbTaxTypes.Select(dbTaxType => new TaxType()
        {
            Id = dbTaxType.Id,
            Name = dbTaxType.Name,
            Instances = dbTaxType.Instances.Select(dbTaxTypeInstance => new TaxTypeInstance()
            {
                Id = dbTaxTypeInstance.Id,
                Percentage = dbTaxTypeInstance.Percentage,
                BeginTime = dbTaxTypeInstance.BeginTime,
                EndTime = dbTaxTypeInstance.EndTime,
                CreatedTime = dbTaxTypeInstance.CreatedTime,
            }).ToList()
        }).ToList();
    }

    public TaxType CreateInstance(Guid taxTypeId, float percentage, DateTime beginTime, DateTime endTime)
    {
        // In init, a Guid.Empty is given
        DbTaxType dbTaxType = _dbContext.TaxTypes.FirstOrDefault(taxType => taxType.Id == taxTypeId) ?? new DbTaxType();

        // This gets handled as an error in the Controller layer
        if (dbTaxType.Id == Guid.Empty) return new TaxType { Id = Guid.Empty };


        DbTaxTypeInstance dbTaxTypeInstance = new DbTaxTypeInstance
        {
            Id = Guid.NewGuid(),
            BeginTime = beginTime,
            EndTime = endTime,
            Percentage = percentage,
        };

        dbTaxType.Instances.Add(dbTaxTypeInstance);


        _dbContext.TaxTypes.Update(dbTaxType);
        _dbContext.SaveChanges();

        // Map DB entity to Controller entity
        return MapDbToApi(dbTaxType);
    }

    public bool UpdateInstance(Guid instanceId, float percentage, DateTime beginTime, DateTime endTime)
    {
        DbTaxTypeInstance dbTaxTypeInstance = _dbContext.TaxTypeInstances.FirstOrDefault(instance => instance.Id == instanceId) ?? new DbTaxTypeInstance();

        if (dbTaxTypeInstance.Id == Guid.Empty) return false;

        _dbContext.TaxTypeInstances.Update(dbTaxTypeInstance);
        dbTaxTypeInstance.BeginTime = beginTime;
        dbTaxTypeInstance.EndTime = endTime;
        _dbContext.SaveChanges();

        return true;
    }

    public DbTaxType GetDbById(Guid id)
    {
        return _dbContext.TaxTypes.FirstOrDefault(priceType => priceType.Id == id) ?? new DbTaxType();
    }

    public TaxTypeInstance? MapInstanceToApi(DbTaxTypeInstance? dbTaxTypeInstance)
    {
        if (dbTaxTypeInstance == null) return null;

        return new TaxTypeInstance
        {
            Id = dbTaxTypeInstance.Id,
            Percentage = dbTaxTypeInstance.Percentage,
            BeginTime = dbTaxTypeInstance.BeginTime,
            EndTime = dbTaxTypeInstance.EndTime,
            CreatedTime = dbTaxTypeInstance.CreatedTime,
        };
    }
}