using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Common.Enums;
public class Enums
{
    public enum Currency { USD }

    public enum DinnerStatus { Upcoming, InProgress, Ended, Cancelled }

    public enum ReservationStatus { PendingGuestConfirmation, Reserved, Cancelled }
}
