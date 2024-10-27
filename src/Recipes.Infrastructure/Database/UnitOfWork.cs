using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recipes.Application.Interfaces;

namespace Recipes.Infrastructure.Database;

public sealed class UnitOfWork<TContext>(TContext context, ILogger<UnitOfWork<TContext>> logger) : IUnitOfWork
    where TContext : DbContext
{
    public async Task<bool> CommitAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            logger.LogError(dbUpdateException, "An error occured during commiting changes");
            return false;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing) context.Dispose();
    }
}