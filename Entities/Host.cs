using Domain.Common;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Domain.Entities
{
    public class Host : AggregateRoot
    {
        public Host() { }

        public Guid UserId { get; private set; }

        public Guid PermissionsId { get; private set; }

        public Host(Guid userId, Guid permissionsId)
        {
            userId = UserId;
            permissionsId = PermissionsId;
        }

    }
}
