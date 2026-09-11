using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainServices
{
    public interface IBookingConflictChecker
    {
        public void ValidateWithoutOverlapping(
            TimeRange timeRange,
            int numberOfGuests,
            Home home,
            IEnumerable<Booking> existingBookings);
    } 
}
        

