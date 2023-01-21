// Utan navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.OneToManyUsingFluent2;

public class Parent
{
    // Key
    public int MyCustomPK { get; set; }
}

public class Child
{
    // Key
    public int MyCustomPK { get; set; }
    public int MyCustomParentFK { get; set; }
}



public class OneToMany2Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(e => e.MyCustomPK);
        modelBuilder.Entity<Child>()
            .HasOne<Parent>()
            .WithMany()
            .HasForeignKey(e => e.MyCustomParentFK);

        modelBuilder.Entity<Parent>()
            .HasKey(e => e.MyCustomPK);
    }
}