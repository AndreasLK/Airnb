using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.DomainServices;
using Airnb.Domain.Entities;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Bookings.DTO;
using Airnb.Shared.Bookings.Requests;
using Airnb.Application.Exceptions;

namespace Airnb.Application.Usecases.Bookings.CreateBooking
{
    public class CreateBookingUsecase : ICreateBookingUsecase
    {
        
        private readonly IBookingRepository _bookingRepository;
        private readonly IHomeRepository _homeRepository;
        private readonly IBookingConflictChecker _conflictChecker;

        public CreateBookingUsecase(IBookingRepository bookingRepository, IHomeRepository homeRepository, IBookingConflictChecker bookingConflictChecker)
        {
            _bookingRepository = bookingRepository;
            _homeRepository = homeRepository;
            _conflictChecker = bookingConflictChecker;
        }

        public async Task<BookingDto> ExecuteAsync(CreateBookingRequest request, CancellationToken cancellationToken = default)
        {
            var home = await _homeRepository.GetByIdAsync(request.HomeId, cancellationToken)
            ?? throw new NotFoundException($"Hus med ID {request.HomeId} blev ikke fundet.");

            var existingBookings = await _bookingRepository.GetByHomeIdAsync(request.HomeId, cancellationToken); //tjekker for eksisterende bookinger for det givne hjem, så vi kan tjekke for overlap.

            var timeRange = new TimeRange(request.Start, request.End);
            _conflictChecker.ValidateWithoutOverlapping(timeRange, request.NumberOfGuests, home, existingBookings);

            var reciept = Reciept.Create(home.PricePerDay, timeRange, request.Service);

            var booking = Booking.Create(   //gode gamle factorymetoden for at lave en booking. Factorymetoden sørger for at alle regler bliver overholdt, og at objektet er i en valid tilstand når det bliver lavet.
                request.GuestId,
                request.HomeId,
                BookingStatus.Pending,
                timeRange,                   //det der checker om den existerende booking er i orden?
                DateTime.UtcNow,
                request.NumberOfGuests,
                reciept);

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return new BookingDto(
                booking.Id,
                booking.GuestProfileId,
                booking.HomeId,
                booking.Status.ToString(),
                booking.TimeRange.Start,
                booking.TimeRange.End,
                booking.NumberOfGuests,
                booking.Reciept.StartPrice.Amount,
                booking.Reciept.StartPrice.Currency);
        }
    }
}
