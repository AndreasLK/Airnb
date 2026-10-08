using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Shared.Auth.DTO
{
    public record LoginResponse(
        Guid UserId,
        string AccessToken,
        DateTime AccessTokenExpiresAt,
        string RefreshToken);
}
