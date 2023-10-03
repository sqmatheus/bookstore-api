using BookStore.DTOs;
using BookStore.Helpers;

namespace BookStore.Interfaces
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreResponseDTO>> GetGenres(Pagination pagination);
        public Task<GenreResponseDTO> GetGenreById(int id);
        public Task<GenreResponseDTO> GetGenreBySlug(string slug);
        public Task<IEnumerable<BookResponseDTO>> GetBooksByGenreId(int id, Pagination pagination);
        public Task<GenreResponseDTO> CreateGenre(GenreRequestDTO request);
        public Task<bool> DeleteGenre(int id);
    }
}