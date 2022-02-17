using LibraryProject.Business.Dto.Genres;
using System.Collections.Generic;

namespace LibraryProject.WebUI.Models
{
    public class BookForm
    {
        public int? IdBook { get; set; } = null;
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public double Price { get; set; }
        public List<GenreDto> Genres { get; set; } = new();
    }
}
