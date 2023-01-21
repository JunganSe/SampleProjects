// Vanliga versionen med navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.OneToOne.UsingFluent1;

public class Parent
{
    // Key
    public int MyCustomPK { get; set; }

    // Nav
    public Child Child { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}

public class Child
{
    // Key
    public int MyCustomPK { get; set; }
    public int MyCustomParentFK { get; set; }

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class OneToOne1Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(e => e.MyCustomPK);
        modelBuilder.Entity<Child>()
            .HasOne(e => e.Parent)
            .WithOne(e => e.Child)
            .HasForeignKey<Child>(e => e.MyCustomParentFK);

        modelBuilder.Entity<Parent>()
            .HasKey(e => e.MyCustomPK);
    }
}