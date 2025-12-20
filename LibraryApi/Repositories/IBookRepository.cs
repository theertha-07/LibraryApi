using LibraryApi.DTO.Pagination;
using LibraryApi.Entities;

namespace LibraryApi.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);


        //pagination method
        Task<PagedResult<Book>> GetPagedAsync(int pageNumber, int pageSize);

    }
}
