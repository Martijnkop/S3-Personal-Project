using barboek.Interface.Models.Database;

namespace barboek.Interface.Models.API;

public struct Price
{
    public Guid Id { get; set; }
    public float Amount { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public PriceType PriceType { get; set; }
}