using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public required string Title { get; set; }

        [Required]
        [StringLength(64)]
        public required string Slug { get; set; }

        [Required]
        [StringLength(512)]
        public required string Description { get; set; }

        [Required]
        [StringLength(64)]
        public required string Author { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}