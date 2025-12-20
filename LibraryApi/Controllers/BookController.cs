using AutoMapper;
using LibraryApi.DTO;
using LibraryApi.DTO.Pagination;
using LibraryApi.Entities;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repo;
        private readonly IMapper _mapper;

        public BookController(IBookRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookDto>>(books));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(_mapper.Map<BookDto>(book));
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _repo.AddAsync(book);
            var bookDto = _mapper.Map<BookDto>(book);
            return CreatedAtAction(nameof(GetBook), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto dto)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();

            _mapper.Map(dto, book);
            await _repo.UpdateAsync(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book == null) return NotFound();

            await _repo.DeleteAsync(book);
            return NoContent();
        }


        //Use pagination
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<BookDto>>> GetPagedBooks(
            int pageNumber = 1,
            int pageSize = 5) // default values
        {
            // Get paged result from repository
            var pagedBooks = await _repo.GetPagedAsync(pageNumber, pageSize);

            // Map Book -> BookDto
            var dtoItems = _mapper.Map<IEnumerable<BookDto>>(pagedBooks.Items);

            // Return paged result with DTOs
            return Ok(new PagedResult<BookDto>(
                pagedBooks.TotalItems,
                pagedBooks.PageNumber,
                pagedBooks.PageSize,
                dtoItems
            ));
        }
    }
}
