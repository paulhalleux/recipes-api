using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Common;
using Recipes.Application.Extensions;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Common;
using Recipes.Models.Ingredient;

namespace Recipes.Endpoints.Ingredients;

public class GetAllIngredientsEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("/ingredients", Handler)
            .WithName("GetAllIngredients")
            .WithSummary("Get all ingredients paginated")
            .WithTags("Ingredients");
    }

    private static Ok<Paginated<IngredientResponse>> Handler([AsParameters] PaginationRequest request,
        IIngredientsRepository ingredientsRepository, CancellationToken cancellationToken)
    {
        var recipes = ingredientsRepository
            .GetAllNoTracking()
            .Paginate(request.Page ?? 1, request.PageSize ?? 10,
                recipe => recipe.Adapt<IngredientResponse>());
        
        return TypedResults.Ok(recipes);
    }
}