using LibraryProject.Business.Dto.Books;
using LibraryProject.WebUI.Models;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.WebUI.Services
{
    public class BookService : CommonService
    {

        private readonly RestClient Client;

        public BookService(RestClient client)
        {
            Client = client;
        }

        public Task<BookDetailsDto?> GetBookDetailsById(int id)
        {
            return TryExecuteAsync(() =>
            {
                var request = new RestRequest($"{BaseURL}/book/{id}");
                return Client.GetAsync<BookDetailsDto>(request);
            });
        }

        public Task<PaginationResultDto?> GetPaginateBooksAsync(PaginationDto pagination)
        {
            return TryExecuteAsync(() =>
            {
                var request = new RestRequest($"{BaseURL}/book/search")
                .AddQueryParameter("Page", pagination.Page)
                .AddQueryParameter("PageSize", pagination.PageSize);

                if (pagination.IdGenre != null && pagination.IdGenre > 0)
                    request = request.AddQueryParameter("IdGenre", (int)pagination.IdGenre);

                if(!string.IsNullOrWhiteSpace(pagination.Title))
                    request = request.AddQueryParameter("Title", pagination.Title);

                if (!string.IsNullOrWhiteSpace(pagination.AuthorName))
                    request = request.AddQueryParameter("AuthorName", pagination.AuthorName);
                
                return Client.GetAsync<PaginationResultDto>(request);
            });
        }

        public Task<BookDetailsDto?> CreateBook(BookForm form)
        {
            return TryExecuteAsync(() =>
            {
                var createObj = new BookFormCreateDto()
                {
                    Name = form.Name,
                    Content = form.Content,
                    Author = form.Author,
                    Price = form.Price,
                    GenresIds = form.Genres.Select(g => g.Id).ToList()
                };

                var request = new RestRequest($"{BaseURL}/book", Method.Post).AddJsonBody(createObj);
                return Client.PostAsync<BookDetailsDto>(request);
            });
        }

        public async Task<BookDetailsDto?> UpdateBook(BookForm form)
        {
            return await TryExecuteAsync(async () =>
            {
                if (form.IdBook == null)
                    return null;

                var createObj = new BookFormUpdateDto()
                {
                    IdBook = (int)form.IdBook,
                    Name = form.Name,
                    Content = form.Content,
                    Author = form.Author,
                    Price = form.Price,
                    GenresIds = form.Genres.Select(g => g.Id).ToList()
                };

                var request = new RestRequest($"{BaseURL}/book", Method.Put).AddJsonBody(createObj);
                return await Client.PutAsync<BookDetailsDto>(request);
            });
        }

        public async Task DeleteBook(int id)
        {
            var request = new RestRequest($"{BaseURL}/book/{id}", Method.Delete);
            await Client.DeleteAsync<BookDetailsDto>(request);
        }
    }
}