using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.Interfaces
{
    public interface IBookRepository
    {
        public Task<bool> ExistsBook(int id);
        public Task<List<Book>> GetBooks(Pagination pagination, string? query);
        public Task<Book?> GetBookById(int id);
        public Task<Book?> GetBookBySlug(string slug);
        public Task<Book> CreateBook(Book book);
        public Task<bool> DeleteBook(Book book);
        public Task<bool> Save();
    }
}