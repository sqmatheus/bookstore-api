using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Helpers;
using BookStore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository) => _bookRepository = bookRepository;

        public async Task<BookResponseDTO> CreateBook(BookRequestDTO request)
        {
            var book = await _bookRepository.CreateBook(request.ToModel());
            return BookResponseDTO.FromModel(book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new NotFoundException();
            return await _bookRepository.DeleteBook(book);
        }

        public async Task<BookResponseDTO> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new NotFoundException();
            return BookResponseDTO.FromModel(book);
        }

        public async Task<BookResponseDTO> GetBookBySlug(string slug)
        {
            var book = await _bookRepository.GetBookBySlug(slug) ?? throw new NotFoundException();
            return BookResponseDTO.FromModel(book);
        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooks(Pagination pagination)
        {
            var books = await _bookRepository.GetBooks(pagination);
            return books.Select(BookResponseDTO.FromModel);
        }

        public async Task<BookResponseDTO> UpdateBook(int id, BookRequestDTO request)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new NotFoundException();

            book.Title = request.Title;
            book.Slug = Slug.Slugify(book.Title);
            book.Description = request.Description;
            book.Author = request.Author;
            book.PublicationDate = request.PublicationDate;
            book.UpdatedAt = DateTime.Now;

            try
            {
                await _bookRepository.Save();
                return BookResponseDTO.FromModel(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _bookRepository.ExistsBook(id))
                {
                    throw;
                }
                else
                {
                    throw new NotFoundException();
                }
            }
        }
    }
}