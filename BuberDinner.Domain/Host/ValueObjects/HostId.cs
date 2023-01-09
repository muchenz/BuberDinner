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
    public static HostId Create(Guid hostId) => new HostId(hostId);
    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private HostId()
    {

    }
#pragma warning restore CS8618
}

