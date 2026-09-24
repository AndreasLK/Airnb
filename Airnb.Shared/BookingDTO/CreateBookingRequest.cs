using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.BookingDTO
{
    public record CreateBookingRequest(
       Guid GuestId,
       Guid HomeId,
       DateTime Start,
       DateTime End,
       int NumberOfGuests,
       decimal Amount,
       string Currency,
       string Service);
}
