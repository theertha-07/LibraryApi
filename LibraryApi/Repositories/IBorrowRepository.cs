using LibraryApi.Entities;

namespace LibraryApi.Repositories
{
    public interface IBorrowRepository
    {
        Task<IEnumerable<Borrow>> GetAllAsync();
        Task<Borrow?> GetByIdAsync(int id);
        Task AddAsync(Borrow borrow);
        Task UpdateAsync(Borrow borrow);
    }
}
