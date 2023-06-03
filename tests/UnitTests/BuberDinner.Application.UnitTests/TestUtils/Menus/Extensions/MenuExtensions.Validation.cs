using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.TestUtils.Menus.Extensions;
public static partial class MenuExtensions
{
    public static void ValidateCreatedFrom(this Menu menu, CreateMenuCommand command)
    {
        menu.Name.Should().Be(command.Name);
        menu.Description.Should().Be(command.Description);
        menu.Sections.Should().HaveCount(command.Sections.Count);
        menu.Sections.Zip(command.Sections).ToList().ForEach(pair => ValidateSection(pair.First, pair.Second));
    }

    private static void ValidateSection(MenuSection section, CreateMenuSectionCommand command)
    {
        section.Id.Should().BeNull();
        section.Name.Should().Be(command.Name);
        section.Description.Should().Be(command.Description);
        section.Items.Should().HaveCount(command.Items.Count);
        section.Items.Zip(command.Items).ToList().ForEach(pair => ValidateItem(pair.First, pair.Second));
    }

    private static void ValidateItem(MenuItem item, CreateMenuItemCommand command)
    {
        item.Id.Should().NotBeNull();
        item.Name.Should().Be(command.Name);
        item.Description.Should().Be(command.Description);
    }
}
