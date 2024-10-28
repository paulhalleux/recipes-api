using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Ingredient;

namespace Recipes.Endpoints.Ingredients;

public class CreateIngredientEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("/ingredients", Handler)
            .WithSummary("Create a new ingredient")
            .WithName("CreateIngredient")
            .WithTags("Ingredients");
    }

    private static async Task<Created<IngredientResponse>> Handler(CreateIngredientRequest request, IIngredientsRepository ingredientsRepository, CancellationToken cancellationToken)
    {
        var ingredient = new Ingredient(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.Unit
        );

        ingredientsRepository.Add(ingredient);
        await ingredientsRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Created("/ingredients", ingredient.Adapt<IngredientResponse>());
    }
}