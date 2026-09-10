using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public record TimeRange
    {
        public DateTime Start {  get; private set; }
        public DateTime End { get; private set; }

        public TimeRange(DateTime start, DateTime end)
        {
            start = Start;
            end = End;
        }

        public void ValidationForOverLapping(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new DomainException("lad lige vær med det en gang din numse prut!");
            }
            start = Start;
        }
    }
}
