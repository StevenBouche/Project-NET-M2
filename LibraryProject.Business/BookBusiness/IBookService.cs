using LibraryProject.Business.Dto.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Common
{
    public interface IBookService
    {
        public Task<BookDetailsDto> GetByIdAsync(int id);

        public PaginationResultDto GetAll(PaginationDto pagination);
    }
}
