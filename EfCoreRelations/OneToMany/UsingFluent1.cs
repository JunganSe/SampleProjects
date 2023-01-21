// Vanliga versionen med navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.OneToManyUsingFluent1;

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
    public int MyCustomParentFK { get; set; }

    // Nav
    public Parent Parent { get; set; } // Navigation property som EF tolkar för att skapa relationen.
}



public class OneToMany1Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(e => e.MyCustomPK);
        modelBuilder.Entity<Child>()
            .HasOne(e => e.Parent)
            .WithMany(e => e.Children)
            .HasForeignKey(e => e.MyCustomParentFK);

        modelBuilder.Entity<Parent>()
            .HasKey(e => e.MyCustomPK);
    }
}