using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.Entities;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Airnb.Infrastructure.Repositories.RefreshTokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AirnbDbContext _dbContext;

        public RefreshTokenRepository(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);
        }

        public async Task DeleteInactiveForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            await _dbContext.RefreshTokens
                .Where(t => t.UserId == userId && (t.RevokedAt != null || t.ExpiresAt < now))
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
