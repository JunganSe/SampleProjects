using RepositoryPatternExempel.Models;

namespace RepositoryPatternExempel.Repositories.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetStudentsByNameAsync(string name);
}
