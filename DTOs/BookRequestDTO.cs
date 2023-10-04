using System.ComponentModel.DataAnnotations;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.DTOs
{
    public class BookRequestDTO
    {
        [Required(ErrorMessage = "The title field is mandatory")]
        [MinLength(8, ErrorMessage = "The title must be at least 8 characters long")]
        [MaxLength(64, ErrorMessage = "The title must have a maximum of 64 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "The description field is mandatory")]
        [MinLength(8, ErrorMessage = "The description must be at least 8 characters long")]
        [MaxLength(512, ErrorMessage = "The description must have a maximum of 512 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "The author field is mandatory")]
        [MinLength(8, ErrorMessage = "The author must be at least 8 characters long")]
        [MaxLength(64, ErrorMessage = "The author must have a maximum of 64 characters")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "The publication date field is mandatory")]
        public DateTime PublicationDate { get; set; }

        public IEnumerable<int> GenreIds { get; set; } = new List<int>();

        public Book ToModel()
        {
            return new Book()
            {
                Title = Title,
                Slug = Slug.Slugify(Title),
                Description = Description,
                Author = Author,
                PublicationDate = PublicationDate
            };
        }
    }

}