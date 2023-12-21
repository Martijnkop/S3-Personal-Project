using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IDbTaxTypeService
{
    DbTaxType GetDbById(Guid id);
    TaxType MapDbToApi(DbTaxType dbTaxType);
    List<TaxType> MapDbToApi(List<DbTaxType> dbTaxTypes);
    TaxTypeInstance? MapInstanceToApi(DbTaxTypeInstance? dbTaxTypeInstance);


}