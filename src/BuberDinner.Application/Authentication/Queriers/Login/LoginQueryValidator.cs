using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Queriers.Login;
public class LoginQueryValidator:AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(a => a.Email).NotEmpty();
        RuleFor(a => a.Password).NotEmpty();
    }
}
