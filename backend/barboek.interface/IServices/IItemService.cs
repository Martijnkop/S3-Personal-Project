using barboek.Interface.Models.API;

namespace barboek.Interface.IServices;
public interface IItemService
{
    List<Item> GetAll();
    Item GetById(Guid id);
    List<Item> GetAllWithActivePrice(Guid priceTypeId);
    List<Item> GetByCategory(Guid categoryId, Guid priceTypeId);
    Item Create(string name, string path, Dictionary<Guid, float> itemPrices, Guid categoryId, Guid taxTypeId);
}