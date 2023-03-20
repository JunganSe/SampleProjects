using Microsoft.EntityFrameworkCore;
using RepositoryPatternExempel.Models;

namespace RepositoryPatternExempel.Data;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            options.UseSqlite($"Data Source={projectDirectory}/Data/Database/School.sqlite");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.HasMany(e => e.Courses).WithMany(e => e.Students);
            entity.HasOne(e => e.FavoriteCourse).WithMany(e => e.StudentsWhoLikesThis)
                .HasForeignKey(e => e.FavoriteCourseId).IsRequired(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });
    }
}
