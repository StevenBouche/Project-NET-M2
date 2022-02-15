using AutoMapper;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Exceptions;
using LibraryProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Common
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public BookService(LibraryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            var entity = await _context.Books.Where(book =>  book.Id == id).FirstOrDefaultAsync();

            if(entity == null)
            {
                throw new BookException(BookBusinessExceptionTypes.BOOK_NOT_FOUND, $"avec l'identifiant : {id}");
            }

            return _mapper.Map<BookDetailsDto>(entity);
        }
    }
}
