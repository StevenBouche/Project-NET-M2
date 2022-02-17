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
            var entity = await GetBookByIdAsync(id);
            _context.BookGenres.RemoveRange(_context.BookGenres.Where(bg => bg.BookId == id));
            _context.Books.Remove(entity);
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

        public async Task<BookDetailsDto> UpdateBook(BookFormUpdateDto bookFormUpdateDto)
        {
            Book book = await GetBookByIdAsync(bookFormUpdateDto.IdBook);

            using var transaction = _context.Database.BeginTransaction();

            book.Name = bookFormUpdateDto.Name;
            book.Author = bookFormUpdateDto.Author;
            book.Content = bookFormUpdateDto.Content;
            book.Price = bookFormUpdateDto.Price;
           
           var existingGenres = book.BookGenres.Select(bg => bg.GenreId).ToList();
           var updatedListGenres = bookFormUpdateDto.GenresIds;

            //recup les ids genres qui sont dans la liste de base (existingGenres) et qui ne sont pas dans updatedlist (car suppression) sinon il reste dans updated list
            var genreToBeDeleted = existingGenres.Except(updatedListGenres).ToList();
            //var bookGenreToBeDeleted = _context.BookGenres.Where(bg => bg.BookId == book.Id && genreToBeDeleted.Contains(bg.GenreId));
           // _context.BookGenres.(bookGenreToBeDeleted);

            book.BookGenres.RemoveAll(bg => genreToBeDeleted.Contains(bg.GenreId));
            
            //trouver les idsgenre qui sont dans updatedlistgenre et ne sont pas dans existinggenre
            var newListGenres = updatedListGenres.Where(id => !existingGenres.Contains(id)).ToList();

            var addNewBookGenre = newListGenres.Select(genreId => _context.BookGenres.CreateProxy(entity =>
            {
                entity.GenreId = genreId;
                entity.BookId = book.Id;
            })).ToList();

            //_context.BookGenres.AddRange(addNewBookGenre);
            book.BookGenres.AddRange(addNewBookGenre);

            _context.Update(book);

            await _context.SaveChangesAsync();

            transaction.Commit();

            return _mapper.Map<BookDetailsDto>(book);
        }
    }
}
