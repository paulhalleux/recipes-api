using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Application.Interfaces.Services;

namespace Recipes.Endpoints.Ingredients;

public class DeleteIngredientEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapDelete("/ingredients/{id:guid}", Handler)
            .WithName("DeleteIngredient")
            .WithSummary("Delete an ingredient by id")
            .WithTags("Ingredients");
    }

    private static async Task<Results<NotFound, NoContent>> Handler(Guid id,
        IIngredientsRepository ingredientsRepository, 
        CancellationToken cancellationToken)
    {
        var ingredient = await ingredientsRepository.GetByIdAsync(id);
        if (ingredient is null)
        {
            return TypedResults.NotFound();
        }

        ingredientsRepository.Remove(ingredient);
        await ingredientsRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}