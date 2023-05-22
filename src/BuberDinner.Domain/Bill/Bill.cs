using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Bill;
public sealed class Bill : AggregateRoot<BillId>
{
    public DinnerId DinnerId { get; }
    public GuestId GuestId { get; }
    public HostId HostId { get; }
    public Price Price { get; }

    public DateTime CreatedDatetime { get; }
    public DateTime UpdatedDatetime { get; }

    private Bill(BillId billId, DinnerId dinnerId, GuestId guestId, HostId hostId, DateTime createdDatetime, DateTime updatedDatetime) :base(billId)
    {

        DinnerId=  dinnerId;
        GuestId= guestId;
        HostId= hostId;
        CreatedDatetime= createdDatetime;
        UpdatedDatetime= updatedDatetime;
    }

    public static Bill Create(DinnerId dinnerId, GuestId guestId, HostId hostId)
    {
        return new(BillId.CreateUnique(), dinnerId, guestId, hostId, DateTime.UtcNow, DateTime.UtcNow);
    }
#pragma warning disable CS8618
    private Bill()
    {

    }
#pragma warning restore CS8618
}
