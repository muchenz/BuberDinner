using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Guest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Menu.ValueObjects;
public sealed class MenuId : AggregateRootId<Guid>
{
    public override Guid Value { get;  protected set; }

    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId CreateUnique() => new MenuId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }

    public static MenuId Create(Guid value)
    {
        return new MenuId(value);
    }
#pragma warning disable CS8618
    private MenuId()
    {

    }
#pragma warning restore CS8618
}
