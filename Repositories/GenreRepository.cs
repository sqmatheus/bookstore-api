using BookStore.Data;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context) => _context = context;

        public Task<bool> ExistsGenre(int id) => _context.Genres.AnyAsync(g => g.Id == id);

        public Task<List<Genre>> GetGenres(Pagination pagination)
        {
            var genres = _context.Genres.AsQueryable();

            if (pagination.Offset is not null)
                genres = genres.Skip(pagination.Offset.Value);

            if (pagination.Limit is not null)
                genres = genres.Take(pagination.Limit.Value);

            return genres.ToListAsync();
        }

        public Task<Genre?> GetGenreById(int id) => _context.Genres.Where(g => g.Id == id).FirstOrDefaultAsync();

        public Task<Genre?> GetGenreBySlug(string slug) => _context.Genres.Where(g => g.Slug == slug).FirstOrDefaultAsync();

        public Task<List<Book>> GetBooksByGenreId(int id, Pagination pagination)
        {
            var books = _context.Books
                .Where(b => b.BookGenres.Any(bg => bg.GenreId == id))
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .AsQueryable();

            if (pagination.Offset is not null)
                books = books.Skip(pagination.Offset.Value);

            if (pagination.Limit is not null)
                books = books.Take(pagination.Limit.Value);

            return books.ToListAsync();
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public Task<bool> DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            return Save();
        }

        public async Task<bool> Save()
        {
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}