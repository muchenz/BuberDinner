using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Guest.ValueObjects;
public sealed class RatingId : ValueObject
{
    public Guid Value { get; }

    private RatingId(Guid value)
    {
        Value = value;
    }
    public static RatingId Create(string hostId) => new RatingId(Guid.Parse(hostId));
    public static RatingId Create(Guid hostId) => new RatingId(hostId);
    public static RatingId CreateUnique() => new RatingId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private RatingId()
    {

    }
#pragma warning restore CS8618
}

