using Recipes.Application.Entities;

namespace Recipes.Application.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllNoTracking();
    
    Task<TEntity?> GetByIdAsync(Guid id);

    void Update(TEntity entity);

    Task<bool> ExistsAsync(Guid id);

    public void Remove(TEntity entity, bool hardDelete = false);
    
    public TEntity? RestoreById(Guid id);

    void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}