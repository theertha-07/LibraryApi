namespace LibraryApi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation property
        public Category? Category { get; set; }

        // Book availability status
        public bool IsAvailable { get; set; } = true;
    }
}
