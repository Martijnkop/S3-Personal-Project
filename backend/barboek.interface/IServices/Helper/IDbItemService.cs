using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IDbItemService
{
    DbItem GetDbById(Guid id);
    Item MapDbToApi(DbItem dbItem, DbPrice? activePrice = null, DbTaxTypeInstance activeInstance = null);
    List<Item> MapDbToApi(List<DbItem> dbItem, DbPrice? activePrice = null);
}