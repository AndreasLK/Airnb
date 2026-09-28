using Airnb.Application.Repository.Interfaces;

namespace Airnb.Application.Usecases.Bookings.DeleteBooking
{
    public class DeleteBookingUsecase : IDeleteBookingUsecase
    {
        private readonly IBookingRepository _bookingRepository;

        public DeleteBookingUsecase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task ExecuteAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken)
                ?? throw new KeyNotFoundException($"Booking med ID {bookingId} blev ikke fundet.");

            _bookingRepository.Delete(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
