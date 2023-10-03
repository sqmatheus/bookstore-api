using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public required string Name { get; set; }

        [Required]
        [StringLength(32)]
        public required string Slug { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<BookGenre> BookGenres { get; set; } = null!;
    }
}