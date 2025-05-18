using RepositoryPatternExempel.Models;

namespace RepositoryPatternExempel.Repositories.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesByNameAsync(string name);
}
