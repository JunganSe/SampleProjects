using RepositoryPatternExempel.Data;
using RepositoryPatternExempel.Models;

namespace RepositoryPatternExempel;

internal class Program
{
    static async Task Main(string[] args)
    {
        await Reset();
        await Seed();
        await Test();
    }

    internal static async Task Reset()
    {
        var context = new SchoolContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }

    private static async Task Seed()
    {
        // using för konsolappar, behövs inte i asp?
        using var unitOfWork = new UnitOfWork(new SchoolContext());

        // Skapa studenter.
        await unitOfWork.Students.AddAsync(new Student() { Name = "Andy" });
        var students = new List<Student>()
            {
                new Student() { Name = "Bella" },
                new Student() { Name = "Charlie" },
                new Student() { Name = "Dani" },
            };
        await unitOfWork.Students.AddRangeAsync(students);

        // Skapa kurser
        var courses = new List<Course>()
            {
                new Course(){ Name = "Alpha"},
                new Course(){ Name = "Beta"},
                new Course(){ Name = "Gamma"}
            };
        await unitOfWork.Courses.AddRangeAsync(courses);

        await unitOfWork.SaveAsync();

        // Tilldela kurser.
        var bella = await unitOfWork.Students.GetAsync(2);
        var alpha = await unitOfWork.Courses.GetAsync(1);
        bella.Courses.Add(alpha);
        bella.Courses.Add(courses[1]); // Beta
        courses.ForEach(c => students[2].Courses.Add(c)); // Lägg till alla kurser på Charlie.

        await unitOfWork.SaveAsync();
    }

    private static async Task Test()
    {
        using var unitOfWork = new UnitOfWork(new SchoolContext());

        var studentById = await unitOfWork.Students.GetAsync(1);
        var allStudents = await unitOfWork.Students.GetAllAsync();
        var studentsByName = await unitOfWork.Students.GetStudentsByNameAsync("an");

        var dani = await unitOfWork.Students.GetEntitiesAsync(s => s.Name == "Dani", "Courses");

        // Radera
        var studentToDelete = await unitOfWork.Students.GetAsync(1); // Hämta student på id.
        unitOfWork.Courses.RemoveRange(studentToDelete.Courses); // Radera alla kurser som studenten har.
        unitOfWork.Students.Remove(studentToDelete); // Radera studenten.
        await unitOfWork.SaveAsync(); // Utför ändringarna.

        // Redigera
        var studentToEdit = await unitOfWork.Students.GetAsync(2); // Hämta student på id.
        studentToEdit.Name = "New name"; // Redigera studenten.
        await unitOfWork.SaveAsync(); // Utför ändringarna.
    }
}
