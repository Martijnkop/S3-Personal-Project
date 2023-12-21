using System.ComponentModel.DataAnnotations.Schema;

namespace barboek.Interface.Models.Database;

public class DbTaxTypeInstance
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; } = Guid.Empty;
    public float Percentage { get; set; } = 0f;
    public DateTime? BeginTime { get; set; } = DateTime.MinValue;
    public DateTime? EndTime { get; set; } = DateTime.MinValue;
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
}