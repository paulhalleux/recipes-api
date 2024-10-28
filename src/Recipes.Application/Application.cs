using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Interfaces.Services;
using Recipes.Application.Services;

namespace Recipes.Application;

public static class Application
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
        return services;
    }
}