using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Bill.ValueObjects;
public sealed class BillId : ValueObject
{
    public Guid Value { get; }

    private BillId(Guid value)
    {
        Value = value;
    }
    public static BillId Create(string hostId) => new BillId(Guid.Parse(hostId));
    public static BillId Create(Guid hostId) => new BillId(hostId);
    public static BillId CreateUnique() => new BillId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }
#pragma warning disable CS8618
    private BillId()
    {

    }
#pragma warning restore CS8618
}

