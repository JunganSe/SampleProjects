// Vanliga versionen med navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.WithJunctionTableUsingFluent1;

public class Parent
{
    // Key
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Parent_Child // Kopplingstabell som får en-till-många relation till både parent och child.
{
    // Key
    public int MyCustomParentFK { get; set; }
    public int MyCustomChildFK { get; set; }

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
    public Child Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyWithJunctionTableUsingFluent1Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(e => e.MyCustomPK);

        modelBuilder.Entity<Parent>()
            .HasKey(e => e.MyCustomPK);

        modelBuilder.Entity<Parent_Child>()
            .HasKey(e => new { e.MyCustomParentFK, e.MyCustomChildFK });
        modelBuilder.Entity<Parent_Child>()
            .HasOne(e => e.Child)
            .WithMany(e => e.Parent_Child)
            .HasForeignKey(e => e.MyCustomChildFK);
        modelBuilder.Entity<Parent_Child>()
            .HasOne(e => e.Parent)
            .WithMany(e => e.Parent_Child)
            .HasForeignKey(e => e.MyCustomParentFK);
    }
}
