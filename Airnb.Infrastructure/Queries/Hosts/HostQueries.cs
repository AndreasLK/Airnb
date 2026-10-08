using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Airnb.Shared.Hosts.DTO;



namespace Airnb.Infrastructure.Queries.Hosts
{
    public class HostQueries : IHostQueries
    {
        private readonly AirnbDbContext _dbContext;

        public HostQueries(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<HostDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Hosts
                .AsNoTracking()
                .Select(h => new HostDto(h.Id, h.UserId))
                .ToListAsync(cancellationToken);
        }

        public async Task<HostDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Hosts
                .AsNoTracking()
                .Where(h => h.Id == id)
                .Select(h => new HostDto(h.Id, h.UserId))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<HostDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Hosts
                .AsNoTracking()
                .Where(h => h.UserId == userId)
                .Select(h => new HostDto(h.Id, h.UserId))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
