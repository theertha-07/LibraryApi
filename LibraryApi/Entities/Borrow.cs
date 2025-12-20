namespace LibraryApi.Entities
{
    public class Borrow
    {
        public int Id { get; set; }

        // Foreign Key to Book
        public int BookId { get; set; }
        public Book? Book { get; set; }

        // Foreign Key to Member
        public string BorrowerName { get; set; } = string.Empty;


        public DateTime BorrowedAt { get; set; } = DateTime.Now;
        public DateTime ReturnedAt { get; set; }
    }
}
