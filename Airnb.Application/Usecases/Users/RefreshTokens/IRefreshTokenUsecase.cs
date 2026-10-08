using Airnb.Shared.Auth.DTO;
using Airnb.Shared.Auth.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Users.RefreshTokens
{
    public interface IRefreshTokenUsecase
    {
        Task<LoginResponse> ExecuteAsync(RefreshRequest request, CancellationToken cancellationToken = default);
    }
}
