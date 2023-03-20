using System.Linq.Expressions;

namespace RepositoryPatternExempel.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // Hämta
    Task<TEntity?> GetAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate); // Parametern gör att man kan skicka in ett vanligt predicate.
    Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "");

    // Lägga till
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Ta bort
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
