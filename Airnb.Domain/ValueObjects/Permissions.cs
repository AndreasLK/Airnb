using Airnb.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.ValueObjects
{
    public record Permissions(
        Guid UserId,
        PermissionStatus PermissionStatus);
}
