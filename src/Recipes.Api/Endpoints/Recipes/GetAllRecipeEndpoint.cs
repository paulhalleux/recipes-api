using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Common;
using Recipes.Application.Extensions;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Common;
using Recipes.Models.Recipe;

namespace Recipes.Endpoints.Recipes;

public class GetAllRecipesEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/recipes", Handler)
            .WithName("GetAllRecipes")
            .WithSummary("Get all recipes paginated")
            .WithTags("Recipes");
    }

    private static Ok<Paginated<RecipeSummary>> Handler([AsParameters] PaginationRequest request, IRecipesRepository recipesRepository, CancellationToken cancellationToken)
    {
        var recipes = recipesRepository
            .GetAllNoTracking()
            .Paginate(request.Page ?? 1, request.PageSize ?? 10, recipe => recipe.Adapt<RecipeSummary>());
        
        return TypedResults.Ok(recipes);
    }
}