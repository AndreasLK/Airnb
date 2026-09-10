using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public record Address
    {
        public string Country { get; private set; }

        public string PostalCode { get; private set; }

        public string City { get; private set; }

        public string StreetName { get; private set; }

        public string HouseNumber { get; private set; }

        public string Floor { get; private set; }
    }
}
