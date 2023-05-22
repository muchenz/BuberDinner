using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Guest.ValueObjects;
public sealed class GuestId : ValueObject
{
    public Guid Value { get; }

    private GuestId(Guid value)
    {
        Value = value;
    }
    public static GuestId Create(string hostId) => new GuestId(Guid.Parse(hostId));
    public static GuestId Create(Guid hostId) => new GuestId(hostId);
    public static GuestId CreateUnique() => new GuestId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private GuestId()
    {

    }
#pragma warning restore CS8618
}

