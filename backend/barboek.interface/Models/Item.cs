using System.ComponentModel.DataAnnotations.Schema;

namespace barboek.Interface.Models;
public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    [NotMapped]
    public double CurrentPrice { get; set; }
    //public string Description { get; set; }
    public List<Price> Prices { get; set; }
    //public TaxType TaxType { get; set; }
    //public string ImagePath { get; set; }
}