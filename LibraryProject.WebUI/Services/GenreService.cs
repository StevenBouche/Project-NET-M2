using LibraryProject.Business.Dto.Genres;

namespace LibraryProject.WebUI.Services
{
    public class GenreService
    {
        private readonly List<GenreDto> Genres = new()
        {
            new GenreDto() { Id = 1, Name = "Genre1", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new GenreDto() { Id = 2, Name = "Genre2", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
            new GenreDto() { Id = 3, Name = "Genre3", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow }
        };

        public List<GenreDto> GetBooks()
        {
            return Genres;
        }
    }
}