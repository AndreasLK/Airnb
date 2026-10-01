using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Homes.Requests
{
    public record UpdateHomeRequest(
     Guid HostId,
     int Capacity,
     string Country,
     string PostalCode,
     string City,
     string StreetName,
     string HouseNumber,
     string? Floor,
     DateTime CheckInTime,
     DateTime CheckOutTime,
     string HomeType,
     decimal PricePerDay,
     bool PetsAllowed,
     bool SmokingAllowed,
     bool PartiesAllowed,
     string Currency);
}
