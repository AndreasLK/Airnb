using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public record TimeRange
    {
        public DateTime Start {  get; private set; }
        public DateTime End { get; private set; }

        public TimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            //ValidateOverlapping();
            ValidateNotInPast();
        }

        public bool OverlapsWith(TimeRange other)
        {
            return Start < other.End && End > other.Start;
        }
        public void ValidateNotInPast()
        {
            if (Start < DateTime.Now)
            {
                throw new DomainException("Du kan ikke booke i fortiden!");
            }
        }
    }
}
