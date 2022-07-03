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
        public AuthenticationResult Login(string email, string password)
        {
            return  new AuthenticationResult(Guid.NewGuid(), "EEla", "KKKowalska", "ala.gmail.com", "wdeqer123ew");
        }

        public AuthenticationResult Register(string firsName, string lastName, string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), firsName, lastName, email, "333r3wdeqer123ew");

        }
    }
}
