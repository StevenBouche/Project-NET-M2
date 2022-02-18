using LibraryProject.Business.Dto.Books;
using LibraryProject.WebUI.Models;
using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using Blazorise.Snackbar;

namespace LibraryProject.WebUI.Services
{
    public class BookService : CommonService
    {

        private readonly RestClient Client;

        public BookService(RestClient client, SnackBarService service) : base(service)
        {
            Client = client;
        }

        public async Task<BookDetailsDto?> GetBookDetailsById(int id)
        {
            var request = new RestRequest($"{BaseURL}/book/{id}");
            var result = await Client.ExecuteGetAsync<BookDetailsDto>(request);
            return HandleResult(result);
        }

        public async Task<PaginationResultDto?> GetPaginateBooksAsync(PaginationDto pagination)
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

            if (!string.IsNullOrWhiteSpace(pagination.OrderBy))
                request = request.AddQueryParameter("OrderBy", pagination.OrderBy);

            var result = await Client.ExecuteGetAsync<PaginationResultDto?>(request);

            return HandleResult(result);
        }

        public async Task<BookDetailsDto?> CreateBook(BookForm form)
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
            var result = await Client.ExecutePostAsync<BookDetailsDto>(request);

            return HandleResult(result);
        }

        public async Task<BookDetailsDto?> UpdateBook(BookForm form)
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
            var result = await Client.ExecutePutAsync<BookDetailsDto>(request);

            return HandleResult(result);
        }

        public async Task DeleteBook(int id)
        {
            var request = new RestRequest($"{BaseURL}/book/{id}", Method.Delete);
            var result = await Client.ExecuteAsync(request);

            if (!result.IsSuccessful)
            {
                SnackService.SnackbarStack?.PushAsync(result.Content, SnackbarColor.Success, options => { options.IntervalBeforeClose = 2000; });
            }
        }
    }
}