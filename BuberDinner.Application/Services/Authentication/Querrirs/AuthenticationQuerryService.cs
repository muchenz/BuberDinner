using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Querrirs;

public class AuthenticationQuerryService : IAuthenticationQuerryService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQuerryService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {

        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            //throw new Exception("User doesn't exist.");
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != password)
        {
            //throw new Exception("Invalid password.");
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

}
