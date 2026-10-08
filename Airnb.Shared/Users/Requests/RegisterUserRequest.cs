using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Users.Requests
{
    public record RegisterUserRequest(
    string Email,
    string Name,
    string Password,
    string Country,
    string PostalCode,
    string City,
    string StreetName,
    string HouseNumber,
    string? Floor);
}
