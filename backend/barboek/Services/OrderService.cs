using barboek.Data;
using barboek.Interface.Models;

namespace barboek.Services;

public class OrderService
{
    private DataStore _dbContext;

    public OrderService(DataStore dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateOrder(Guid accountId, Dictionary<Guid, int> itemIds)
    {
        Order order = new Order
        {
            Id = Guid.NewGuid(),
            CreatedTime = DateTime.UtcNow,
            AccountOrdered = _dbContext.Accounts.FirstOrDefault(account => account.Id == accountId),
            OrderedItems = _dbContext.Items.AsEnumerable()
                .Where(item => itemIds.ContainsKey(item.Id))
                .Select(item => new OrderItem
                {
                    Item = item,
                    Quantity = itemIds[item.Id]
                })
                .ToList()
        };

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
}