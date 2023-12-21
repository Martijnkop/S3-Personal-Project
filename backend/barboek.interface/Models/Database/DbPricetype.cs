namespace barboek.Interface.Models.Database;

public class DbPriceType
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public bool Active { get; set; } = true;
}