namespace barboek.Interface.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public Account AccountOrdered { get; set; }
    public List<OrderItem> OrderedItems { get; set; }
}

public class OrderItem
{
    public Guid Id { get; set; }
    public Item Item { get; set; }
    public int Quantity { get; set; }
}