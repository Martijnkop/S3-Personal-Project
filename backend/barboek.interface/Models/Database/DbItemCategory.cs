namespace barboek.Interface.Models.Database;

public class DbItemCategory
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";
    public bool Active { get; set; } = true;
    public int Order { get; set; } = int.MinValue;
}