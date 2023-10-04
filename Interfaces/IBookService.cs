using BookStore.DTOs;
using BookStore.Helpers;

namespace BookStore.Interfaces
{
    public interface IBookService
    {
        public Task<IEnumerable<BookResponseDTO>> GetBooks(Pagination pagination, string? query);
        public Task<BookResponseDTO> GetBookById(int id);
        public Task<BookResponseDTO> GetBookBySlug(string slug);
        public Task<BookResponseDTO> CreateBook(BookRequestDTO request);
        public Task<bool> DeleteBook(int id);
        public Task<BookResponseDTO> UpdateBook(int id, BookRequestDTO request);
    }
}