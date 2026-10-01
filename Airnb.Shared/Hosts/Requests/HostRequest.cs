using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Hosts.Requests
{
    public record HostRequest(Guid UserId, Guid PermissionsId);

}
