namespace Recipes.Models.Resource;

public record ResourceSummary(Guid Id, string Name, string Path, string ContentType, bool Deleted, DateTimeOffset Created, DateTimeOffset LastModified);