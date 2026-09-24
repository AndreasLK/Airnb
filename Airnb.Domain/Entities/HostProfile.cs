using System;
using System.Collections.Generic;
using System.Text;
using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class HostProfile : AggregateRoot
    {
        public HostProfile() { }

        public Guid UserId { get; private set; }

        public Guid PermissionsId { get; private set; }

        public HostProfile(Guid userId, Guid permissionsId)
        {
            userId = UserId;
            permissionsId = PermissionsId;
        }

    }
}
