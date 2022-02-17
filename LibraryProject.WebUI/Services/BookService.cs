using LibraryProject.Business.Dto.Books;
using RestSharp;
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

                if(!string.IsNullOrEmpty(pagination.Title))
                    request = request.AddQueryParameter("Title", pagination.Title);

                if (!string.IsNullOrEmpty(pagination.AuthorName))
                    request = request.AddQueryParameter("AuthorName", pagination.AuthorName);
                
                return Client.GetAsync<PaginationResultDto>(request);
            });
        }
    }
}