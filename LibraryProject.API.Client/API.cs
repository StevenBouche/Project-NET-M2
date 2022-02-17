using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Genres;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LibraryProject.API.Client
{
    public class API
    {
        static readonly HttpClient client = new HttpClient();
        static readonly string API_URL = "http://localhost:8080/api";

        public static async Task<BookDetailsDto?> findById(int id)
        {
            try
            {   
                HttpResponseMessage response = await client.GetAsync($"{API_URL}/book/{id}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Trace.WriteLine("book: " + responseBody);
                return JsonConvert.DeserializeObject<BookDetailsDto>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine("HttpRequestException :{0} ", e.Message);
            }
            return null;
        }

        public static async Task<PaginationResultDto?> search(
            int page,
            int pageSize,
            int idGenre = -1,
            string AuthorName = "",
            string title=""
        )
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{API_URL}/book/search?Page={page}&PageSize={pageSize}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PaginationResultDto>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine("HttpRequestException :{0} ", e.Message);
            }
            return null;
        }

        public static async Task<List<GenreDto>?> getAllGenres()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{API_URL}/genre/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GenreDto>>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine("HttpRequestException :{0} ", e.Message);
            }
            return null;
        }
    }
}