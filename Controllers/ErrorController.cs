using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using ASPCoreWebApplication.ExceptionHandler;

namespace ASPCoreWebApplication.Controllers
{
    [ApiController]
    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)] // скрыть в swagger
    public class ErrorController : ControllerBase
    {
        [HttpGet, HttpPost, HttpPut, HttpDelete]
        public IActionResult HandleError()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionFeature?.Error;

            if (ex is NotFoundException nf)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Album not found",
                    detail: nf.Message);
            }

            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                    title: "Unexpected Error",
                    detail: "An unexpected error occured");
        }
    }
}
