using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Recipes;

public class RestoreRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/recipes/{id}/restore", Handle)
            .WithSummary("Restore a recipe by id")
            .WithTags("Recipes");
    }
    
    private static async Task<Results<NotFound, Ok<Recipe>>> Handle([AsParameters] RestoreRecipeByIdRequest request, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var restored = recipeRepository.RestoreById(request.Id);
        if (restored is null)
        {
            return TypedResults.NotFound();
        }
        
        await recipeRepository.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok(restored);
    }
    
    internal record RestoreRecipeByIdRequest(Guid Id); 
}