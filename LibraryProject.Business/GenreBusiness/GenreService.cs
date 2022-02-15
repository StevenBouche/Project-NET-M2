using AutoMapper;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Genres;
using LibraryProject.Business.Exceptions;
using LibraryProject.Domain.Entities;
using LibraryProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Business.GenreBusiness
{
    public class GenreService : IGenreService
    {

        private readonly LibraryContext _context;
        private readonly IMapper _mapper;

        public GenreService(LibraryContext context, IMapper mapper) { 
        
            _context = context;
            _mapper = mapper;
        }

        public IList<GenreDto> GetAll()
        {
            var entities = _context.Genres.ToList();
            return _mapper.Map<List<GenreDto>>(entities);

        }

        public GenreDto GetById(int id)
        {
            return _mapper.Map<GenreDto>(GetGenreEntityById(id));
        }

        public async Task<GenreDto> CreateGenreAsync(GenreFormCreateDto genreFormCreateDto)
        {
            var entityCreated = new Genre { Name = genreFormCreateDto.Name};
            //var entityCreated = _mapper.Map<Genre>(genreFormCreateDto);
            _context.Genres.Add(entityCreated);
            await _context.SaveChangesAsync();

            return _mapper.Map<GenreDto>(entityCreated);
        }

        public void DeleteGenreById(int id)
        {
            _context.Remove(GetGenreEntityById(id));
            _context.SaveChanges();
        }

        private Genre GetGenreEntityById(int id)
        {
            var entity = _context.Genres.SingleOrDefault(genre => genre.Id == id);

            if (entity == null)
            {
                throw new GenreException(GenreExceptionTypes.GENRE_NOT_FOUND, $"avec l'identifiant {id}");
            }

            return entity;
        }

        public IList<BookDto> GetAllBooksByGenreId(int idGenre)
        {
            var entities = _context.Books.Where(book => book.BookGenres.Any(g => g.GenreId == idGenre)).ToList();
            return _mapper.Map<List<BookDto>>(entities);
        }
    }
}
