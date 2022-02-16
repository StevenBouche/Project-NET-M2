using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Dto.Books
{
    public class PaginationResultDto
    {
        [JsonProperty("books")]
        public List<BookDto> Books { get; set; } = new();
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }
}
