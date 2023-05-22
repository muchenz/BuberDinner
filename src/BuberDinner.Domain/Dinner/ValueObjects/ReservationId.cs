using BuberDinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Dinner.ValueObjects;
public sealed class ReservationId : ValueObject
{
    public Guid Value { get; }

    private ReservationId(Guid value)
    {
        Value = value;
    }
    public static ReservationId Create(string hostId) => new ReservationId(Guid.Parse(hostId));
    public static ReservationId Create(Guid hostId) => new ReservationId(hostId);
    public static ReservationId CreateUnique() => new ReservationId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private ReservationId()
    {

    }
#pragma warning restore CS8618
}

