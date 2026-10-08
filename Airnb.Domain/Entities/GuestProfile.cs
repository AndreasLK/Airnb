using Airnb.Domain.ValueObjects;
using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class GuestProfile : AggregateRoot
    {
        private GuestProfile() { }

        public Guid UserId { get; private set; }

        public Address Address { get; private set; } = null!;

        public GuestProfile(Guid userId, Address address)
        {
            UserId = userId;
            Address = address;
        }
        public static GuestProfile Create(Guid userId, Address address)
        {
            return new GuestProfile(userId, address);
        }
        public void UpdateAddress(Address address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

    }
}
