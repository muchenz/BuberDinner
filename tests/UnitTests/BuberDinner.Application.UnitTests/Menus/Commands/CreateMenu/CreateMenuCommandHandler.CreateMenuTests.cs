using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Application.UnitTests.Menus.Commands.TestUtils;
using BuberDinner.Application.UnitTests.TestUtils.Menus.Extensions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.UnitTests.Menus.Commands.CreateMenu;
public class CreateMenuCommandHandlerTests
{
    //T1: sut (system under the test)
    //T2: Scenario
    //T3: expected outcome
    public void T1_T2_T3() { }



    private readonly CreateMenuCommandHandler _handler;
    private readonly Mock<IMenuRepository> _mockMenuRepository;

    public CreateMenuCommandHandlerTests()
    {
        _mockMenuRepository = new Mock<IMenuRepository>();
        _handler = new CreateMenuCommandHandler(_mockMenuRepository.Object);
    }
    [Theory]
    [MemberData(nameof(ValidCreateMenuCommands))]
    public async void HandleCreateMenuCommand_WhenMenuWasValid_ShouldCreateAndReturnMenu(CreateMenuCommand createMenuCommand)
    {
        //Arrange
        //createMenuCommand = CreateMenuCommandUtils.CreateCommand();
        //Act
        var result = await _handler.Handle(createMenuCommand, default);
        //Assert
        result.IsError.Should().BeFalse();

        result.Value.ValidateCreatedFrom(createMenuCommand);

        _mockMenuRepository.Verify(m => m.Add(result.Value), Times.Once);
    }

    public static IEnumerable<object[]> ValidCreateMenuCommands()
    {
        yield return new[] { CreateMenuCommandUtils.CreateCommand() };
        yield return new object[] { CreateMenuCommandUtils.CreateCommand(
            sections: CreateMenuCommandUtils.CreateSectionsCommand(sectionCount:3)
            ) };
        yield return new object[] { CreateMenuCommandUtils.CreateCommand(
            sections: CreateMenuCommandUtils.CreateSectionsCommand(
                sectionCount:3,
                CreateMenuCommandUtils.CreateItemsCommand(3) )
            ) };
    }
}
