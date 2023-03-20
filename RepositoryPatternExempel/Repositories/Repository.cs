using Microsoft.EntityFrameworkCore;
using RepositoryPatternExempel.Repositories.Interfaces;
using System.Linq.Expressions;

namespace RepositoryPatternExempel.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext Context; // Läggs i en protected field så att de ärvande klasserna kan nå den.
    private readonly DbSet<TEntity> _entities;

    public Repository(DbContext context) // Ta in en DbContext så att det går att använda olika context i olika repon (eftersom de alla ärver DbContext).
    {
        Context = context;
        _entities = context.Set<TEntity>(); // Motsvarar tex "SchoolContext.Students", men skrivs så för att vi är i parent-contexten DbContext.
    }



    // Hämta utan include-delar.
    public async Task<TEntity?> GetAsync(int id) => await _entities.FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.SingleOrDefaultAsync(predicate);
    }

    // Hämta med filter och include.
    // filter: Predicate.
    // includeProperties: Kommaseparerad lista i sträng.
    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        string? includeProperties = null)
    {
        IQueryable<TEntity> query = _entities;
        if (filter != null)
            query = query.Where(filter);
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(','))
                query = query.Include(includeProperty);
        }

        return await query.ToListAsync();
    }



    // Lägga till
    public async Task AddAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _entities.AddRangeAsync(entities);
    }



    // Ta bort
    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _entities.RemoveRange(entities);
    }

}
