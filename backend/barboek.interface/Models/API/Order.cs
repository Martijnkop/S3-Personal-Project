namespace barboek.Interface.Models.API;

public struct Order
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public List<OrderItem> OrderedItems { get; set; }
    public PriceType PriceType { get; set; }
}