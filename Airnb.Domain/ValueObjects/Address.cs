using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.ValueObjects
{
    public class Address
    {
        public string Country { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string StreetName { get; private set; } = null!;
        public string HouseNumber { get; private set; } = null!;
        public string? Floor { get; private set; }

        private Address() { } // EF Core

        public Address(string country, string postalCode, string city, string streetName, string houseNumber, string? floor = null)
        {
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Land mangler.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("By mangler.");
            if (string.IsNullOrWhiteSpace(streetName)) throw new ArgumentException("Gadenavn mangler.");
            if (string.IsNullOrWhiteSpace(houseNumber)) throw new ArgumentException("Husnummer mangler.");

            Country = country;
            PostalCode = postalCode;
            City = city;
            StreetName = streetName;
            HouseNumber = houseNumber;
            Floor = floor;
        }
    }
}
