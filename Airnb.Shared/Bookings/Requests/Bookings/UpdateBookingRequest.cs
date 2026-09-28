using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Bookings.Requests.Bookings
{
    public record UpdateBookingRequest(
        string Status,
        DateTime Start,
        DateTime End,
        int NumberOfGuests,
        decimal Amount,
        string Currency,
        string Service);
}
