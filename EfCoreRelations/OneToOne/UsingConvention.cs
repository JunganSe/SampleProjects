using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.OneToOne.UsingConvention;

public class Parent
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.

    // Nav
    public Child Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int Id { get; set; } // Blir PK genom konvention, pga namnet.
    public int ParentId { get; set; } // Blir FK genom konvention, pga namnet.

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class OneToOneUsingConventionContext : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }
}
