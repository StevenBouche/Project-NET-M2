using LibraryProject.Business.Dto.Books;

namespace LibraryProject.WebUI.Services
{
    public class BookService
    {
        private readonly List<BookDto> Books = new()
        {
            new BookDto() { Id = 1, Name = "Livre1", Author = "Moi", Price = 1, CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new BookDto() { Id = 2, Name = "Livre2", Author = "Moi", Price = 2, CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new BookDto() { Id = 3, Name = "Livre3", Author = "Moi", Price = 3, CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow }
        };

        public List<BookDto> GetBooks()
        {
            return Books;
        }
    }
}