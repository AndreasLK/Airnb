using Airnb.Shared.Homes.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.Queries.Homes
{
    public interface IHomeQueries
    {
        Task<IReadOnlyList<HomeDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<HomeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
