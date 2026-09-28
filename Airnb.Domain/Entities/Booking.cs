using System;
using System.Collections.Generic;
using System.Text;
using Airnb.Domain.Common;
using Airnb.Domain.Enums;
using Airnb.Domain.Exceptions;
using Airnb.Domain.ValueObjects;

namespace Airnb.Domain.Entities
{
    public class Booking : AggregateRoot
    {
        public Guid GuestId { get; private set; }
        public Guid HomeId { get; private set; }
        public BookingStatus Status { get; private set; }
        public TimeRange TimeRange { get; private set; }
        public DateTime CreatedTime { get; private set; }
        public int NumberOfGuests { get; private set; }
        public double Price { get; private set; }
        public Reciept Reciept { get; private set; }

        private Booking() { } // EF Core needs this

        private Booking(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdTime,
            int numberOfGuests,
            double price,
            Reciept reciept)
        {
            this.GuestId = guestId;
            this.HomeId = homeId;
            this.Status = status;
            this.TimeRange = timeRange;
            this.CreatedTime = createdTime;
            this.NumberOfGuests = numberOfGuests;
            this.Price = price;
            this.Reciept = reciept;
            this.Validate();
        }

        public static Booking Create(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdTime,
            int numberOfGuests,
            double price,
            Reciept reciept)
        {
            return new Booking(guestId, homeId, status, timeRange, createdTime, numberOfGuests, price, reciept);
        }

        public void Validate()
        {
            if (GuestId == Guid.Empty)
                throw new DomainException("GuestId cannot be empty.");
            if (HomeId == Guid.Empty)
                throw new DomainException("HomeId cannot be empty.");
            if (NumberOfGuests <= 0)
                throw new DomainException("NumberOfGuests must be greater than zero.");
            if (Price < 0)
                throw new DomainException("Price cannot be negative.");
            if (Reciept == null)
                throw new DomainException("Reciept cannot be null.");
        }

        public void UpdateDetails(BookingStatus status, TimeRange timeRange, int numberOfGuests, double price, Reciept reciept)
        {
            Status = status;
            TimeRange = timeRange;
            NumberOfGuests = numberOfGuests;
            Price = price;
            Reciept = reciept;
            Validate();
        }

        public void Update(DateTime start, DateTime end, BookingStatus status)
        {
            TimeRange = new TimeRange(start, end);
            Status = status;
            Validate();
        }
    }
}
