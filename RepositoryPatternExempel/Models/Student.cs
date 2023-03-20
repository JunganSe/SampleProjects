namespace RepositoryPatternExempel.Models;

public class Student
{
    public int Id { get; set; }
    public int? FavoriteCourseId { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public Course FavoriteCourse { get; set; } = null!;
}
