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
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetBooks([FromQuery] Pagination pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _bookService.GetBooks(pagination));
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
            catch (NotFoundException)
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
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
            catch (NotFoundException)
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
            catch (NotFoundException)
            {
                return NotFound();
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