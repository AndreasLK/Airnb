using Airnb.Shared.Hosts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.Queries.Hosts
{
    public interface IHostQueries
    {
        Task<IReadOnlyList<HostDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<HostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<HostDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    }
}
