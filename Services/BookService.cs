using BookStore.DTOs;
using BookStore.Exceptions;
using BookStore.Helpers;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookResponseDTO> CreateBook(BookRequestDTO request)
        {
            var model = request.ToModel();
            foreach (int genreId in request.GenreIds)
            {
                model.BookGenres.Add(new BookGenre()
                {
                    Genre = await _genreRepository.GetGenreById(genreId) ?? throw new GenreNotFoundException()
                });
            }

            var book = await _bookRepository.CreateBook(model);
            return BookResponseDTO.FromModel(book);
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new BookNotFoundException();
            return await _bookRepository.DeleteBook(book);
        }

        public async Task<BookResponseDTO> GetBookById(int id)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new BookNotFoundException();
            return BookResponseDTO.FromModel(book);
        }

        public async Task<BookResponseDTO> GetBookBySlug(string slug)
        {
            var book = await _bookRepository.GetBookBySlug(slug) ?? throw new BookNotFoundException();
            return BookResponseDTO.FromModel(book);
        }

        public async Task<IEnumerable<BookResponseDTO>> GetBooks(Pagination pagination)
        {
            var books = await _bookRepository.GetBooks(pagination);
            return books.Select(BookResponseDTO.FromModel);
        }

        public async Task<BookResponseDTO> UpdateBook(int id, BookRequestDTO request)
        {
            var book = await _bookRepository.GetBookById(id) ?? throw new BookNotFoundException();

            book.Title = request.Title;
            book.Slug = Slug.Slugify(book.Title);
            book.Description = request.Description;
            book.Author = request.Author;
            book.PublicationDate = request.PublicationDate;
            book.UpdatedAt = DateTime.Now;

            book.BookGenres.Clear();
            foreach (int genreId in request.GenreIds)
            {
                book.BookGenres.Add(new BookGenre()
                {
                    Genre = await _genreRepository.GetGenreById(genreId) ?? throw new GenreNotFoundException()
                });
            }

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
                    throw new BookNotFoundException();
                }
            }
        }
    }
}