using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menu.ValueObjects;

public class MenuSectionId : ValueObject
{
    public Guid Value { get; }

    private MenuSectionId(Guid value)
    {
        Value = value;
    }

    public static MenuSectionId CreateUnique() => new MenuSectionId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }

    public static MenuSectionId Create(Guid value)
    {
        return new MenuSectionId(value);
    }
#pragma warning disable CS8618
    private MenuSectionId()
    {

    }
#pragma warning restore CS8618
}