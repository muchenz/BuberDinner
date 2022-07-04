using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Contracts.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public AuthenticationService(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }
        public AuthenticationResult Login(string email, string password)
        {
            return  new AuthenticationResult(Guid.NewGuid(), "EEla", "KKKowalska", "ala.gmail.com", "wdeqer123ew");
        }

        public AuthenticationResult Register(string firsName, string lastName, string email, string password)
        {

            var userId = Guid.NewGuid();

            var token = _tokenGenerator.GenerateToken(userId, firsName, lastName);

            return new AuthenticationResult(userId, firsName, lastName, email, token);

        }
    }
}
