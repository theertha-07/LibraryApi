using AutoMapper;
using LibraryApi.DTO;
using LibraryApi.Entities;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRepository _repo;
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;

        public BorrowController(IBorrowRepository repo, IBookRepository bookRepo, IMapper mapper)
        {
            _repo = repo;
            _bookRepo = bookRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowDto>>> GetBorrows()
        {
            var borrows = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BorrowDto>>(borrows));
        }

        [HttpPost]
        public async Task<ActionResult<BorrowDto>> BorrowBook(CreateBorrowDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(dto.BookId);
            if (book == null) return NotFound("Book not found");
            if (!book.IsAvailable) return BadRequest("Book is not available");

            var borrow = _mapper.Map<Borrow>(dto);
            borrow.BorrowedAt = DateTime.UtcNow;
            book.IsAvailable = false;

            await _repo.AddAsync(borrow);
            await _bookRepo.UpdateAsync(book);

            var borrowDto = _mapper.Map<BorrowDto>(borrow);
            return CreatedAtAction(nameof(GetBorrows), new { id = borrowDto.Id }, borrowDto);
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var borrow = await _repo.GetByIdAsync(id);
            if (borrow == null) return NotFound();

            borrow.ReturnedAt = DateTime.UtcNow;

            var book = await _bookRepo.GetByIdAsync(borrow.BookId);
            if (book != null) book.IsAvailable = true;

            await _repo.UpdateAsync(borrow);
            await _bookRepo.UpdateAsync(book!);

            return NoContent();
        }
    }
}
