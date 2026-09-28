using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Homes.DTO
{
    public record HomeDto(
    Guid Id,
    Guid HostId,
    int Capacity,
    string City,
    string HomeType,
    decimal PricePerDay,
    string Currency);
}
