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

        public PaginationResultDto GetAll(PaginationDto pagination)
        {
            var filter = _context.Books.AsNoTracking();

            if (pagination.IdGenre != null)
            {
                filter = filter.Where(book => book.BookGenres.Any(genre => genre.GenreId == pagination.IdGenre));
            }

            if (!string.IsNullOrWhiteSpace(pagination.AuthorName))
            {
                 filter = filter.Where(entity => entity.Author.Contains(pagination.AuthorName));
            }

            if (!string.IsNullOrWhiteSpace(pagination.Title))
            {
                filter = filter.Where(entity => entity.Name.Contains(pagination.Title));
            }

            var pageEntity = filter
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

            PaginationResultDto paginationResultDto = new PaginationResultDto() { 
                Total=filter.Count(), 
                TotalPages= pageEntity.Count(),
                Books = _mapper.Map<List<BookDto>>(pageEntity.ToList())
            };

            return paginationResultDto;
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
