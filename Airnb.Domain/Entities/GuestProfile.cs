using Airnb.Domain.ValueObjects;
using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class GuestProfile : AggregateRoot
    {
        public GuestProfile() { }

        public Guid UserId { get; private set; }

        public Address Address { get; private set; }

        public GuestProfile(Guid userId, Address address)
        {
            UserId = userId;
            Address = address;
        }

    }
}
