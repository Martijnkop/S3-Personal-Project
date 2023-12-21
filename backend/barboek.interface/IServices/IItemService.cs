using barboek.Interface.Models.API;

namespace barboek.Interface.IServices;
public interface IItemService
{
    List<Item> GetAll();
    Item GetById(Guid id);
    public List<Item> GetAllWithActivePrice(Guid priceTypeId);

    Item Create(string name, string path, Dictionary<Guid, float> itemPrices, Guid categoryId, Guid taxTypeId);
}