using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.DataAccessLayer;

namespace Persistance.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDBContext _context;

        public BookingRepository(BookingDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .AsNoTracking()
                .ToListAsync(cancellationToken);
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
    }
}