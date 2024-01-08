using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace IOTBack.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController:ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError() => Problem();

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment host)
        {
            if (!host.IsDevelopment())
            {
                return NotFound();
            }

            var e = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(
                    detail: e.Error.StackTrace,
                    title: e.Error.Message
                );
        }
    }
}
