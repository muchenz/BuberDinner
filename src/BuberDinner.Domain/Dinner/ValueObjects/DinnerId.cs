using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Guest.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Dinner.ValueObjects;
public sealed class DinnerId : ValueObject
{
    public Guid Value { get; }

    private DinnerId(Guid value)
    {
        Value = value;
    }
    public static DinnerId Create(string hostId) => new DinnerId(Guid.Parse(hostId));
    public static DinnerId Create(Guid hostId) => new DinnerId(hostId);
    public static DinnerId CreateUnique() => new DinnerId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private DinnerId()
    {

    }
#pragma warning restore CS8618
}

