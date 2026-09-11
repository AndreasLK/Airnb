using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public record Permissions(
        Guid UserId,
        PermissionStatus PermissionStatus);
}
