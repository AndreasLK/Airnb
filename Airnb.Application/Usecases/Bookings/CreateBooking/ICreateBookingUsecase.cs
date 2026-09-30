using Airnb.Shared.Bookings.DTO;
using Airnb.Shared.Bookings.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Bookings.CreateBooking
{
    public interface ICreateBookingUsecase
    {
        Task<BookingDto> ExecuteAsync(CreateBookingRequest request, CancellationToken cancellationToken = default);
    }
}
