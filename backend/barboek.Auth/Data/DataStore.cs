using Microsoft.EntityFrameworkCore;

namespace barboek.Auth.Data;

public class DataStore : DbContext
{
    public DbSet<Account> Accounts { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=barboek_auth;Username=postgres;Password=3iYvGvz9");
    }
}