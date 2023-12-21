using System.ComponentModel.DataAnnotations.Schema;

namespace barboek.Interface.Models.API;

public struct TaxTypeInstance
{
    public Guid Id { get; set; }
    public float Percentage { get; set; }
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedTime { get; set; }
}