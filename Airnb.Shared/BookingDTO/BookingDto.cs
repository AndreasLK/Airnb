using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.BookingDTO
{
    public record BookingDto(
    Guid Id, 
    Guid GuestId, 
    Guid HomeId,
    string Status,
    DateTime Start, 
    DateTime End,
    int NumberOfGuests,
    decimal Amount, 
    string Currency);

}
