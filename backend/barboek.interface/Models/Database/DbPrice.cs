namespace barboek.Interface.Models.Database;

public class DbPrice
{
    public Guid Id { get; set; } = Guid.Empty;
    public float Price { get; set; }
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DbPriceType PriceType { get; set; } = new DbPriceType();
}