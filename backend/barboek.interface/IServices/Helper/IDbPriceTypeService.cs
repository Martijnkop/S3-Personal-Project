using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IDbPriceTypeService
{
    DbPriceType GetDbById(Guid id);
    PriceType MapDbToApi(DbPriceType dbPriceType);
    List<PriceType> MapDbToApi(List<DbPriceType> dbPriceTypes);
}