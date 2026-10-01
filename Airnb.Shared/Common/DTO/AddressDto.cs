using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Common.DTO
{
    public record AddressDto(
    string Country,
    string PostalCode,
    string City,
    string StreetName,
    string HouseNumber,
    string? Floor);
}
