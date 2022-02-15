using LibraryProject.Business.Dto.Genres;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Dto.Books
{
    public class BookDetailsDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("author")]
        public string Author { get; set; } = string.Empty;

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;

        [JsonProperty("books")]
        public IList<GenreDto> Genres { get; set; } = new List<GenreDto>();

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
