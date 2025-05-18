using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations;
public class BaseContext : DbContext
{
    public BaseContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //optionsBuilder.UseInMemoryDatabase("EfCoreRelations");
            Directory.CreateDirectory("Database");
            optionsBuilder.UseSqlite("Data Source=Database/EfCoreRelations.sqlite");
        }
    }
}
