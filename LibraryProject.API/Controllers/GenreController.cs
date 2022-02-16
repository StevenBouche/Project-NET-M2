using LibraryProject.API.Controllers.Common;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Common;
using LibraryProject.Business.Dto.Genres;
using LibraryProject.Business.GenreBusiness;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : LibraryBaseController
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service, ILogger<GenreController> logger) : base(logger)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(GenreDto), 200)]
        public ActionResult<List<GenreDto>> GetAllGenres()
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(_service.GetAll());
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public ActionResult<GenreDto> GetGenreById(int id)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(_service.GetById(id));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(GenreDto),201)]
        public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] GenreFormCreateDto genreFormCreateDto)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var result = await _service.CreateGenreAsync(genreFormCreateDto);
                return Created($"/api/genre/{result.Id}", result);
            });

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteGenreById(int id)
        {
            return TryExecute(() =>
            {
                _service.DeleteGenreById(id);
                return Ok();
            });
            
        }

        [HttpGet("/{id}/books")]
        [ProducesResponseType(typeof(ApiErrorResponse), 400)]
        [ProducesResponseType(typeof(BookDto), 200)]
        public ActionResult GetAllBooksByGenreId(int id)
        {
            return TryExecute(() =>
            {
                return Ok(_service.GetAllBooksByGenreId(id));
            });
        }

    }
    
}
