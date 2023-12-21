namespace barboek.Interface.Models.API;

public struct Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FilePath { get; set; }
    public List<Price>? Prices { get; set; }
    public Price? ActivePrice { get; set; }
    public TaxType TaxType { get; set; }
    public TaxTypeInstance? ActiveInstance { get; set; }
    public ItemCategory ItemCategoryId { get; set; }
}