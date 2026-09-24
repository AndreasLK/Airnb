using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.BookingDTO
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
