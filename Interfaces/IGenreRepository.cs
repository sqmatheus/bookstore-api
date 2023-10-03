using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IGenreRepository
    {
        public Task<bool> ExistsGenre(int id);
        public Task<List<Genre>> GetGenres(Pagination pagination);
        public Task<Genre?> GetGenreById(int id);
        public Task<Genre?> GetGenreBySlug(string slug);
        public Task<List<Book>> GetBooksByGenreId(int id, Pagination pagination);
        public Task<Genre> CreateGenre(Genre genre);
        public Task<bool> DeleteGenre(Genre genre);
        public Task<bool> Save();
    }
}