namespace barboek.Interface.Models.Database;

public class DbTaxType
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public List<DbTaxTypeInstance> Instances { get; set; } = new List<DbTaxTypeInstance>();
}