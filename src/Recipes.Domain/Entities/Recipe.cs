﻿using Recipes.Application.Interfaces.Entities;

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
    
    public Guid? ImageId { get; private set; }
    public Resource? Image { get; private set; }
    
    public ICollection<RecipeIngredient> Ingredients { get; } = new List<RecipeIngredient>();
    
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void SetImage(Guid resourceId)
    {
        ImageId = resourceId;
    }
    
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset LastModified { get; set; }
}