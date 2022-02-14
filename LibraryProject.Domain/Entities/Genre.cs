using LibraryProject.Domain.Common;

namespace LibraryProject.Domain.Entities
{
    public class Genre : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual IList<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
    }
}