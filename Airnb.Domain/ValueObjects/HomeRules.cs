using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.ValueObjects
{
    public class HomeRules
    {
        public bool PetsAllowed { get; private set; }
        public bool SmokingAllowed { get; private set; }
        public bool PartiesAllowed { get; private set; }
        public int MaxGuests { get; private set; }

        private HomeRules() { } // EF Core

        public HomeRules(bool petsAllowed, bool smokingAllowed, bool partiesAllowed, int maxGuests)
        {
            if (maxGuests <= 0) throw new ArgumentException("MaxGuests skal være mindst 1.");

            PetsAllowed = petsAllowed;
            SmokingAllowed = smokingAllowed;
            PartiesAllowed = partiesAllowed;
            MaxGuests = maxGuests;
        }
    }
}
