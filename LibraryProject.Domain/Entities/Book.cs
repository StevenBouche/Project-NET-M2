using LibraryProject.Domain.Common;

namespace LibraryProject.Domain.Entities
{
    public class Book : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Content { get; set; } = string.Empty;
        public virtual List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}