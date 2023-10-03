using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
    public class Pagination
    {
        [Range(1, int.MaxValue, ErrorMessage = "The limit must be greater than 0")]
        public int? Limit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The offset must be greater than 0")]
        public int? Offset { get; set; }
    }
}