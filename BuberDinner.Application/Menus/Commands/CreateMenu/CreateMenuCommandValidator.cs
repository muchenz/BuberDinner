using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;
public  class CreateMenuCommandValidator: AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(a => a.Name).NotEmpty();
        RuleFor(a => a.Description).NotEmpty();
        RuleFor(a => a.Sections).NotEmpty();
    }
}
