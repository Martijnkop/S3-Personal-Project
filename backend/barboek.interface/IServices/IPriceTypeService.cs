using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface IPriceTypeService
{
    public List<PriceType> GetAll();
    public PriceType Create(string name);
    PriceType GetById(Guid id);
    PriceType ChangeName(Guid id, string name);
    PriceType SetActive(Guid id, bool active);
}