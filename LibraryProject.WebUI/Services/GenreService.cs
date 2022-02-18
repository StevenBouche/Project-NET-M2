using LibraryProject.Business.Dto.Genres;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise.Snackbar;

namespace LibraryProject.WebUI.Services
{
    public class GenreService : CommonService
    {
        private readonly RestClient Client;

        public GenreService(RestClient client, SnackBarService service) : base(service)
        {
            Client = client;
        }


        public async Task<List<GenreDto>?> GetGenresAsync()
        {
            var request = new RestRequest($"{BaseURL}/genre");
            var result = await Client.ExecuteGetAsync<List<GenreDto>>(request);
            return HandleResult(result);
        }

        public async Task DeleteGenre(int id)
        {
            var request = new RestRequest($"{BaseURL}/genre/{id}", Method.Delete);
            var result = await Client.ExecuteAsync(request);

            if (!result.IsSuccessful)
            {
                SnackService.SnackbarStack?.PushAsync(result.Content, SnackbarColor.Danger, options => { options.IntervalBeforeClose = 2000; });
            }
        }

        public async Task<GenreDto?> CreateGenre(GenreFormCreateDto genreFormCreateDto)
        {
            var request = new RestRequest($"{BaseURL}/genre", Method.Post).AddJsonBody(genreFormCreateDto);
            var result = await Client.ExecutePostAsync<GenreDto>(request);
            return HandleResult(result);
        }
    }
}