using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Airnb.Shared.Auth.Requests
{
    public record RefreshRequest([Required] string RefreshToken);
}
