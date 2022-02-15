using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Dto.Books
{
    public class PaginationResultDto
    {
        public List<BookDto> Books;
        public int Total;
        public int TotalPages;
    }
}
