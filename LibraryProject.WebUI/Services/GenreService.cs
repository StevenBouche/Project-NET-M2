using LibraryProject.Business.Dto.Genres;
using RestSharp;

namespace LibraryProject.WebUI.Services
{
    public class GenreService : CommonService
    {
        private readonly RestClient Client;

        public GenreService(RestClient client)
        {
            Client = client;
        }

        public Task<List<GenreDto>?> GetGenresAsync()
        {
            var request = new RestRequest($"{BaseURL}/genre");
            return Client.GetAsync<List<GenreDto>>(request);
        }
    }
}