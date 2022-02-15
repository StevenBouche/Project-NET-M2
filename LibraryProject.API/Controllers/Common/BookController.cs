using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.API.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : LibraryBaseController
    {
        public BookController(ILogger<LibraryBaseController> logger) : base(logger)
        {
        }
    }
}
