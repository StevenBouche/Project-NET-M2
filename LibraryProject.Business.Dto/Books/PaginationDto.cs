using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Dto.Books
{
    public class PaginationDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? IdGenre { get; set; } = null;
        public string? AuthorName { get; set; } = null;
        public string? Title { get; set; } = null;

    }
}
