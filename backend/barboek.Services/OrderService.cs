using barboek.Data;
using barboek.Interface.IServices;
using barboek.Interface.Models;
using barboek.Interface.Models.API;
using barboek.Interface.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace barboek.Services;

public class OrderService : IOrderService
{
    private DataStore _dbContext;
    private IDbItemService _itemService;
    private IDbPriceTypeService _priceTypeService;

    public OrderService(DataStore dbContext, IDbItemService itemService, IDbPriceTypeService priceTypeService)
    {
        _dbContext = dbContext;
        _itemService = itemService;
        _priceTypeService = priceTypeService;
    }
    public List<Order> GetAll(DateTime? startTime, DateTime? endTime)
    {
        List<DbOrder> dbOrders = new List<DbOrder>();

        dbOrders = _dbContext.Orders
            .Include(order => order.OrderedItems)
                .ThenInclude(orderItem => orderItem.Item)
                    .ThenInclude(item => item.Prices)
                        .ThenInclude(price => price.PriceType)
            .Include(order => order.OrderedItems)
                .ThenInclude(orderItem => orderItem.Item)
                    .ThenInclude(item => item.TaxType)
                        .ThenInclude(taxType => taxType.Instances)
            .ToList();

        List<Order> orders = dbOrders.Select(dbOrder =>
        {
            DateTime createdTime = dbOrder.CreatedTime;

            Order order = new Order
            {
                Id = dbOrder.Id,
                CreatedTime = createdTime,
                OrderedItems = dbOrder.OrderedItems.Select(orderItem =>
                {
                    DbPrice activePrice = orderItem.Item.Prices
                    .OrderByDescending(dbPrice => dbPrice.CreatedTime)
                    .FirstOrDefault(dbPrice => (dbPrice.StartTime == null || dbPrice.StartTime <= createdTime) && (dbPrice.EndTime == null || dbPrice.EndTime >= createdTime)) ?? new DbPrice();

                    DbTaxTypeInstance activeTaxTypeInstance = orderItem.Item.TaxType.Instances
                        .OrderByDescending(instance => instance.CreatedTime)
                        .FirstOrDefault(instance => (instance.BeginTime == null || instance.BeginTime <= createdTime) && (instance.EndTime == null || instance.EndTime >= createdTime)) ?? new DbTaxTypeInstance();
                    return new OrderItem
                    {
                        Amount = orderItem.Amount,
                        Item = _itemService.MapDbToApi(orderItem.Item, activePrice, activeTaxTypeInstance)
                    };
                }).ToList(),
                PriceType = _priceTypeService.MapDbToApi(dbOrder.PriceType)
            };

            return order;
        }).ToList();

        Console.WriteLine(orders);
        return orders;
    }

    public Order GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Order> GetByCustomer(Guid userId, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }


    public List<Order> GetBySeller(Guid sellerId, DateTime startTime, DateTime endTime)
    {
        throw new NotImplementedException();
    }

    public void Create(Dictionary<Guid, float> itemIdsWithAmounts, Guid priceTypeId)
    {
        List<DbOrderItem> orderItems = new List<DbOrderItem>();

        foreach (var itemIdWithAmount in itemIdsWithAmounts)
        {
            orderItems.Add(new DbOrderItem
            {
                Id = Guid.NewGuid(),
                Item = _itemService.GetDbById(itemIdWithAmount.Key),
                Amount = itemIdWithAmount.Value,
            });
        }

        DbOrder dbOrder = new DbOrder
        {
            Id = Guid.NewGuid(),
            CreatedTime = DateTime.UtcNow,
            OrderedItems = orderItems,
            PriceType = _priceTypeService.GetDbById(priceTypeId)
        };

        _dbContext.Orders.Add(dbOrder);
        _dbContext.SaveChanges();
    }


}
