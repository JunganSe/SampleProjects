using Microsoft.EntityFrameworkCore;
using RepositoryPatternExempel.Data;
using RepositoryPatternExempel.Models;
using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public SchoolContext SchoolContext { get => (SchoolContext)Context; } // Property som hämtar DbContext från parent-klassen och castar till SchoolContext (som ärver DbContext).

    public StudentRepository(SchoolContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Student>> GetStudentsByNameAsync(string name)
    {
        return await SchoolContext.Students
            .Where(s => s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsByFavoriteCourseAsync(int courseId)
    {
        return await SchoolContext.Students
            .Where(s => s.FavoriteCourseId == courseId)
            .ToListAsync();
    }
}
