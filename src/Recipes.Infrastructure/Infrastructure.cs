using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Interfaces;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Infrastructure.Database;
using Recipes.Infrastructure.Database.Repositories;

namespace Recipes.Infrastructure;

public static class Infrastructure
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        
        services.AddScoped<IUnitOfWork, UnitOfWork<RecipesDbContext>>();
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecipesDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRecipesRepository, RecipesRepository>();
        services.AddScoped<IIngredientsRepository, IngredientsRepository>();
        services.AddScoped<IResourcesRepository, ResourcesRepository>();
    }
}