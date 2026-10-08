using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default);

        Task DeleteInactiveForUserAsync(Guid userId, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}