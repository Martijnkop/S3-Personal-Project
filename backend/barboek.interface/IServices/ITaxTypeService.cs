using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;

namespace barboek.Interface.IServices;

public interface ITaxTypeService
{
    List<TaxType> GetAll();
    TaxType Create(string name, float percentage, DateTime? beginTime, DateTime? endTime);
    TaxType GetById(Guid id);
    TaxType ChangeName(Guid id, string name);
    TaxType CreateInstance(Guid taxTypeId, float percentage, DateTime beginTime, DateTime endTime);
    bool UpdateInstance(Guid instanceId, float percentage, DateTime beginTime, DateTime endTime);

}