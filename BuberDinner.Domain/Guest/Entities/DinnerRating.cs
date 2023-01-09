using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Dinner.Entities;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using static BuberDinner.Domain.Common.Enums.Enums;

namespace BuberDinner.Domain.Guest.Entities;

public sealed class DinnerRating : Entity<RatingId>
{
    public HostId HostId { get; }
    public DinnerId DinnerId { get; }
    public Rating Rating { get; }

    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private DinnerRating(RatingId ratingId,
                        HostId hostId,
                        DinnerId dinnerId,
                        Rating rating,
                        DateTime createdDateTime,
                        DateTime updatedDateTime) :base(ratingId)
    {
        HostId = hostId;
        DinnerId = dinnerId;
        Rating = rating;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static DinnerRating Create(
                       HostId hostId,
                       DinnerId dinnerId,
                       Rating rating)
    {
        return new DinnerRating(RatingId.CreateUnique(), hostId, dinnerId, rating, DateTime.UtcNow, DateTime.UtcNow);
    }
#pragma warning disable CS8618
    private DinnerRating()
    {

    }
#pragma warning restore CS8618
}


