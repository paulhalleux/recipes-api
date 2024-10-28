using Recipes.Models.RecipeIngredient;

namespace Recipes.Models.Recipe;

public record RecipeResponse(
    Guid Id,
    string Name,
    string Description,
    string ImageId,
    bool Deleted,
    DateTimeOffset Created,
    DateTimeOffset LastModified,
    List<RecipeIngredientSummary> Ingredients);