using Recipes.Application.Interfaces.Entities;

namespace Recipes.Application.Entities;

public class Recipe(Guid id) : Entity(id), IAuditableEntity
{
    public Recipe(Guid id, string name, string description) : this(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}