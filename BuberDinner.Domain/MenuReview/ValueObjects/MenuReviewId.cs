using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.MenuReview.ValueObjects;
public sealed class MenuReviewId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MenuReviewId(Guid value)
    {
        Value = value;
    }
    public static MenuReviewId Create(string hostId) => new MenuReviewId(Guid.Parse(hostId));
    public static MenuReviewId Create(Guid hostId) => new MenuReviewId(hostId);
    public static MenuReviewId CreateUnique() => new MenuReviewId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private MenuReviewId()
    {

    }
#pragma warning restore CS8618
}

