using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.ValueObjects
{
    public record Money(
        decimal Amount,
        string Currency);
}
