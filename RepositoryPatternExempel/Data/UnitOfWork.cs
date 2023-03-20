using RepositoryPatternExempel.Repositories;
using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolContext _context;

    public ICourseRepository Courses { get; init; }
    public IStudentRepository Students { get; init; }

    public UnitOfWork(SchoolContext context)
    {
        _context = context;
        Courses = new CourseRepository(context);
        Students = new StudentRepository(context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose() // Viktigt att disposa context när unit of work disposas.
    {
        _context.Dispose();
    }
}
