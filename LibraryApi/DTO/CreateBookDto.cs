namespace LibraryApi.DTO
{
    public record class CreateBookDto
    (
        string Title,
        string Author,
        int CategoryId
        );
}
