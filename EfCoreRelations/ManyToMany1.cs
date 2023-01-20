using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations;

public class ManyToMany1
{
    public class Parent
    {
        public int Id { get; set; }

        public ICollection<Child> Children { get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public ICollection<Parent> Parents { get; set; }
    }



    public class Context : DbContext
    {
        DbSet<Parent> Parents { get; set; }
        DbSet<Child> Children { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Options...
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}