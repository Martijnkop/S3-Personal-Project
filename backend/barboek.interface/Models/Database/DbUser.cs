namespace barboek.Interface.Models.Database;

public class DbUser
{
    public Guid Id {  get; set; } = Guid.Empty;
    public string Name { get; set; } = "";
    public float Balance { get; set; } = 0f;
}