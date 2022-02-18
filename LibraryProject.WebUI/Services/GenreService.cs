using LibraryProject.Business.Dto.Genres;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.WebUI.Services
{
    public class GenreService : CommonService
    {
        private readonly RestClient Client;

        public GenreService(RestClient client, SnackBarService service) : base(service)
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