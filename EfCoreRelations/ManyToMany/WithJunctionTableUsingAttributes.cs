using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreRelations.ManyToMany.WithJunctionTableUsingAttributes;

public class Parent
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent_Child> Parent_Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Parent_Child // Kopplingstabell som får en-till-många relation till både parent och child.
{
    // Key
    [ForeignKey(nameof(Parent))]
    public int MyCustomParentFK { get; set; }
    [ForeignKey(nameof(Child))]
    public int MyCustomChildFK { get; set; }

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
    public Child Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyWithJunctionTableUsingAttributesContext : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kompositnyckeln i kopplingstabellen måste anges med fluent.
        modelBuilder.Entity<Parent_Child>()
            .HasKey(e => new { e.MyCustomParentFK, e.MyCustomChildFK });
    }
}
