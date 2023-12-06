using BuberDinner.Domain.Common.Models;
using FluentAssertions;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BubberDinner.Domain.ArchitectureTests;
public class LayerTests
{
    private static readonly Assembly DomainAssembly = typeof(Entity<>).Assembly;

    [Fact]
    public void Domain_Should_notHaveDependencyOnApplication()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("Application")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
