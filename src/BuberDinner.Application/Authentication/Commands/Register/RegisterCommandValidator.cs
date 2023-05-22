using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Commands.Register;
public class RegisterCommandValidator:AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(a => a.FirstName).NotEmpty();
        RuleFor(a => a.LastName).NotEmpty();
        RuleFor(a => a.Email).NotEmpty();
        RuleFor(a => a.Password).NotEmpty();
    }
}
