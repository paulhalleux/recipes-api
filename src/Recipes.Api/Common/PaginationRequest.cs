using Microsoft.AspNetCore.Mvc;

namespace Recipes.Common;

public record PaginationRequest
{
    [FromQuery(Name = "page")]
    public int? Page { get; init; } = 1;
    
    [FromQuery(Name = "size")]
    public int? PageSize { get; init; } = 10;
}