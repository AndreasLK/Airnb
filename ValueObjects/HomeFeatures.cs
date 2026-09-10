using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public record HomeFeatures
    {
        public bool SwimmingPool { get; set; }

        public bool HotTub {  get; set; }

        public bool DryingCloset { get; set; }

        public bool Sauna { get; set; }
    }
}
