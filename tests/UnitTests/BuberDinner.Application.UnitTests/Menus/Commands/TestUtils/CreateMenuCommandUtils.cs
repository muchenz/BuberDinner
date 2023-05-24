using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.TestUtils.Constants;
using BuberDinner.Domain.Menu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
public static class CreateMenuCommandUtils
{
    public static CreateMenuCommand CreateCommand =
        new CreateMenuCommand(
            Constants.Host.Id.ToString(),
            Constants.Menu.Name,
            Constants.Menu.Description,

        ;

    public static List<MenuSection> CreateSectionCommand(int sectionCount) =>
        Enumerable.Range(0,sectionCount)
            .Select(index=>new MenuSection)
}
