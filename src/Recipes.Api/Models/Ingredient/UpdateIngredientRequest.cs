using Recipes.Application.Enums;

namespace Recipes.Models.Ingredient;

public record UpdateIngredientRequest(string Name, string Description, IngredientUnit Unit);