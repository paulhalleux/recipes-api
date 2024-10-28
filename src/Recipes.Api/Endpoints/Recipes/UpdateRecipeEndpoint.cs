using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Models.Recipe;

namespace Recipes.Endpoints.Recipes;

public class UpdateRecipeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPut("/recipes/{id:guid}", Handler)
            .WithName("UpdateRecipe")
            .WithSummary("Update a recipe")
            .WithTags("Recipes");
    }

    private static async Task<Results<NotFound, Ok<RecipeResponse>>> Handler(Guid id, UpdateRecipeRequest request, IRecipesRepository recipeRepository, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetByIdAsync(id);
        if (recipe is null)
        {
            return TypedResults.NotFound();
        }
        
        recipe.Update(request.Name, request.Description);
        
        recipeRepository.Update(recipe);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(recipe.Adapt<RecipeResponse>());
    }
}