using barboek.Interface;
using Microsoft.EntityFrameworkCore;

namespace barboek.Data;

public class DataStore : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=barboek_main;Username=postgres;Password=3iYvGvz9");
        
    }

}