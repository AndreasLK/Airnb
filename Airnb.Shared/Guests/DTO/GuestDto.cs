

using Airnb.Shared.Common.DTO;

namespace Airnb.Shared.Guests.DTO
{
    public record GuestDto(Guid Id, Guid UserId, AddressDto Address);
}
