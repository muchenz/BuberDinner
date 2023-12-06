using BuberDinner.Domain.Common.Models;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace BubberDinner.Domain.ArchitectureTests;

public class DomainTests
{
    private static readonly Assembly DomainAssembly = typeof(Entity<>).Assembly;
    [Fact]
    public void Entities_Sould_havePrivateParamtrelessConstructor()
    {

        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(AggregateRoot2<,>))
            .GetTypes();
        var failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructor = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!constructor.Any(c=>c.IsPrivate && c.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();

       }

    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_HaveEDending()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("ed")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}