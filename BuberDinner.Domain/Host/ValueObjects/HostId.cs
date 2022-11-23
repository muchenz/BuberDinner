using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Host.ValueObjects;
public sealed class HostId : ValueObject
{
    public Guid Value { get; }

    private HostId(Guid value)
    {
        Value = value;
    }

    public static HostId CreateUnique() => new HostId(Guid.NewGuid());
    public static HostId Create(string hostId) => new HostId(Guid.Parse(hostId));
    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
}

