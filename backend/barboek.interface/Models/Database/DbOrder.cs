namespace barboek.Interface.Models.Database;

public class DbOrder
{
    public Guid Id { get; set; } = Guid.Empty;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public List<DbOrderItem> OrderedItems { get; set; } = new List<DbOrderItem>();
    public DbPriceType PriceType { get; set; }
}