using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queriers.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;

using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);


        return registerResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
       

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {

        var query = _mapper.Map<LoginQuery>(request);

        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);



        if (loginResult.IsError && loginResult.FirstError == BuberDinner.Domain.Common.Errors.Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);
        }

        return loginResult.Match(
              authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
              errors => Problem(errors));
    }

}
