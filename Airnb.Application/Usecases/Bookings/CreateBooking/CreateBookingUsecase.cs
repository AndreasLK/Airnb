using Airnb.Application.Repository.Interfaces;
using Airnb.Shared.Bookings.DTO;
using Airnb.Domain.Entities;
using Airnb.Domain.Enums;
using Airnb.Domain.ValueObjects;
using Airnb.Shared.Bookings.Request.Bookings;

namespace Airnb.Application.Usecases.Bookings.CreateBooking
{
    public class CreateBookingUsecase : ICreateBookingUsecase
    {
        
        private readonly IBookingRepository _bookingRepository;

        public CreateBookingUsecase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> ExecuteAsync(CreateBookingRequest request, CancellationToken cancellationToken = default)
        {
            var booking = Booking.Create(   //gode gamle factorymetoden for at lave en booking. Factorymetoden sørger for at alle regler bliver overholdt, og at objektet er i en valid tilstand når det bliver lavet.
                request.GuestId,
                request.HomeId,
                BookingStatus.Pending,
                new TimeRange(request.Start, request.End),
                DateTime.UtcNow,
                request.NumberOfGuests,
                (double)request.Amount,
                new Reciept(new Money(request.Amount, request.Currency), request.Service));

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return new BookingDto(
                booking.Id,
                booking.GuestId,
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
