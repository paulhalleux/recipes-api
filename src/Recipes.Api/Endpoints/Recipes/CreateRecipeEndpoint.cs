using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Recipe;

namespace Recipes.Endpoints.Recipes;

public class CreateRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/recipes", Handler)
            .WithSummary("Create a new recipe")
            .WithTags("Recipes");
    }

    private static async Task<Created<RecipeResponse>> Handler(CreateRecipeRequest request, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var recipe = new Recipe(
            Guid.NewGuid(),
            request.Name,
            request.Description
        );

        recipeRepository.Add(recipe);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Created("/recipes", recipe.Adapt<RecipeResponse>());
    }
}