using barboek.Interface;
using barboek.Interface.Models;
using barboek.Interface.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace barboek.Data;

public class DataStore : DbContext
{
    public new DbSet<DbUser> Users { get; set; }
    public new DbSet<DbTaxType> TaxTypes { get; set; }
    public new DbSet<DbTaxTypeInstance> TaxTypeInstances { get; set; }
    public new DbSet<DbPriceType> PriceTypes { get; set; }
    public new DbSet<DbItemCategory> ItemCategories { get; set; }
    public new DbSet<DbItem> Items { get; set; }
    public new DbSet<DbPrice> Prices { get; set; }
    public new DbSet<DbOrder> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=barboek_main;Username=postgres;Password=3iYvGvz9");
        
    }

}