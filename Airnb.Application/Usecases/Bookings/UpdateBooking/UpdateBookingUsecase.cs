using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.DomainServices;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Bookings.Requests.Bookings;


namespace Airnb.Application.Usecases.Bookings.UpdateBooking
{
    public class UpdateBookingUsecase : IUpdateBookingUsecase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly IBookingConflictChecker _conflictChecker;

        public UpdateBookingUsecase(IBookingRepository bookingRepository, IHomeRepository homeRepository, IBookingConflictChecker bookingConflictChecker)
        {
            _bookingRepository = bookingRepository;
            _homeRepository = homeRepository;
            _conflictChecker = bookingConflictChecker;
        }
        public async Task ExecuteAsync(Guid bookingId, UpdateBookingRequest request, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken)
                ?? throw new KeyNotFoundException("Booking not found");

            if (!Enum.TryParse<BookingStatus>(request.Status, ignoreCase: true, out var status))
                throw new ArgumentException($"Ugyldig status: {request.Status}");
            
            var home = await _homeRepository.GetByIdAsync(booking.HomeId, cancellationToken)
            ?? throw new KeyNotFoundException($"Hus med ID {booking.HomeId} blev ikke fundet.");

            var otherBookings = (await _bookingRepository.GetByHomeIdAsync(booking.HomeId, cancellationToken))
            .Where(b => b.Id != booking.Id)
            .ToList();

            var timeRange = new TimeRange(request.Start, request.End);
            _conflictChecker.ValidateWithoutOverlapping(timeRange, request.NumberOfGuests, home, otherBookings);

            booking.UpdateDetails(
                status,
                timeRange,
                request.NumberOfGuests,
                new Reciept(new Money(request.Amount, request.Currency), request.Service));

            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }

    }
        
}

