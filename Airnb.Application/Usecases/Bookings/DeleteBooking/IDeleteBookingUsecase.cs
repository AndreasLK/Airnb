using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Bookings.DeleteBooking
{
    public interface IDeleteBookingUsecase
    {
        Task ExecuteAsync(Guid bookingId, CancellationToken cancellationToken = default);
    }
}
