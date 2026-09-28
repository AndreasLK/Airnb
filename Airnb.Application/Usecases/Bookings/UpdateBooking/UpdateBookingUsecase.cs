using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Bookings.Request.Bookings;


namespace Airnb.Application.Usecases.Bookings.UpdateBooking
{
    public class UpdateBookingUsecase : IUpdateBookingUsecase
    {
        private readonly IBookingRepository _bookingRepository;

        public async Task ExecuteAsync(Guid bookingId, UpdateBookingRequest request, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken)
                ?? throw new KeyNotFoundException("Booking not found");

            if (!Enum.TryParse<BookingStatus>(request.Status, ignoreCase: true, out var status))
                throw new ArgumentException($"Ugyldig status: {request.Status}");

            booking.UpdateDetails(
                status,
                new TimeRange(request.Start, request.End),
                request.NumberOfGuests,
                (double)request.Amount,
                new Reciept(new Money(request.Amount, request.Currency), request.Service));

            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
