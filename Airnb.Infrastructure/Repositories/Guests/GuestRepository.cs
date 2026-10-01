using Airnb.Domain.Entities;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Airnb.Application.Repository.Interfaces;

namespace Airnb.Infrastructure.Repositories.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AirnbDbContext _context;

        public GuestRepository(AirnbDbContext context)
        {
            _context = context;
        }

        public async Task<GuestProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Guests
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(GuestProfile guest, CancellationToken cancellationToken = default)
        {
            
            await _context.Guests.AddAsync(guest, cancellationToken);
        }

        public void Update(GuestProfile guest)
        {
            
            _context.Guests.Update(guest);
        }

        public void Delete(GuestProfile guest)
        { 
            _context.Guests.Remove(guest);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }


    }
}
