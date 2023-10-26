using barboek.Interface;
using barboek.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace barboek.Data;

public class DataStore : DbContext
{
    public new DbSet<Account> Accounts { get; set; }
    public new DbSet<Item> Items { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=barboek_main;Username=postgres;Password=3iYvGvz9");
        
    }

}