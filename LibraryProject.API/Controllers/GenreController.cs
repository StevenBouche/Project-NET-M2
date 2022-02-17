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
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        public ActionResult<List<GenreDto>> GetAllGenres()
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(_service.GetAll());
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
        public ActionResult<GenreDto> GetGenreById(int id)
        {
            return TryExecute<ActionResult>(() =>
            {
                return Ok(_service.GetById(id));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] GenreFormCreateDto genreFormCreateDto)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var result = await _service.CreateGenreAsync(genreFormCreateDto);
                return Created($"/api/genre/{result.Id}", result);
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        public ActionResult GetAllBooksByGenreId(int id)
        {
            return TryExecute(() =>
            {
                return Ok(_service.GetAllBooksByGenreId(id));
            });
        }
    }
}