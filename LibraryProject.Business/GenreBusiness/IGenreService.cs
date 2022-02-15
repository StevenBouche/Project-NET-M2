using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Genres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.GenreBusiness
{
    public interface IGenreService
    {

        /*
         Genre API
            POST /genres => GenreCreateForm 
            GET /genres => Get all genres
            DELETE /genres/{id}
            GET /genres/{id}/books
         */

        IList<GenreDto> GetAll();
        GenreDto GetById(int id);
        Task<GenreDto> CreateGenreAsync(GenreFormCreateDto genreFormCreateDto);
        void DeleteGenreById(int id);
        IList<BookDto> GetAllBooksByGenreId(int idGenre);


    }
}
