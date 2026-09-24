using Airnb.Domain.Entities;
using Airnb.Domain.Enums;
using Airnb.Domain.Exceptions;
using Airnb.Domain.ValueObjects;

namespace Airnb.Domain.DomainServices
{
    public class BookingConflictChecker : IBookingConflictChecker
    {
        public void ValidateWithoutOverlapping(
            TimeRange timeRange,
            int numberOfGuests,
            Home home,
            IEnumerable<Booking> existingBookings)
        {
            // Tjek om huset er ledigt i perioden
            var hasOverlap = existingBookings
                .Any(b => b.HomeId == home.Id &&
                          b.Status != BookingStatus.Cancelled &&
                          b.Status != BookingStatus.CheckedOut &&
                          b.TimeRange.OverlapsWith(timeRange));

            if (hasOverlap)
                throw new DomainException("Huset er allerede booket i den periode.");

            // Tjek om antal gæster overstiger husets kapacitet
            if (numberOfGuests > home.Capacity)
                throw new DomainException($"Huset har kun plads til {home.Capacity} gæster.");
        }
    }
}
