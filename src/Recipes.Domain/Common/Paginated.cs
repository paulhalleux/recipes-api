namespace Recipes.Application.Common;

public class Paginated<T>
    (IEnumerable<T> items, int count, int pageIndex, int pageSize)
{
    public IEnumerable<T> Items { get; } = items;

    public int TotalCount { get; } = count;

    public int PageIndex { get; } = pageIndex;

    public int PageSize { get; } = pageSize;

    public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;
    
    public static Paginated<TMapping> CreateWithMapping<TS, TMapping>(IEnumerable<TS> source, int pageIndex, int pageSize, Func<TS, TMapping> mapping)
    {
        var enumerable = source as TS[] ?? source.ToArray();
        var count = enumerable.Length;
        var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(mapping).ToList();
        return new Paginated<TMapping>(items, count, pageIndex, pageSize);
    }
    
    public static Paginated<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var enumerable = source as T[] ?? source.ToArray();
        var count = enumerable.Length;
        var items = enumerable.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new Paginated<T>(items, count, pageIndex, pageSize);
    }
}