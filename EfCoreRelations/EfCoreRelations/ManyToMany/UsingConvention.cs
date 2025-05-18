using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.UsingConvention;

public class Parent
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.

    // Nav
    public ICollection<Child> Children { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.

    // Nav
    public ICollection<Parent> Parents { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class ManyToManyUsingConventionContext : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }
}
