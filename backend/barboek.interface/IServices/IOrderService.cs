using barboek.Interface.Models.API;

namespace barboek.Interface.IServices;

public interface IOrderService
{
    List<Order> GetAll(DateTime? startTime, DateTime? endTime);
    Order GetById(Guid id);
    List<Order> GetByCustomer(Guid userId, DateTime startTime, DateTime endTime);
    List<Order> GetBySeller(Guid sellerId, DateTime startTime, DateTime endTime);
    void Create(Dictionary<Guid, float> itemIdsWithAmounts, Guid priceTypeId);
}