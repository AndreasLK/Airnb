using Airnb.Shared.Users.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Users.RegisterUser
{
    public interface IRegisterUserUsecase
    {
        Task<Guid> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
    }
}
