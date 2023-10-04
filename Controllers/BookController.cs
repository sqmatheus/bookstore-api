using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Helpers;
using BookStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService) => _bookService = bookService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetBooks([FromQuery] Pagination pagination, [FromQuery] string? query)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _bookService.GetBooks(pagination, query));
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookResponseDTO>> GetBookById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _bookService.GetBookById(id));
            }
            catch (BookNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookResponseDTO>> GetBookBySlug(string slug)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _bookService.GetBookBySlug(slug));
            }
            catch (BookNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookResponseDTO>> CreateBook([FromBody] BookRequestDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return StatusCode(StatusCodes.Status201Created, await _bookService.CreateBook(payload));
            }
            catch (GenreNotFoundException)
            {
                return NotFound(new
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Genre not found"
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(Database.IsUniqueConstraintViolation(ex) ?
                    StatusCodes.Status409Conflict :
                    StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _bookService.DeleteBook(id);
                return NoContent();
            }
            catch (BookNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookResponseDTO>> UpdateBook(int id, [FromBody] BookRequestDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _bookService.UpdateBook(id, payload));
            }
            catch (BookNotFoundException)
            {
                return NotFound();
            }
            catch (GenreNotFoundException)
            {
                return NotFound(new
                {
                    Status = StatusCodes.Status404NotFound,
                    Message = "Genre not found"
                });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(Database.IsUniqueConstraintViolation(ex) ?
                    StatusCodes.Status409Conflict :
                    StatusCodes.Status500InternalServerError);
            }
        }

    }


}