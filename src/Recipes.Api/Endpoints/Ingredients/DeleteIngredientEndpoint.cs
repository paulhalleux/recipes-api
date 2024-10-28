using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Ingredients;

public class DeleteIngredientEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("/ingredients/{id:guid}", Handler)
            .WithSummary("Delete an ingredient by id")
            .WithTags("Ingredients");
    }
    
    private static async Task<Results<NotFound, NoContent>> Handler(Guid id, IIngredientsRepository ingredientsRepository, CancellationToken cancellationToken)
    {
        var recipe = await ingredientsRepository.GetByIdAsync(id);
        if (recipe is null)
        {
            return TypedResults.NotFound();
        }

        ingredientsRepository.Remove(recipe);
        await ingredientsRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}