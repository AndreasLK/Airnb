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

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Booking>> GetByHomeIdAsync(Guid homeId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Where(b => b.HomeId == homeId
                 && b.Status != BookingStatus.Cancelled
                 && b.Status != BookingStatus.CheckedOut)
                .ToListAsync(cancellationToken);
        }
        public async Task<bool> ExistsForHomeAsync(Guid homeId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings.AnyAsync(b => b.HomeId == homeId, cancellationToken);
        }

        public async Task<bool> HasUpcomingForHostAsync(Guid hostId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var blockingStatuses = new[] { BookingStatus.Pending, BookingStatus.Confirmed, BookingStatus.CheckedIn };

            return await _context.Bookings.AnyAsync(b =>
                blockingStatuses.Contains(b.Status)
                && b.TimeRange.End > now
                && _context.Homes.Any(h => h.Id == b.HomeId && h.HostProfileId == hostId),
                cancellationToken);
        }
    }
}
