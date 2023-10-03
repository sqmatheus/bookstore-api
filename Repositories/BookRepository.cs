using BookStore.Data;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context) => _context = context;

        public Task<bool> ExistsBook(int id) => _context.Books.AnyAsync(b => b.Id == id);

        public Task<List<Book>> GetBooks(Pagination pagination)
        {
            var books = _context.Books
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .AsQueryable();

            if (pagination.Offset is not null)
                books = books.Skip(pagination.Offset.Value);

            if (pagination.Limit is not null)
                books = books.Take(pagination.Limit.Value);

            return books.ToListAsync();
        }

        public Task<Book?> GetBookById(int id) => _context.Books
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        public Task<Book?> GetBookBySlug(string slug) => _context.Books
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Where(b => b.Slug == slug)
            .FirstOrDefaultAsync();

        public async Task<Book> CreateBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public Task<bool> DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            return Save();
        }

        public async Task<bool> Save()
        {
            var rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}