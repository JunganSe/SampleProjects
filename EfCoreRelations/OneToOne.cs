using Microsoft.EntityFrameworkCore;

namespace EfCoreRelations;

public class OneToOne
{
    public class Parent
    {
        public int Id { get; set; }

        public Child Child { get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public Parent Parent { get; set; }
    }



    public class Context : DbContext
    {
        DbSet<Parent> Parents { get; set;}
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
