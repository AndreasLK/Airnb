using Airnb.Domain.Entities;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Shared.Common.DTO;
using Airnb.Shared.Guests.DTO;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Airnb.Infrastructure.Queries.Guests
{
    public class GuestQueries : IGuestQueries
    {
        private readonly AirnbDbContext _dbContext;

        public GuestQueries(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<GuestDto>> ListGuestsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Guests
                .Select(g => new GuestDto(
                    g.Id,
                    g.UserId,
                    new AddressDto(
                        g.Address.Country,
                        g.Address.PostalCode,
                        g.Address.City,
                        g.Address.StreetName,
                        g.Address.HouseNumber,
                        g.Address.Floor)))
                .ToListAsync(cancellationToken);
        }
    }
}
