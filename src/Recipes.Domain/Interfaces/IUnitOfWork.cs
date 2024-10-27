namespace Recipes.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public Task<bool> CommitAsync();
}