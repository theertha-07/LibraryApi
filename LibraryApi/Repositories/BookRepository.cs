using LibraryApi.Data;
using LibraryApi.DTO.Pagination;
using LibraryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                                 .Include(b => b.Category)
                                 .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                                 .Include(b => b.Category)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }


        //Implement pagination
        public async Task<PagedResult<Book>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Books
                                .Include(b => b.Category) // Include category for DTO mapping
                                .AsQueryable();

            int totalItems = await query.CountAsync(); // Total items before pagination

            var items = await query
                .Skip((pageNumber - 1) * pageSize) // Skip previous pages
                .Take(pageSize)                    // Take only pageSize items
                .ToListAsync();

            return new PagedResult<Book>(
                totalItems,
                pageNumber,
                pageSize,
                items
            );
        }


    }
}
