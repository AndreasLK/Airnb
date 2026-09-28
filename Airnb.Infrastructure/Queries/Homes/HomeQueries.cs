using Airnb.Domain.Entities;
using Airnb.Shared.Homes;
using Microsoft.EntityFrameworkCore;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;

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
                    h.HostId,
                    h.Capacity,
                    h.Address.City,
                    h.HomeType.ToString(),
                    h.PricePerDay.Amount,
                    h.PricePerDay.Currency))
                .ToListAsync(cancellationToken);
        }
    }
}
