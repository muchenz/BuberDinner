using BuberDinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.ValueObjects;
public sealed class MenuId : ValueObject
{
    public Guid Value { get;  }

    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId CreateUnique() => new MenuId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
}
