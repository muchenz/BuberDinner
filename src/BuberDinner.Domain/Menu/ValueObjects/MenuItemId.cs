using BuberDinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.ValueObjects;
public sealed class MenuItemId : ValueObject
{
    public Guid Value { get; }

    private MenuItemId(Guid value)
    {
        Value = value;
    }

    public static MenuItemId CreateUnique() => new MenuItemId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }

    public static MenuItemId Create(Guid value)
    {
        return new MenuItemId(value);
    }

#pragma warning disable CS8618
    private MenuItemId()
    {

    }
#pragma warning restore CS8618
}
