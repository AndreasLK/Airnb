using Airnb.Domain.Enums;
using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class HomeRating : AggregateRoot
    {
        public Guid BookingId { get; private set; }

        public Guid GuestId { get; private set; }

        public Guid HomeId { get; private set; }

        public string Review { get; private set; }

        public StarRating StarRating { get; private set; }

        public HomeRating() { }

        public HomeRating(Guid bookingId, Guid guestId, Guid homeId, string review, StarRating starRating)
        {
            BookingId = bookingId;
            GuestId = guestId;
            HomeId = homeId;
            Review = review;
            StarRating = starRating;
        }
    }
}
