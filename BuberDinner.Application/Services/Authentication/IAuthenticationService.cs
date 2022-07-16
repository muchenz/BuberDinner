using BuberDinner.Application.Common.Errors;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email , string password);

    ErrorOr<AuthenticationResult> Register(string firsName,
    string lastName,
    string email,
    string password);
}
