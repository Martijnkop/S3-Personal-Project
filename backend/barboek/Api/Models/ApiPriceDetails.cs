namespace barboek.Api.Models;

public struct ApiPriceDetails
{
    public Guid PriceTypeId { get; set; }
    public Guid TaxTypeId { get; set; }

    public float Price { get; set; }
    public DateTime StartTime {  get; set; }
    public DateTime EndTime { get; set; }
}