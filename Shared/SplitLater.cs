using Domain.Enums;

namespace Shared
{
    public record BookingResponse(
        Guid Id,
        Guid GuestId,
        Guid HomeId,
        BookingStatus Status,
        DateTime CreatedTime,
        int NumberOfGuests,
        double Price);

    public record CreateBookingRequest(
        Guid GuestId,
        Guid HomeId,
        BookingStatus Status,
        DateTime Start,
        DateTime End,
        int NumberOfGuests,
        double Price);

    public record UpdateBookingRequest(
        BookingStatus Status,
        DateTime Start,
        DateTime End,
        int NumberOfGuests,
        double Price);
}