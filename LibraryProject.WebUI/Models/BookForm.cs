using System.Collections.Generic;

namespace LibraryProject.WebUI.Models
{
    public class BookForm
    {
        public int IdBook { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<int> GenresIds { get; set; } = new();
    }
}
