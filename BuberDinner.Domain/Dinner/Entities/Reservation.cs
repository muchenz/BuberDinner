using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using static BuberDinner.Domain.Common.Enums.Enums;

namespace BuberDinner.Domain.Dinner.Entities;
public sealed class Reservation : Entity<ReservationId>
{
    public int GuestCount { get; }
    public ReservationStatus ReservationStatus { get; }
    public GuestId GuestId { get; }
    public BillId BillId { get; }

    public DateTime ArrivalDateTime { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public Reservation(ReservationId reservationId,
                       int guestCount,
                       ReservationStatus reservationStatus,
                       GuestId guestId,
                       BillId billId,
                       DateTime arrivalDateTime,
                       DateTime createdDateTime,
                       DateTime updatedDateTime):base(reservationId)
    {
        GuestCount = guestCount;
        ReservationStatus = reservationStatus;
        GuestId = guestId;
        BillId = billId;
        ArrivalDateTime = arrivalDateTime;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public static Reservation Create(
                       int guestCount,
                       ReservationStatus reservationStatus,
                       GuestId guestId,
                       BillId billId)
    {
        return new (ReservationId.CreateUnique(), guestCount, reservationStatus, guestId, billId, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow);
    }
}
