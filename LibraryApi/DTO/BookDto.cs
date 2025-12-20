namespace LibraryApi.DTO
{
    public record class BookDto
    (
        int Id,
        string Title,
        string Author,
        string? CategoryName,
        bool IsAvailable

        );
}
