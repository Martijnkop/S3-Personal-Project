using barboek.Interface.Models.Database;

namespace barboek.Interface.Models;

public class DbOrderItem
{
    public Guid Id { get; set; } = Guid.Empty;
    public DbItem Item { get; set; } = new DbItem();
    public float Amount { get; set; } = 0f;
}