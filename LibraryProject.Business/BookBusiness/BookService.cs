using AutoMapper;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Exceptions;
using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Business.BookBusiness
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

        public async Task DeleteOneBook(int id)
        {
            _context.Books.Remove(await GetBookByIdAsync(id));
            _context.SaveChanges();
        }

        public PaginationResultDto GetAll(PaginationDto pagination)
        {
            var filter = _context.Books.AsQueryable();

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

            PaginationResultDto paginationResultDto = new PaginationResultDto()
            {
                Total = filter.Count(),
                TotalPages = pageEntity.Count(),
                Books = _mapper.Map<List<BookDto>>(pageEntity.ToList())
            };

            return paginationResultDto;
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            var entity = await GetBookByIdAsync(id);

            return _mapper.Map<BookDetailsDto>(entity);
        }

        private async Task<Book> GetBookByIdAsync(int id)
        {
            var entity = await _context.Books.Include(e => e.BookGenres).SingleOrDefaultAsync(book => book.Id == id);

            if (entity == null)
            {
                throw new BookException(BookBusinessExceptionTypes.BOOK_NOT_FOUND, $"avec l'identifiant : {id}");
            }

            return entity;
        }

        public async Task<BookDetailsDto> PostNewBookAsync(BookFormCreateDto bookFormCreateDto)
        {
            var BooksDbSet = _context.Books;
            var BookGenreDbSet = _context.BookGenres;
            var IdsGenre = _context.Genres.Select(genre => genre.Id);
            var isOk = bookFormCreateDto.GenresIds.All(id => IdsGenre.Contains(id));

            if(!isOk)
            {
                var IdsNotFound = bookFormCreateDto.GenresIds.Where(id => !IdsGenre.Contains(id)).ToList();
                var IdsString = string.Join(",", IdsNotFound);
                throw new BookException(BookBusinessExceptionTypes.GENRE_NOT_FOUND, $"sur les IDs : {IdsString}");
            }

            using var transaction = _context.Database.BeginTransaction();
            
            var addedBookEntity = _mapper.Map<Book>(bookFormCreateDto);

            addedBookEntity.BookGenres = bookFormCreateDto.GenresIds.Select(genreId => _context.BookGenres.CreateProxy(entity =>
            {
                entity.GenreId = genreId;
                entity.BookId = addedBookEntity.Id;
            })).ToList();

            BooksDbSet.Add(addedBookEntity);
            await _context.SaveChangesAsync();

            transaction.Commit();

            return _mapper.Map<BookDetailsDto>(addedBookEntity);
        }
    }
}
