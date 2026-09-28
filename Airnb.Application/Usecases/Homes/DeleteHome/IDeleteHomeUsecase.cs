using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Homes.DeleteHome
{
    public interface IDeleteHomeUsecase
    {
        Task ExecuteAsync(Guid homeId, CancellationToken cancellationToken = default);
    }
}
