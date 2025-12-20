using LibraryApi.Data;
using LibraryApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrow>> GetAllAsync()
        {
            return await _context.Borrows
                                 .Include(b => b.Book)
                                 .ToListAsync();
        }

        public async Task<Borrow?> GetByIdAsync(int id)
        {
            return await _context.Borrows
                                 .Include(b => b.Book)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Borrow borrow)
        {
            _context.Borrows.Add(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Borrow borrow)
        {
            _context.Borrows.Update(borrow);
            await _context.SaveChangesAsync();
        }
    }
}
