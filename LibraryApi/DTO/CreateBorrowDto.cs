namespace LibraryApi.DTO
{
    public record class CreateBorrowDto
    (
        int BookId,
        string BorrowerName
        );
}
