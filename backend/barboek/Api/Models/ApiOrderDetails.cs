using barboek.Interface.Models.API;

namespace barboek.Api.Models;

public struct ApiOrderDetails
{
    public Dictionary<Guid, float> ItemIdsWithAmounts { get; set; }
    public Guid PriceTypeId { get; set; }
}