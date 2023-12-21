namespace barboek.Interface.Models.Database;

public class DbItem
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public string FilePath { get; set; } = "";
    public List<DbPrice> Prices { get; set; } = new List<DbPrice>();
    public DbTaxType TaxType { get; set; } = new DbTaxType();
    public DbItemCategory ItemCategory { get; set; } = new DbItemCategory();
}