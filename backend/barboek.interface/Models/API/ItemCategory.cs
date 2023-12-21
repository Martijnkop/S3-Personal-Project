namespace barboek.Interface.Models.API;

public struct ItemCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public bool Active { get; set; }
    public int Order { get; set; }
}