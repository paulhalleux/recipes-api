using Recipes.Models.Ingredient;

namespace Recipes.Models.RecipeIngredient;

public record RecipeIngredientSummary(Guid Id, IngredientResponse Ingredient, string Quantity);