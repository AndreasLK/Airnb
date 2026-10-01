using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Guests.Request
{
    public record CreateGuestRequest(
        Guid UserId,
        string Country,
        string PostalCode,
        string City,
        string StreetName,
        string HouseNumber,
        string? Floor);
}
