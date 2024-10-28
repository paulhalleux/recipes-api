using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Recipes;

public class DeleteRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("/recipes/{id:guid}", Handler)
            .WithName("DeleteRecipe")
            .WithSummary("Delete a recipe by id")
            .WithTags("Recipes");
    }
    
    private static async Task<Results<NotFound, NoContent>> Handler(Guid id, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetByIdAsync(id);
        if (recipe is null)
        {
            return TypedResults.NotFound();
        }

        recipeRepository.Remove(recipe);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}