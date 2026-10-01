using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Hosts.DeleteHost
{
    public interface IDeleteHostUsecase
    {
        Task ExecuteAsync(Guid hostId, CancellationToken cancellationToken = default);
    }
}
