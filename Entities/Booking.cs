using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
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

        public Booking() { }  

        public Booking(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdDate,
            int numberOfGuests,
            double price,
            Reciept recipt)
        {
            guestId = GuestId;
            homeId = HomeId;
            status = Status;
            timeRange = TimeRange;
            createdDate = CreatedDate;
            numberOfGuests = NumberOfGuests;
            price = Price;
            reciept = Reciept;
            Validate();
        }
        public static Booking Create(Guid guestId,
            Guid homeId,
            BookingStatus status,
            TimeRange timeRange,
            DateTime createdDate,
            int numberOfGuests,
            double price,
            Reciept recipt)
        {
            return new Booking(guestId, homeId, status, timeRange, createdDate, numberOfGuests, price, recipt);
        }

        public void Validate()
        {
            if (GuestId == Guid.Empty)
                throw new ArgumentException("GuestId cannot be empty.");
            if (HomeId == Guid.Empty)
                throw new ArgumentException("HomeId cannot be empty.");
            if (NumberOfGuests <= 0)
                throw new ArgumentException("NumberOfGuests must be greater than zero.");
            if (Price < 0)
                throw new ArgumentException("Price cannot be negative.");
            if (Reciept == null)
                throw new ArgumentException("Reciept cannot be null.");
        }
    }
}
