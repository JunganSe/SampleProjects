using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Data;

public interface IUnitOfWork : IDisposable
{
    public ICourseRepository Courses { get; }
    public IStudentRepository Students { get; }

    Task<bool> TrySaveAsync();
}
