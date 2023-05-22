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

namespace BuberDinner.Domain.Host;

public sealed class Host : AggregateRoot<HostId>
{
    private readonly List<MenuId> _menuIds = new();
    private readonly List<DinnerId> _dinnerIds = new();
    public string FirstName { get; }
    public string LastName { get; }
    public Uri ProfileImage { get; }
    public AverageRating AverageRating { get; }
    public UserId UserId { get; }
    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public DateTime CreatedDatetime { get; }
    public DateTime UpdatedDatetime { get; }

    private Host(HostId hostId,
                string firstName,
                string lastName,
                Uri profileImage,
                AverageRating averageRating,
                UserId userId,
                DateTime createdDatetime,
                DateTime updatedDatetime):base(hostId)
    {
        FirstName = firstName;
        LastName = lastName;
        ProfileImage = profileImage;
        AverageRating = averageRating;
        UserId = userId;
        CreatedDatetime = createdDatetime;
        UpdatedDatetime = updatedDatetime;
    }

    public static Host Create(HostId hostId,
               string firstName,
               string lastName,
               Uri profileImage,
               AverageRating averageRating,
               UserId userId)
    {
        return new Host(HostId.CreateUnique(), firstName, lastName, profileImage, averageRating, userId, DateTime.UtcNow, DateTime.UtcNow) ;

    }

#pragma warning disable CS8618
    private Host()
    {

    }
#pragma warning restore CS8618
}

