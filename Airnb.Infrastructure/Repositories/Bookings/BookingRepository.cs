using Airnb.Application.Repository.Interfaces;
using Airnb.Domain.Entities;
using Airnb.Domain.Enums;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Airnb.Infrastructure.Repositories.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AirnbDbContext _context;
        public BookingRepository(AirnbDbContext context)
        {
            _context = context;
        }
        public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            await _context.Bookings.AddAsync(booking, cancellationToken);
        }

        public void Update(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            _context.Bookings.Update(booking);
        }

        public void Delete(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            _context.Bookings.Remove(booking);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .AnyAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<List<Booking>> GetByHomeIdAsync(Guid homeId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Where(b => b.HomeId == homeId
                 && b.Status != BookingStatus.Cancelled
                 && b.Status != BookingStatus.CheckedOut)
                .ToListAsync(cancellationToken);
        }
    }
}
