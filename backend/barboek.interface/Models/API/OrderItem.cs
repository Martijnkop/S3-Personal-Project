using barboek.Interface.Models.API;

namespace barboek.Interface.Models;

public struct OrderItem
{
    public Item Item { get; set; }
    public float Amount { get; set; }
}