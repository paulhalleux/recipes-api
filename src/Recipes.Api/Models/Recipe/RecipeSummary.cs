namespace Recipes.Models.Recipe;

public record RecipeSummary(Guid Id, string Name, string Description, Guid ImageId, bool Deleted, DateTimeOffset Created, DateTimeOffset LastModified);