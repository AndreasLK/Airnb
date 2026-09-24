using Airnb.Domain.Enums;
using Airnb.Domain.Common;
using Airnb.Domain.ValueObjects;

namespace Airnb.Domain.Entities
{
    public class Home : AggregateRoot
    {

        public Guid HostId { get; private set; }

        public int Capacity { get; private set; }

        public Address Address { get; private set; }

        public DateTime CheckInTime { get; private set; }

        public DateTime CheckOutTime { get; private set; }

        public HomeType HomeType { get; private set; }

        public Money PricePerDay { get; private set; }

        public Guid HomeRulesId { get; private set; }

        public List<DateOnly> AvailableDates { get; private set; }

        public HomeFeatures HomeFeatures { get; private set; }

        public Home() { }

        public Home(Guid hostId,
            int capacity,
            Address address,
            DateTime checkInTime,
            DateTime checkOutTime,
            HomeType homeType,
            Money pricePerDay,
            Guid homeRulesId,
            List<DateOnly> dateOnlies,
            HomeFeatures homeFeatures)
        {
            HostId = hostId;
            Capacity = capacity;
            Address = address;
            CheckInTime = checkInTime;
            CheckOutTime = checkOutTime;
            HomeType = homeType;
            PricePerDay = pricePerDay;
            HomeRulesId = homeRulesId;
            AvailableDates = dateOnlies;
            HomeFeatures = homeFeatures;
        }
    }
}
