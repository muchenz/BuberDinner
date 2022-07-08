using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BuberDinner.Api.Controllers;
//[Route("api/[controller]")]
[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exeption = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(title: exeption?.Message, statusCode: 400);
    }
}
