using System;
using System.Collections.Generic;
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
            createdDate = createdDate;
            numberOfGuests = NumberOfGuests;
            price = Price;
            reciept = Reciept;
        }

    }
}
