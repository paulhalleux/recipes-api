using Microsoft.EntityFrameworkCore;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Infrastructure.Database.Common;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> DbSet;

    protected BaseRepository(DbContext context)
    {
        _dbContext = context;
        DbSet = _dbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public virtual IQueryable<TEntity> GetAllNoTracking()
    {
        return DbSet.AsNoTracking();
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await DbSet.AnyAsync(entity => entity.Id == id);
    }

    public void Remove(TEntity entity, bool hardDelete = false)
    {
        if (hardDelete)
        {
            DbSet.Remove(entity);
        }
        else
        {
            entity.Delete();
            DbSet.Update(entity);
        }
    }

    public TEntity? RestoreById(Guid id)
    {
        var entity = DbSet.Find(id);
        if (entity is null) return null;
        entity.Undelete();
        DbSet.Update(entity);
        return entity;
    }

    public void RemoveRange(IEnumerable<TEntity> entities, bool hardDelete = false)
    {
        if (hardDelete)
        {
            DbSet.RemoveRange(entities);
            return;
        }

        foreach (var entity in entities) entity.Delete();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _dbContext.Dispose();
    }
}