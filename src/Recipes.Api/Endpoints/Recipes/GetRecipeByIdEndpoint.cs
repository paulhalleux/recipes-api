using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Recipe;

namespace Recipes.Endpoints.Recipes;

public class GetRecipeByIdEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/recipes/{id:guid}", Handler)
            .WithName("GetRecipeById")
            .WithSummary("Get a recipe by id")
            .WithTags("Recipes");
    }

    private static async Task<Results<NotFound, Ok<RecipeResponse>>> Handler(Guid id, IRecipesRepository recipesRepository,
        CancellationToken cancellationToken)
    {
        var recipe = await recipesRepository.GetByIdAsync(id);
        if (recipe == null) return TypedResults.NotFound();
        
        return TypedResults.Ok(recipe.Adapt<RecipeResponse>());
    }
}