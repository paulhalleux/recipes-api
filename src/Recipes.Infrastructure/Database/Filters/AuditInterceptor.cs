using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Recipes.Application.Interfaces.Entities;

namespace Recipes.Infrastructure.Database.Filters;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChanges(eventData, result);
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            SetAuditProperties(entry);
        }
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);
        foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified))
        {
            SetAuditProperties(entry);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
    private static void SetAuditProperties(EntityEntry entry)
    {
        if (entry.Entity is not IAuditableEntity auditable) return;
        if (entry.State == EntityState.Added)
            auditable.Created = DateTime.UtcNow;
        else if (entry.State == EntityState.Modified) auditable.LastModified = DateTime.UtcNow;
    }
}