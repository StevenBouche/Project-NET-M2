using LibraryProject.Business.Dto.Books;
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
            Thread.Sleep(1000);
            try
            {   
                HttpResponseMessage response = await client.GetAsync($"{API_URL}/book/{id}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookDetailsDto>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine("HttpRequestException :{0} ", e.Message);
            }
            return null;
        }

        public static async Task<BookDetailsDto?> search(
            int page,
            int pageSize,
            int idGenre = -1,
            string AuthorName = "",
            string title=""
        )
        {
            Thread.Sleep(3000);
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{API_URL}/book/{1}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BookDetailsDto>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine("HttpRequestException :{0} ", e.Message);
            }
            return null;
        }
    }
}