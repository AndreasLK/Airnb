using Airnb.Shared.Homes.DTO;
using Airnb.Shared.Homes.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Homes.CreateHome
{
    public interface ICreateHomeUsecase
    {
        Task<HomeDto> ExecuteAsync(CreateHomeRequest request, CancellationToken cancellationToken = default);
    }
}
