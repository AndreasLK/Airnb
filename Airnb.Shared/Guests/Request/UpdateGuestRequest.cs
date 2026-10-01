

namespace Airnb.Shared.Guests.Request
{
    public record UpdateGuestRequest(
        string Country,
        string PostalCode,
        string City,
        string StreetName,
        string HouseNumber,
        string Floor);
    
}
