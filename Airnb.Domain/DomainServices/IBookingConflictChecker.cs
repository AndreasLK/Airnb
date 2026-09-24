using Airnb.Domain.Entities;
using Airnb.Domain.ValueObjects;

namespace Airnb.Domain.DomainServices
{
    public interface IBookingConflictChecker
    {
        public void ValidateWithoutOverlapping(
            TimeRange timeRange,
            int numberOfGuests,
            Home home,
            IEnumerable<Booking> existingBookings);
    }
}
