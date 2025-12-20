namespace LibraryApi.DTO
{
    public record class BorrowDto
    (
        int Id,
        int BookId,
        string BookTitle,
        string BorrowerName,
        DateTime BorrowedAt,
        DateTime? ReturnedAt
        );
}
