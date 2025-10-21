namespace VisionHive.Domain.Pagination;

public class PageResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

    public int Page { get; set; }

    public int PageSize { get; set; }

    public long Total { get; set; }


    public bool HasNext => Page < TotalPages;

    public bool HasPrevious => Page > 1;
    public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
}