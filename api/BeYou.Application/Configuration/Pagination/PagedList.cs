using Microsoft.EntityFrameworkCore;

namespace BeYou.Application.Configuration.Pagination;

// PagedList<T> handles paginated data for large datasets.
// - Holds pagination info: CurrentPage, TotalPages, PageSize, TotalCount.
// - PaginatedCollection: fetches a specific page from IQueryable<T>.
// - ToPagedList: creates a paginated list with given items.
// Useful for dividing large data into manageable pages.

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }

    public int TotalPages { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static async Task<ICollection<T>> PaginatedCollection(IQueryable<T> source, int pageNumber, int pageSize)
    {
        return await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public static PagedList<T> ToPagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}