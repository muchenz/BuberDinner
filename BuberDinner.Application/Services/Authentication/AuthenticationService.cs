using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
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
            return  new[] { Errors.Authentication.InvalidCredentials };
        }

        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public ErrorOr<AuthenticationResult> Register(string firsName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            //throw new DuplicateEmailExeption();
            // return Result.Fail<AuthenticationResult>(new DuplicateEmailError());

            // or list of errors 
            //return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
            return Errors.Users.DuplicateEmail;
        }

        var user = new User { FirstName=firsName, LastName=lastName, Email=email,Password=password };

        _userRepository.Add(user);

        var token = _tokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);

    }

}
