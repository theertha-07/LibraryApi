namespace LibraryApi.DTO
{
    public record class UpdateBookDto
    (
        string Title,
        string Author,
        int CategoryId
        );
}
