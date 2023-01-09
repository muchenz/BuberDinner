using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.Entities;
using BuberDinner.Domain.Dinner.ValueObjects;
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
using static BuberDinner.Domain.Common.Enums.Enums;

namespace BuberDinner.Domain.Dinner;

public sealed class Dinner : AggregateRoot<DinnerId>
{
    private readonly List<Reservation> _reservations = new();

    public string Name { get; }
    public string Description { get; }

    public DateTime StartDatetime { get; }
    public DateTime EndDatetime { get; }

    public DateTime StartedDatetime { get; }
    public DateTime EndedDatetime { get; }

    public DinnerStatus Status { get; }
    public bool IsPublic { get; }
    public int MaxGests { get; }
    public Price Price { get; }
    public HostId HostId { get; }
    public MenuId MenuId { get; }
    
    public Uri ImageUrl { get; } 
    public Location Location { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    public DateTime CreatedDatetime { get; }
    public DateTime UpdatedDatetime { get; }

    public Dinner(DinnerId dinnerId,
                  string name,
                  string description,
                  DateTime startDatetime,
                  DateTime endDatetime,
                  DateTime startedDatetime,
                  DateTime endedDatetime,
                  DinnerStatus status,
                  bool isPublic,
                  int maxGests,
                  Price price,
                  HostId hostId,
                  MenuId menuId,
                  Uri imageUrl,
                  Location location,
                  DateTime createdDatetime,
                  DateTime updatedDatetime) :base(dinnerId)
    {
        Name = name;
        Description = description;
        StartDatetime = startDatetime;
        EndDatetime = endDatetime;
        StartedDatetime = startedDatetime;
        EndedDatetime = endedDatetime;
        Status = status;
        IsPublic = isPublic;
        MaxGests = maxGests;
        Price = price;
        HostId = hostId;
        MenuId = menuId;
        ImageUrl = imageUrl;
        Location = location;
        CreatedDatetime = createdDatetime;
        UpdatedDatetime = updatedDatetime;
    }

    public static Dinner Create(DinnerId dinnerId,
                  string name,
                  string description,
                  DinnerStatus status,
                  bool isPublic,
                  int maxGests,
                  Price price,
                  HostId hostId,
                  MenuId menuId,
                  Uri imageUrl,
                  Location location)
    {
        return new Dinner(DinnerId.CreateUnique(), name, description, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, status,
            isPublic, maxGests, price, hostId, menuId, imageUrl, location, DateTime.UtcNow, DateTime.UtcNow);
    }
#pragma warning disable CS8618
    private Dinner()
    {

    }
#pragma warning restore CS8618


}
