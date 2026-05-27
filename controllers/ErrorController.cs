using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace UserManagement.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        var context =
            HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        var exception = context?.Error;

        if (exception != null)
        {
            Log.Error(exception, "An unhandled exception occurred.");
        }

        return Problem(
            detail: exception?.Message,
            title: "An unexpected error occurred.",
            statusCode: 500
        );
    }
}
