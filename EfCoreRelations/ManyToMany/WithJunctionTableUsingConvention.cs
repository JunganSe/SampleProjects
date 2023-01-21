using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.WithJunctionTableUsingConvention;

public class Parent
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Parent_Child // Kopplingstabell som får en-till-många relation till både parent och child.
{
    // Key
    public int ParentId { get; set; } // Blir FK genom konvention, pga namnet.
    public int ChildId { get; set; } // Blir FK genom konvention, pga namnet.

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
    public Child Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyWithJunctionTableUsingConventionContext : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kompositnyckeln i kopplingstabellen måste anges med fluent.
        modelBuilder.Entity<Parent_Child>()
            .HasKey(e => new {e.ParentId, e.ChildId });
    }
}
