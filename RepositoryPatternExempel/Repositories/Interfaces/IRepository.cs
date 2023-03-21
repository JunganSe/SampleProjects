using System.Linq.Expressions;

namespace RepositoryPatternExempel.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{

    // Hämta
    Task<TEntity?> GetOnlyAsync(int id);
    Task<IEnumerable<TEntity>> GetAllOnlyAsync();
    Task<TEntity?> SingleOrDefaultOnlyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetEntitiesAsync(string? include = null, Expression<Func<TEntity, bool>>? predicate = null);

    // Lägga till
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    // Ta bort
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
