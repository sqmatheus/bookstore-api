using BookStore.Models;

namespace BookStore.DTOs
{
    public class GenreResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public DateTime CreatedAt { get; set; }

        public static GenreResponseDTO FromModel(Genre genre)
        {
            return new GenreResponseDTO
            {
                Id = genre.Id,
                Name = genre.Name,
                Slug = genre.Slug,
                CreatedAt = genre.CreatedAt
            };
        }
    }
}