using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Ingredients;

public class RestoreIngredientEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPatch("/ingredients/{id:guid}/restore", Handler)
            .WithSummary("Restore an ingredient by id")
            .WithTags("Ingredients");
    }
    
    private static async Task<Results<NotFound, Ok<Ingredient>>> Handler(Guid id, IIngredientsRepository ingredientsRepository, CancellationToken cancellationToken)
    {
        var restored = ingredientsRepository.RestoreById(id);
        if (restored is null)
        {
            return TypedResults.NotFound();
        }
        
        await ingredientsRepository.SaveChangesAsync(cancellationToken);
        return TypedResults.Ok(restored);
    }
}