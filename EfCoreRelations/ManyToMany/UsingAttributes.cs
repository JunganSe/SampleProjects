using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelations.ManyToMany.UsingAttributes;

public class Parent
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Child> Children { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    [Key]
    public int MyCustomPK { get; set; }

    // Nav
    public ICollection<Parent> Parents { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyUsingAttributesContext : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }
}
