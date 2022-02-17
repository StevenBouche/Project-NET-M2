using LibraryProject.API.Hubs;
using LibraryProject.Business.BookBusiness;
using LibraryProject.Business.Dto.Books;
using LibraryProject.Business.Dto.Common;
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
        private readonly LibraryProxyHub _hub;
        public BookController(ILogger<LibraryBaseController> logger, IBookService bookService, LibraryProxyHub hub) : base(logger)
        {
            _bookService = bookService;
            _hub = hub;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                return Ok(await _bookService.GetByIdAsync(id));
            });
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PaginationResultDto), StatusCodes.Status200OK)]
        public  ActionResult Get([FromQuery] PaginationDto data)
        {
            return TryExecute<ActionResult>( () =>
            {
                return Ok(_bookService.GetAll(data));
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] BookFormCreateDto data)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var resultDto = await _bookService.PostNewBookAsync(data);
                await _hub.OnCreatedBook(resultDto);
                return Created($"/api/book/{resultDto.Id}", resultDto);
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public Task<ActionResult> Delete(int id)
        {
            return TryExecuteAsync<ActionResult>( async () =>
            {
                await _bookService.DeleteOneBook(id);
                await _hub.OnDeletedBook(id);
                return Ok();
            });
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookDetailsDto), StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] BookFormUpdateDto bookFormUpdateDto)
        {
            return await TryExecuteAsync<ActionResult>(async () =>
            {
                var resultDto = await _bookService.UpdateBook(bookFormUpdateDto);
                return Ok(resultDto);
            });
        }

    }
}
