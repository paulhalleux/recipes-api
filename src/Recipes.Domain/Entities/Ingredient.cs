using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Entities;

namespace Recipes.Application.Entities;

public class Ingredient(Guid id) : Entity(id), IAuditableEntity
{
    public Ingredient(Guid id, string name, string description, IngredientUnit unit) : this(id)
    {
        Name = name;
        Description = description;
        Unit = unit;
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public IngredientUnit Unit { get; private set; }

    public Guid? ImageId { get; private set; }
    public Resource? Image { get; private set; }

    public void Update(string name, string description, IngredientUnit unit)
    {
        Name = name;
        Description = description;
        Unit = unit;
    }
    
    public void SetImage(Guid resourceId)
    {
        ImageId = resourceId;
    }

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}