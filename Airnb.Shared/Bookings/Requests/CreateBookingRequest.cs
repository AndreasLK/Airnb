using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Bookings.Requests
{
    public record CreateBookingRequest(
       Guid GuestId,
       Guid HomeId,
       DateTime Start,
       DateTime End,
       int NumberOfGuests,
       string Service);
}
