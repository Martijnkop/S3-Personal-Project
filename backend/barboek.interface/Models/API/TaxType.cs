namespace barboek.Interface.Models.API;

public struct TaxType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<TaxTypeInstance> Instances { get; set; }
}