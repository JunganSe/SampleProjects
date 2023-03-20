namespace RepositoryPatternExempel.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Student> StudentsWhoLikesThis { get; set; } = new List<Student>();
}
