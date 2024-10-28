namespace Recipes.Models.Recipe;

public record RecipeSummary(Guid Id, string Name, string Description, bool Deleted, DateTimeOffset Created, DateTimeOffset LastModified);