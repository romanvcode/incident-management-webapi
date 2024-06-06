using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("error")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Handles error requests and returns a custom error response
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            return Problem(
                detail: exception.Message,
                title: "An error occurred",
                statusCode: 500
            );
        }
    }
}