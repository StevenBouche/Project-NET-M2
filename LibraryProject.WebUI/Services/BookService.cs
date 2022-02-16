using LibraryProject.Business.Dto.Books;
using RestSharp;

namespace LibraryProject.WebUI.Services
{
    public class BookService : CommonService
    {

        private readonly RestClient Client;

        public BookService(RestClient client)
        {
            Client = client;
        }

        public Task<PaginationResultDto?> GetPaginateBooksAsync(PaginationDto pagination)
        {
            return TryExecuteAsync(() =>
            {
                var request = new RestRequest("http://localhost:8080/api/book/search",Method.Post)
                .AddJsonBody(pagination);
                return Client.PostAsync<PaginationResultDto>(request);
            });
        }

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