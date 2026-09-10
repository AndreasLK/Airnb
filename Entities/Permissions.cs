using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Permissions : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public PermissionStatus PermissionStatus { get; private set; }

        public Permissions() { }
        public Permissions(Guid userId, PermissionStatus permissionStatus)
        {
            userId = UserId;
            permissionStatus = PermissionStatus;
        }
    }
}
