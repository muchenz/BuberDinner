using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.Entities;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Guest;


public sealed class Guest : AggregateRoot<GuestId>
{
    private readonly List<UserId> _userIds = new();
    private readonly List<DinnerId> _upcomingDinnerIds = new();
    private readonly List<DinnerId> _pastDinnerIds = new();
    private readonly List<DinnerId> _pendingDinnerIds = new();
    private readonly List<BillId> _billIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();
    private readonly List<DinnerRating> _dinnerRatings = new();
    public string FirstName { get; }
    public string LastName { get; }
    public Uri ProfileImage { get; }
    public AverageRating AverageRating { get; }
    public IReadOnlyList<UserId> UserIds => _userIds.AsReadOnly();
    public IReadOnlyList<DinnerId> UpcomingDinnerIds => _upcomingDinnerIds.AsReadOnly();
    public IReadOnlyList<DinnerId> PastDinnerIds => _pastDinnerIds.AsReadOnly();
    public IReadOnlyList<DinnerId> PendingDinnerIds => _pendingDinnerIds.AsReadOnly();
    public IReadOnlyList<BillId> BillIds => _billIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();
    public IReadOnlyList<DinnerRating> DinnerRatings => _dinnerRatings.AsReadOnly();
    public DateTime CreatedDatetime { get; }
    public DateTime UpdatedDatetime { get; }

    public Guest(GuestId guestId,
                string firstName,
                 string lastName,
                 Uri profileImage,
                 AverageRating averageRating,
                 DateTime createdDatetime,
                 DateTime updatedDatetime):base(guestId)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileImage = profileImage;
        AverageRating = averageRating;
        CreatedDatetime = createdDatetime;
        UpdatedDatetime = updatedDatetime;
    }

    public static Guest Create(
                string firstName,
                string lastName,
                Uri profileImage,
                AverageRating averageRating)
    {
        return new Guest(GuestId.CreateUnique(), firstName, lastName, profileImage, averageRating, DateTime.UtcNow, DateTime.UtcNow);
    }

#pragma warning disable CS8618
    private Guest()
    {

    }
#pragma warning restore CS8618
}


