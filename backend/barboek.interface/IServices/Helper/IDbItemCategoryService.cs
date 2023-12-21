using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IDbItemCategoryService
{
    DbItemCategory GetDbById(Guid id);
    ItemCategory MapDbToApi(DbItemCategory dbItemCategory);
    List<ItemCategory> MapDbToApi(List<DbItemCategory> dbItemCategory);
}