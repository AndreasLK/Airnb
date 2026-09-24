using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Airnb.Domain.Common;
using Airnb.Domain.Enums;

namespace Airnb.Domain.Entities
{
    public class Permission : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public PermissionStatus PermissionStatus { get; private set; }

        public Permission() { }
        public Permission(Guid userId, PermissionStatus permissionStatus)
        {
            userId = UserId;
            permissionStatus = PermissionStatus;
        }
    }
}
