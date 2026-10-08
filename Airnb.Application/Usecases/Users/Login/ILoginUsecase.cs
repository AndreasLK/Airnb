using Airnb.Shared.Auth.DTO;
using Airnb.Shared.Auth.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Users.Login
{
    public interface ILoginUsecase
    {
        Task<LoginResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
