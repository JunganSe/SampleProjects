// Vanliga versionen med navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.UsingFluent1;

public class Parent
{
    // Key
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Child> Children { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent> Parents { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyUsingFluent1Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(e => e.MyCustomPK);
        modelBuilder.Entity<Child>()
            .HasMany(e => e.Parents)
            .WithMany(e => e.Children);

        modelBuilder.Entity<Parent>()
            .HasKey(e => e.MyCustomPK);
    }
}
