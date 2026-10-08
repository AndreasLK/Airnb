using System;
using System.Collections.Generic;
using System.Text;
using Airnb.Domain.Common;
using Airnb.Domain.Enums;

namespace Airnb.Domain.Entities
{
    public class HostProfile : AggregateRoot
    {
        private HostProfile() { }

        public Guid UserId { get; private set; }
        public HostRole Role { get; private set; }

        private HostProfile(Guid userId, HostRole role)
        {
            UserId = userId;
            Role = role;
        }
        

        public static HostProfile Create(Guid userId)
            => new HostProfile(userId, HostRole.Admin);
        

        public void UpdateRole(HostRole role)
        {
            Role = role;
        }



    }
}
