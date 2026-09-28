using Airnb.Shared.Bookings.Requests.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Bookings.UpdateBooking
{
    public interface IUpdateBookingUsecase
    {
        Task ExecuteAsync(Guid bookingId, UpdateBookingRequest request, CancellationToken cancellationToken = default);
    }
}
