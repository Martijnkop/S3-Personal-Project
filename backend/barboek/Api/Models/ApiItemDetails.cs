using barboek.Interface.Models.API;

namespace barboek.Api.Models;

public class ApiItemDetails
{
    public string Name { get; set; }
    public Dictionary<Guid, string> ItemPrices { get; set; } // for each item PriceType
    public Guid TaxTypeId { get; set; }
    public Guid CategoryId { get; set; }
    public IFormFile Image { get; set; }
}