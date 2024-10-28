using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Ingredient;

namespace Recipes.Endpoints.Ingredients;

public class GetIngredientByIdEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/ingredients/{id}", Handler)
            .WithName("GetIngredientById")
            .WithSummary("Get an ingredient by id")
            .WithTags("Ingredients");
    }

    private static async Task<Results<NotFound, Ok<IngredientResponse>>> Handler(Guid id, IIngredientsRepository recipesRepository, CancellationToken cancellationToken)
    {
        var recipe = await recipesRepository.GetByIdAsync(id);
        if (recipe == null) return TypedResults.NotFound();
        return TypedResults.Ok(recipe.Adapt<IngredientResponse>());
    }
}