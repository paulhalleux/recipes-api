using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Recipes;

public class CreateRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/recipes", Handle)
            .WithSummary("Create a new recipe")
            .WithTags("Recipes");
    }

    private static async Task<Created<Recipe>> Handle(CreateRecipeRequest request, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var recipe = new Recipe(
            Guid.NewGuid(),
            request.Name,
            request.Description
        );

        recipeRepository.Add(recipe);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Created("/recipes", recipe);
    }

    public record CreateRecipeRequest(string Name, string Description);
}