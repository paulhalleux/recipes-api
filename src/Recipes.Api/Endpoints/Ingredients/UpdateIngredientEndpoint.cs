using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Ingredient;

namespace Recipes.Endpoints.Ingredients;

public class UpdateIngredientEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/ingredients/{id:guid}", Handler)
            .WithSummary("Update an ingredient")
            .WithTags("Ingredients");
    }

    private static async Task<Results<NotFound, Ok<Ingredient>>> Handler(Guid id, UpdateIngredientRequest request, IIngredientsRepository ingredientsRepository, CancellationToken cancellationToken)
    {
        var ingredient = await ingredientsRepository.GetByIdAsync(id);
        if (ingredient is null)
        {
            return TypedResults.NotFound();
        }
        
        ingredient.Update(request.Name, request.Description, request.Unit);
        
        ingredientsRepository.Update(ingredient);
        await ingredientsRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(ingredient);
    }
}