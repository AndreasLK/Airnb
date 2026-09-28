using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Domain.Entities;
using Airnb.Application.Repository.Interfaces;

namespace Airnb.Infrastructure.Repositories.Homes
{
    public class HomeRepository : IHomeRepository
    {
        private readonly AirnbDbContext _dbContext;

        public HomeRepository(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Home home, CancellationToken cancellationToken = default)
        {
            await _dbContext.Homes.AddAsync(home, cancellationToken);
        }

        public async Task<Home?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Homes.FindAsync(new object[] { id }, cancellationToken);
        }

        public void Update(Home home)
        {
            _dbContext.Homes.Update(home);
        }

        public void Delete(Home home)
        {
            _dbContext.Homes.Remove(home);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
