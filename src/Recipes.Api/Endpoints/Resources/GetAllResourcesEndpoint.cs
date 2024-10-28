using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Common;
using Recipes.Application.Extensions;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Common;
using Recipes.Models.Resource;

namespace Recipes.Endpoints.Resources;

public class GetAllResourcesEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/resources", Handler)
            .WithSummary("Get all resources paginated")
            .WithTags("Resources");
    }

    private static Ok<Paginated<ResourceSummary>> Handler([AsParameters] PaginationRequest request, IResourcesRepository resourcesRepository)
    {
        var resources = resourcesRepository
            .GetAllNoTracking()
            .Paginate(request.Page ?? 1, request.PageSize ?? 10, recipe => recipe.Adapt<ResourceSummary>());
        
        return TypedResults.Ok(resources);
    }
}