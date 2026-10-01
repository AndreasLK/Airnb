using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Usecases.Hosts.CreateHost
{
    public interface IBecomeHostUsecase
    {
        Task<Guid> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
