using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Entities;

namespace Recipes.Application.Entities;

public class Resource(Guid id) : Entity(id), IAuditableEntity
{
    public Resource(Guid id, string name, string path, ResourceType type, string contentType) : this(id)
    {
        Name = name;
        Path = path;
        Type = type;
        ContentType = contentType;
    }

    public string Name { get; private set; } = string.Empty;
    public string Path { get; private set; } = string.Empty;
    
    public ResourceType Type { get; private set; }
    public string ContentType { get; private set; } = string.Empty;
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}