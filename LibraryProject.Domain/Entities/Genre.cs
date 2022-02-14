using LibraryProject.Domain.Common;

namespace LibraryProject.Domain.Entities
{
    public class Genre : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IList<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}