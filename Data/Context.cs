using Microsoft.EntityFrameworkCore;
using EfCoreExempel.Models;

namespace EfCoreExempel.Data
{
    public class Context : DbContext
    {
        // En DbSet-property skapas för varje tabell som ska kunna nås.
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; } // Kopplingstabellen kan utelämnas här, om man inte behöver nå den specifikt.
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Card> Cards { get; set; }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"
                    Server = (localdb)\mssqllocaldb; 
                    Database = EfCoreExempel; 
                    Trusted_Connection = true;");
            }
        }

        // Denna metod används för att definiera modellen, dvs hur vissa kopplingar, nycklar, och constraints ser ut.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>()              // Entiteten (klassen/tabellen) CourseStudent...
                .HasKey(                                      // ...har en primärnyckel...
                    sc => new { sc.CourseId, sc.StudentId }); // ...som består av CourseId och StudentId.

            // TODO: Beskriv när dessa två delar används och när de är överflödiga.
            modelBuilder.Entity<CourseStudent>() // Entiteten (klassen/tabellen) CourseStudent...
                .HasOne(sc => sc.Course)         // ...kan ha EN Course... (Navigation property som argument)
                .WithMany(s => s.CourseStudents) // ...som i sin tur kan ha MÅNGA CourseStudent. (Navigation property som argument)
                .HasForeignKey(cs => cs.CourseId); // Specifierar FK från CourseStudent till Course.

            modelBuilder.Entity<CourseStudent>() // Entiteten (klassen/tabellen) CourseStudent...
                .HasOne(sc => sc.Student)        // ...kan ha EN Student... (Navigation property som argument)
                .WithMany(s => s.CourseStudents) // ...som i sin tur kan ha MÅNGA CourseStudent. (Navigation property som argument)
                .HasForeignKey(cs => cs.StudentId); // Specifierar FK från CourseStudent till Student.



            modelBuilder.Entity<Card>()     // Entiteten (klassen/tabellen) Card...
                .HasIndex(c => c.StudentId) // ...vars property StudentId får ett index och...
                .IsUnique();                // ...värdet måste vara unikt.



            modelBuilder.Entity<Student>()  // Entiteten (klassen/tabellen) Student...
                .Property(s => s.FirstName) // ...vars property FirstName...
                .IsRequired()               // ...får inte vara null...
                .HasMaxLength(20);          // ...och får ha max 20 tecken.

            modelBuilder.Entity<Student>() // Entiteten (klassen/tabellen) Student...
                .Property(s => s.LastName) // ...vars property LastName...
                .IsRequired()              // ...får inte vara null...
                .HasMaxLength(20);         // ...och får ha max 20 tecken.
        }
    }
}
