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
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService) => _genreService = genreService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GenreResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GenreResponseDTO>>> GetGenres([FromQuery] Pagination pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _genreService.GetGenres(pagination));
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreResponseDTO>> GetGenreById(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _genreService.GetGenreById(id));
            }
            catch (GenreNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenreResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenreResponseDTO>> GetGenreBySlug(string slug)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return Ok(await _genreService.GetGenreBySlug(slug));
            }
            catch (GenreNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("books/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BookResponseDTO>>> GetBooksByGenreId(int id, [FromQuery] Pagination pagination)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _genreService.GetBooksByGenreId(id, pagination));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenreResponseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GenreResponseDTO>> CreateGenre([FromBody] GenreRequestDTO payload)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                return StatusCode(StatusCodes.Status201Created, await _genreService.CreateGenre(payload));
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
        public async Task<ActionResult> DeleteGenre(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _genreService.DeleteGenre(id);
                return NoContent();
            }
            catch (GenreNotFoundException)
            {
                return NotFound();
            }
        }

    }


}