using Recipes.Application.Enums;

namespace Recipes.Models.Ingredient;

public record IngredientSummary(
    Guid Id,
    string Name,
    string Description,
    IngredientUnit Unit,
    string ImageId,
    bool Deleted,
    DateTimeOffset Created,
    DateTimeOffset LastModified);