using Recipes.Application.Interfaces.Repositories;

namespace Recipes.Endpoints.Recipes;

public class GetRecipeByIdEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("recipes/{id}", Handler)
            .WithSummary("Get a recipe by id")
            .WithTags("Recipes");
    }

    private static async Task<IResult> Handler([AsParameters] GetRecipeByIdRequest request, IRecipesRepository recipesRepository, CancellationToken cancellationToken)
    {
        var recipe = await recipesRepository.GetByIdAsync(request.Id);
        if (recipe == null) return TypedResults.NotFound();
        return TypedResults.Ok(recipe);
    }

    internal record GetRecipeByIdRequest(Guid Id); 
}