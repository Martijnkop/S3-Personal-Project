using barboek.Interface;
using barboek.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace barboek.Data;

public class DataStore : DbContext
{
    public new DbSet<Account> Accounts { get; set; }
    public new DbSet<Item> Items { get; set; }
    public new DbSet<Order> Orders { get; set; }

    public new DbSet<PriceType> PriceTypes { get; set; }
    public new DbSet<Price> Prices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=barboek_main;Username=postgres;Password=3iYvGvz9");
        
    }

}