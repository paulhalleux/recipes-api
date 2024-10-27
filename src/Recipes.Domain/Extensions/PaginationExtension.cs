using Recipes.Application.Common;

namespace Recipes.Application.Extensions;

public static class PaginationExtension
{
    public static Paginated<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return Paginated<T>.Create(query, page, pageSize);
    }
    
    public static Paginated<TMapping> Paginate<T, TMapping>(this IQueryable<T> query, int page, int pageSize, Func<T, TMapping> mapping)
    {
        return Paginated<TMapping>.CreateWithMapping(query, page, pageSize, mapping);
    }
}