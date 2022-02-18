using LibraryProject.Business.Dto.Genres;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryProject.WebUI.Models
{
    public class BookForm
    {
        public int? IdBook { get; set; } = null;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Price { get; set; }
        public List<GenreDto> Genres { get; set; } = new();
    }
}
