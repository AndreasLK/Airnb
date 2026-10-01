using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Repository.Interfaces
{
    public interface IHostRepository
    {
        Task<HostProfile> GetByIdAsync(Guid hostId, CancellationToken cancellationToken = default);
        Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(HostProfile host, CancellationToken cancellationToken = default);
        void Delete(HostProfile host);
        void Update(HostProfile host);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
