using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Querrirs;

public interface IAuthenticationQuerryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);

}
