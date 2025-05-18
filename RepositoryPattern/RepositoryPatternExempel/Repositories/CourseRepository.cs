using Microsoft.EntityFrameworkCore;
using RepositoryPatternExempel.Data;
using RepositoryPatternExempel.Models;
using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public SchoolContext SchoolContext { get => (SchoolContext)Context; }

    public CourseRepository(SchoolContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Course>> GetCoursesByNameAsync(string name)
    {
        return await SchoolContext.Courses
            .Where(c => c.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }
}
