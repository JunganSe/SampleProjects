using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations.OneToOne;

public class Parent
{
    // Key
    public int Id { get; set; }

    // Data
    public string Name { get; set; }

    // Nav
    public Child Child { get; set; }
}

public class Child
{
    // Key
    public int Id { get; set; }
    public int ParentId { get; set; }

    // Data
    public string Name { get; set; }

    // Nav
    public Parent Parent { get; set; }
}



public class Context : DbContext
{
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Child> Children { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=./Database/EfCoreRelations.sqlite");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ändra default delete behavior: (Cascade är standard.)
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }
}

public class Worker
{
    private readonly Context _context = new();

    public void CreateAndSeed()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var parents = new List<Parent>();
        parents.Add(new Parent() { Name = "Parent A" });
        parents.Add(new Parent() { Name = "Parent B" });
        _context.Parents.AddRange(parents);
        _context.SaveChanges();

        var children = new List<Child>();
        children.Add(new Child() { Name = "Child A", ParentId = parents[0].Id });
        children.Add(new Child() { Name = "Child B", ParentId = parents[1].Id });
        //children.Add(new Child() { Name = "Child C" }); // Går bara om FK är nullable.
        _context.Children.AddRange(children);

        _context.SaveChanges();
    }

    public void DeleteParentA()
    {
        var parent = _context.Parents.First(p => p.Name.Contains("A"));
        _context.Parents.Remove(parent);
        _context.SaveChanges();
    }
}
