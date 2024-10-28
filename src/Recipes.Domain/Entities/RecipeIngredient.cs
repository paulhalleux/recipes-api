using Recipes.Application.Interfaces.Entities;

namespace Recipes.Application.Entities;

public class RecipeIngredient(Guid id) : Entity(id), IAuditableEntity
{
    public RecipeIngredient(Guid id, int quantity, Guid ingredientId, Guid recipeId) : this(id)
    {
        Quantity = quantity;
        IngredientId = ingredientId;
        RecipeId = recipeId;
    }
    
    public int Quantity { get; set; }
    
    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; } = null!;
    
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}