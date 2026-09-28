using Airnb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.ValueObjects
{
    public record TimeRange
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        private TimeRange() { }
        public TimeRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
          
            ValidateEndAfterStart();
        }

        public bool OverlapsWith(TimeRange other)
        {
            return Start < other.End && End > other.Start;
        }
        private void ValidateEndAfterStart()
        {
            if (End <= Start)
            {
                throw new DomainException("Slutdatoen skal ligge efter startdatoen.");
            }
        }
        public void ValidateNotInPast()
        {
            if (Start < DateTime.UtcNow)
                throw new DomainException("Starttiden kan ikke være i fortiden.");
        }
        
    }
}
