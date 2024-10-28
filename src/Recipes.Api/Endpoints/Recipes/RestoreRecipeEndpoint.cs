using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Recipes;

public class RestoreRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPatch("/recipes/{id:guid}/restore", Handler)
            .WithSummary("Restore a recipe by id")
            .WithTags("Recipes");
    }
    
    private static async Task<Results<NotFound, Ok<Recipe>>> Handler(Guid id, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var restored = recipeRepository.RestoreById(id);
        if (restored is null)
        {
            return TypedResults.NotFound();
        }
        
        await recipeRepository.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok(restored);
    }
}