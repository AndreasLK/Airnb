using Airnb.Domain.Enums;
using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class HomeRating : AggregateRoot
    {
        public Guid BookingId { get; private set; }

        public Guid GuestProfileId { get; private set; }

        public Guid HomeId { get; private set; }

        public string Review { get; private set; } = null!;

        public StarRating StarRating { get; private set; }

        private HomeRating() { }

        public HomeRating(Guid bookingId, Guid guestId, Guid homeId, string review, StarRating starRating)
        {
            BookingId = bookingId;
            GuestProfileId = guestId;
            HomeId = homeId;
            Review = review;
            StarRating = starRating;
        }
    }
}
