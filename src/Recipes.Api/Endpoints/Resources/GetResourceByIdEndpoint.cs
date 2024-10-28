using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Resources;

public class GetResourceByIdEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/resources/{id:guid}", Handler)
            .WithSummary("Retrieves a resource by its id")
            .WithTags("Resources")
            .Accepts<Resource>("application/json");
    }

    private static async Task<Results<NotFound, Ok<Resource>>> Handler(Guid id,
        IResourcesRepository resourcesRepository)
    {
        var resource = await resourcesRepository.GetByIdAsync(id);
        return resource == null ? TypedResults.NotFound() : TypedResults.Ok(resource);
    }
}