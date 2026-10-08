using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Shared.Homes.DTO;

namespace Airnb.Infrastructure.Queries.Homes
{
    public class HomeQueries : IHomeQueries
    {

        private readonly AirnbDbContext _dbContext;
        public HomeQueries(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<HomeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Homes
                .AsNoTracking()
                .Select(h => new HomeDto(
                    h.Id,
                    h.HostProfileId,
                    h.Capacity,
                    h.Address.City,
                    h.HomeType.ToString(),
                    h.PricePerDay.Amount,
                    h.PricePerDay.Currency))
                .ToListAsync(cancellationToken);
        }

        public async Task<HomeDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Homes
                .AsNoTracking()
                .Where(h => h.Id == id)
                .Select(h => new HomeDto(
                    h.Id,
                    h.HostProfileId,
                    h.Capacity,
                    h.Address.City,
                    h.HomeType.ToString(),
                    h.PricePerDay.Amount,
                    h.PricePerDay.Currency))
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
