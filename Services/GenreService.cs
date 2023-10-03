using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Helpers;
using BookStore.Interfaces;

namespace BookStore.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public async Task<GenreResponseDTO> CreateGenre(GenreRequestDTO request)
        {
            var genre = await _genreRepository.CreateGenre(request.ToModel());
            return GenreResponseDTO.FromModel(genre);
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _genreRepository.GetGenreById(id) ?? throw new NotFoundException();
            return await _genreRepository.DeleteGenre(genre);
        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooksByGenreId(int id, Pagination pagination)
        {
            var books = await _genreRepository.GetBooksByGenreId(id, pagination);
            return books.Select(BookResponseDTO.FromModel);
        }

        public async Task<GenreResponseDTO> GetGenreById(int id)
        {
            var genre = await _genreRepository.GetGenreById(id) ?? throw new NotFoundException();
            return GenreResponseDTO.FromModel(genre);
        }

        public async Task<GenreResponseDTO> GetGenreBySlug(string slug)
        {
            var genre = await _genreRepository.GetGenreBySlug(slug) ?? throw new NotFoundException();
            return GenreResponseDTO.FromModel(genre);
        }

        public async Task<IEnumerable<GenreResponseDTO>> GetGenres(Pagination pagination)
        {
            var genres = await _genreRepository.GetGenres(pagination);
            return genres.Select(GenreResponseDTO.FromModel);
        }

    }
}