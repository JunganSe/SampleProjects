using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Data;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository Courses { get; init; }
    IStudentRepository Students { get; init; }

    Task<int> SaveAsync();
}
