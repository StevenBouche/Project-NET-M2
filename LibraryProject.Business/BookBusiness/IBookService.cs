using LibraryProject.Business.Dto.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.BookBusiness
{
    public interface IBookService
    {
        public Task<BookDetailsDto> GetByIdAsync(int id);

        public PaginationResultDto GetAll(PaginationDto pagination);

        public Task<BookDetailsDto> PostNewBookAsync(BookFormCreateDto bookFormCreateDto);
    }
}
