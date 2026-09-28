using Airnb.Domain.Enums;
using Airnb.Domain.Common;
using Airnb.Domain.ValueObjects;
using Airnb.Domain.Exceptions;

namespace Airnb.Domain.Entities
{
        public class Home : AggregateRoot
        {
            public Guid HostId { get; private set; }
            public int Capacity { get; private set; }
            public Address Address { get; private set; } = null!;
            public DateTime CheckInTime { get; private set; }
            public DateTime CheckOutTime { get; private set; }
            public HomeType HomeType { get; private set; }
            public Money PricePerDay { get; private set; } = null!;
            public HomeRules HomeRules { get; private set; } = null!;
            public List<DateOnly> AvailableDates { get; private set; } = new();
            public HomeFeatures? HomeFeatures { get; private set; }

            private Home() { } // EF Core

            private Home(
                Guid hostId,
                int capacity,
                Address address,
                DateTime checkInTime,
                DateTime checkOutTime,
                HomeType homeType,
                Money pricePerDay,
                HomeRules homeRules,
                List<DateOnly> availableDates,
                HomeFeatures? homeFeatures)
            {
                HostId = hostId;
                Capacity = capacity;
                Address = address;
                CheckInTime = checkInTime;
                CheckOutTime = checkOutTime;
                HomeType = homeType;
                PricePerDay = pricePerDay;
                HomeRules = homeRules;
                AvailableDates = availableDates;
                HomeFeatures = homeFeatures;
                Validate();
            }

            public static Home Create(
                Guid hostId,
                int capacity,
                Address address,
                DateTime checkInTime,
                DateTime checkOutTime,
                HomeType homeType,
                Money pricePerDay,
                HomeRules homeRules,
                List<DateOnly>? availableDates = null,
                HomeFeatures? homeFeatures = null)
            {
                return new Home(
                    hostId, capacity, address, checkInTime, checkOutTime,
                    homeType, pricePerDay, homeRules,
                    availableDates ?? new List<DateOnly>(),
                    homeFeatures);
            }

            public void UpdateDetails(
                int capacity,
                Address address,
                DateTime checkInTime,
                DateTime checkOutTime,
                HomeType homeType,
                Money pricePerDay,
                HomeRules homeRules,
                List<DateOnly>? availableDates = null,
                HomeFeatures? homeFeatures = null)
            {
                Capacity = capacity;
                Address = address;
                CheckInTime = checkInTime;
                CheckOutTime = checkOutTime;
                HomeType = homeType;
                PricePerDay = pricePerDay;
                HomeRules = homeRules;
                AvailableDates = availableDates ?? new List<DateOnly>();
                HomeFeatures = homeFeatures;
                Validate();
            }

            private void Validate()
            {
                if (HostId == Guid.Empty)
                    throw new DomainException("HostId cannot be empty.");
                if (Capacity <= 0)
                    throw new DomainException("Capacity must be greater than zero.");
                if (Address is null)
                    throw new DomainException("Address cannot be null.");
                if (PricePerDay is null || PricePerDay.Amount < 0)
                    throw new DomainException("PricePerDay cannot be null or negative.");
                if (HomeRules is null)
                    throw new DomainException("HomeRules cannot be null.");
            }

        public Money CalculatePrice(TimeRange timeRange)
        {
            var nights = (timeRange.End.Date - timeRange.Start.Date).Days;

            if (nights < 1)
                throw new DomainException("En booking skal være mindst én nat.");

            return new Money(PricePerDay.Amount * nights, PricePerDay.Currency);
        }
    }
    }

