// Utan navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.WithJunctionTableUsingFluent2;

public class Parent
{
    // Key
    public int MyCustomPK { get; set; }
}

public class Child
{
    // Key
    public int MyCustomPK { get; set; }
}

public class Parent_Child // Kopplingstabell som får en-till-många relation till både parent och child.
{
    // Key
    public int MyCustomParentFK { get; set; }
    public int MyCustomChildFK { get; set; }
}



public class ManyToManyWithJunctionTableUsingFluent2Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(c => c.MyCustomPK);

        modelBuilder.Entity<Parent>()
            .HasKey(p => p.MyCustomPK);

        modelBuilder.Entity<Parent_Child>()
            .HasKey(pc => new { pc.MyCustomParentFK, pc.MyCustomChildFK });
        modelBuilder.Entity<Parent_Child>()
            .HasOne<Child>()
            .WithMany()
            .HasForeignKey(pc => pc.MyCustomChildFK);
        modelBuilder.Entity<Parent_Child>()
            .HasOne<Parent>()
            .WithMany()
            .HasForeignKey(pc => pc.MyCustomParentFK);
    }
}
