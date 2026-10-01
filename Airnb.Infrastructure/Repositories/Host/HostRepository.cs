using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Airnb.Application.Repository.Interfaces;

namespace Airnb.Infrastructure.Repositories.Host
{
    public class HostRepository : IHostRepository
    {
        private readonly AirnbDbContext _dbContext;

        public HostRepository(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HostProfile?> GetByIdAsync(Guid hostId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Hosts.FindAsync(new object[] { hostId }, cancellationToken);
        }

        public async Task<bool> ExistsForUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Hosts.AnyAsync(h => h.UserId == userId, cancellationToken);
        }

        public async Task AddAsync(HostProfile host, CancellationToken cancellationToken = default)
        {
            await _dbContext.Hosts.AddAsync(host, cancellationToken);
        }

        public void Delete(HostProfile host)
        {
            _dbContext.Hosts.Remove(host);
        }

        public void Update(HostProfile host)
        {
            _dbContext.Hosts.Update(host);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }



    }
}
