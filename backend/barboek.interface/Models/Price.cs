namespace barboek.Interface.Models;
public class Price
{
    public Guid Id {  get; set; }
    public string? Name { get; set; }
    public double Amount { get; set; }
    public PriceType PriceType { get; set; }
    public DateTime? BeginTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedDate { get; set; }
}