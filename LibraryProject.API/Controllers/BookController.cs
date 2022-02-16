using LibraryProject.Business.BookBusiness;
using LibraryProject.Business.Dto.Books;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BookController : LibraryBaseController
    {
        private readonly IBookService _bookService;
        public BookController(ILogger<LibraryBaseController> logger, IBookService bookService) : base(logger)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDetailsDto), 200)]
        public async Task<ActionResult> Get(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await _bookService.GetByIdAsync(id));
            });
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(PaginationResultDto), 200)]
        public  ActionResult Get([FromQuery] PaginationDto data)
        {
            return TryExecute<ActionResult>( () =>
            {
                return Ok(_bookService.GetAll(data));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookDetailsDto), 201)]
        public async Task<ActionResult> Post([FromBody] BookFormCreateDto data)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var resultDto = await _bookService.PostNewBookAsync(data);
                return Created($"/api/book/{resultDto.Id}", resultDto);
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public Task<ActionResult> Delete(int id)
        {
            return TryExecuteAsync<ActionResult>( async () =>
            {
                await _bookService.DeleteOneBook(id);
                return Ok();
            });
        }

    }
}
