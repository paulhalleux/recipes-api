using Recipes.Application.Enums;

namespace Recipes.Models.Ingredient;

public record CreateIngredientRequest(string Name, string Description, IngredientUnit Unit);