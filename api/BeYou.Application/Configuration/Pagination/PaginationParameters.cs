namespace BeYou.Application.Configuration.Pagination;

// PaginationParameters handles pagination settings.
// - PageNumber: current page (default 1).
// - PageSize: limits page size (default 10, max 50).
// - Paginated: flag to enable/disable pagination.
// Ensures page size doesn't exceed MAXPAGESIZE.

public class PaginationParameters
{
    const int MAXPAGESIZE = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAXPAGESIZE ? value : MAXPAGESIZE;
    }
    public bool Paginated { get; set; } = false;
}