using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queriers.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;

using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
[ApiController]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirsName, request.LastName, request.Email, request.Password);

        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);


        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                        authResult.user.Id,
                        authResult.user.FirstName,
                        authResult.user.LastName,
                        authResult.user.Email,
                        authResult.Token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var query = new LoginQuery(request.Email, request.Password);

        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);


 
        if (loginResult.IsError && loginResult.FirstError == BuberDinner.Domain.Common.Errors.Errors.Authentication.InvalidCredentials) 
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);
        }

        return loginResult.Match(
              authResult => Ok(MapAuthResult(authResult)),
              errors => Problem(errors));
    }

}
