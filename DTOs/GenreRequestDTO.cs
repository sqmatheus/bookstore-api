using System.ComponentModel.DataAnnotations;
using BookStore.Helpers;
using BookStore.Models;

namespace BookStore.DTOs
{
    public class GenreRequestDTO
    {
        [Required(ErrorMessage = "The name field is mandatory")]
        [MinLength(8, ErrorMessage = "The name must be at least 8 characters long")]
        [MaxLength(32, ErrorMessage = "The name must have a maximum of 64 characters")]
        public required string Name { get; set; }

        public Genre ToModel()
        {
            return new Genre()
            {
                Name = Name,
                Slug = Slug.Slugify(Name)
            };
        }
    }

}