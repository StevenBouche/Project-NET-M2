using Newtonsoft.Json;

namespace LibraryProject.Business.Dto.Books
{
    public class BookFormUpdateDto
    {

        [JsonProperty("id")]
        public int IdBook { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        [JsonProperty("author")]
        public string Author { get; set; } = string.Empty;

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("genresIds")]
        public List<int> GenresIds { get; set; } = new();


    }
}
