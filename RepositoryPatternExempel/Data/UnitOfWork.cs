using RepositoryPatternExempel.Repositories;
using RepositoryPatternExempel.Repositories.Interfaces;

namespace RepositoryPatternExempel.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolContext _schoolContext;
    private ICourseRepository? _courseRepository; // Backing field för propertyn.
    private IStudentRepository? _studentRepository;

    // Repon instansieras först när de ska nås, så bara de som faktiskt behövs blir instansierade.
    public ICourseRepository Courses => _courseRepository ??= new CourseRepository(_schoolContext); // Om _courseRepository är null så instansieras det först innan det returneras. "Lazy initialization pattern"
    public IStudentRepository Students => _studentRepository ??= new StudentRepository(_schoolContext);

    public UnitOfWork()
    {
        // Flera context kan instansieras och skickas till respektive repo som behöver dem.
        // Det är då en bra idé att instansiera dem först vid anrop, på samma sätt som repositories gör.
        _schoolContext = new SchoolContext();
    }

    public async Task<bool> TrySaveAsync()
    {
        return (await _schoolContext.SaveChangesAsync() > 0);
    }

    public void Dispose() // Viktigt att disposa context när unit of work disposas.
    {
        _schoolContext.Dispose();
        GC.SuppressFinalize(this); // Ska tydligen vara med för att dispose-delen ska fungera korrekt.
    }
}
