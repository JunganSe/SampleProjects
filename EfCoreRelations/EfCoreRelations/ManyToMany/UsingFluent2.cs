// Utan navigation properties.

using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.ManyToMany.UsingFluent2;

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



public class ManyToManyUsingFluent2Context : BaseContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Child>()
            .HasKey(c => c.MyCustomPK);
        modelBuilder.Entity<Child>()
            .HasMany<Parent>()
            .WithMany();

        modelBuilder.Entity<Parent>()
            .HasKey(p => p.MyCustomPK);
    }
}
