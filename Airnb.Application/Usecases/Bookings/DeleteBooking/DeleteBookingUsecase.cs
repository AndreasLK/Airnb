using Airnb.Application.Repository.Interfaces;

namespace Airnb.Application.Usecases.Bookings.DeleteBooking
{
    public class DeleteBookingUsecase : IDeleteBookingUsecase
    {
        public readonly IBookingRepository _bookingRepository;

        public DeleteBookingUsecase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task ExecuteAsync(Guid bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId, cancellationToken);
            if (booking == null)
            {
                throw new Exception($"Booking with ID {bookingId} not found.");
            }
            _bookingRepository.Delete(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }

    }
}
