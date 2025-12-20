namespace LibraryApi.DTO.Pagination
{
    public record class PagedResult<T>
    (
        int TotalItems,
        int PageNumber,
        int PageSize,
        IEnumerable<T> Items
    );
}
