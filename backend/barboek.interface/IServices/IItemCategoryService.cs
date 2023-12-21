using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IItemCategoryService
{
    List<ItemCategory> GetAll();
    ItemCategory GetById(Guid id);
    ItemCategory Create(string name, string iconString);
    ItemCategory Edit(Guid id, string name, string iconString);
    ItemCategory SetActive(Guid id, bool active);
    List<ItemCategory> MassUpdateOrders(List<ItemCategory> itemCategories);
}