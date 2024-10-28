using Recipes.Application.Enums;

namespace Recipes.Models.Ingredient;

public record IngredientResponse(
    Guid Id,
    string Name,
    string Description,
    IngredientUnit Unit,
    Guid ImageId,
    bool Deleted,
    DateTimeOffset Created,
    DateTimeOffset LastModified);