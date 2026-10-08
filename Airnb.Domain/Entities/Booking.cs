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
        public Guid GuestProfileId { get; private set; }
        public Guid HomeId { get; private set; }
        public BookingStatus Status { get; private set; }
        public TimeRange TimeRange { get; private set; } = null!;
        public DateTime CreatedTime { get; private set; }
        public int NumberOfGuests { get; private set; }
        public Reciept Reciept { get; private set; } = null!;

        private Booking() { } // EF Core needs this

        private Booking(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdTime,
            int numberOfGuests,
            Reciept reciept)
        {
            this.GuestProfileId = guestId;
            this.HomeId = homeId;
            this.Status = status;
            this.TimeRange = timeRange;
            this.CreatedTime = createdTime;
            this.NumberOfGuests = numberOfGuests;
            this.Reciept = reciept;
            this.Validate();
            
        }

        public static Booking Create(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdTime,
            int numberOfGuests,
            Reciept reciept
            )
        {
            timeRange.ValidateNotInPast();
            return new Booking(guestId, homeId, status, timeRange, createdTime, numberOfGuests, reciept);
        }
        
        public void Validate()
        {
            if (GuestProfileId == Guid.Empty)
                throw new DomainException("GuestId cannot be empty.");
            if (HomeId == Guid.Empty)
                throw new DomainException("HomeId cannot be empty.");
            if (NumberOfGuests <= 0)
                throw new DomainException("NumberOfGuests must be greater than zero.");
            if (Reciept == null)
                throw new DomainException("Reciept cannot be null.");
            if (Reciept.StartPrice.Amount < 0)
                throw new DomainException("Price cannot be negative.");
        }

        public void UpdateDetails(BookingStatus status, TimeRange timeRange, int numberOfGuests, Reciept reciept)
        {
            if (timeRange != TimeRange)
                timeRange.ValidateNotInPast();

            Status = status;
            TimeRange = timeRange;
            NumberOfGuests = numberOfGuests;
            Reciept = reciept;
            Validate();
        }

       
    }
}
