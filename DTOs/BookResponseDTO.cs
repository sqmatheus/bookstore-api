using BookStore.Models;

namespace BookStore.DTOs
{
    public class BookResponseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public required string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<GenreResponseDTO> Genres { get; set; } = null!;

        public static BookResponseDTO FromModel(Book book)
        {
            return new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Slug = book.Slug,
                Description = book.Description,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
                Genres = book.BookGenres.Select(bg => GenreResponseDTO.FromModel(bg.Genre))
            };
        }
    }
}