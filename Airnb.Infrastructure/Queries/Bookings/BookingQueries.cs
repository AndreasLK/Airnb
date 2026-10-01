using Airnb.Domain.Entities;
using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Airnb.Shared.Bookings.DTO;
using Microsoft.EntityFrameworkCore;


namespace Airnb.Infrastructure.Queries.Bookings
{
    public class BookingQueries : IBookingQueries
    {
        private readonly AirnbDbContext _context;
        public BookingQueries(AirnbDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Select(b => new BookingDto(
                    b.Id,
                    b.GuestId,
                    b.HomeId,
                    b.Status.ToString(),
                    b.TimeRange.Start,
                    b.TimeRange.End,
                    b.NumberOfGuests,
                    b.Reciept.StartPrice.Amount,
                    b.Reciept.StartPrice.Currency))
                .ToListAsync(cancellationToken);
        }
        public async Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookingDto(
                    b.Id,
                    b.GuestId,
                    b.HomeId,
                    b.Status.ToString(),
                    b.TimeRange.Start,
                    b.TimeRange.End,
                    b.NumberOfGuests,
                    b.Reciept.StartPrice.Amount,
                    b.Reciept.StartPrice.Currency))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<BookingDto>> GetByHomeIdAsync(Guid homeId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings
                .Where(b => b.HomeId == homeId)
                .Select(b => new BookingDto(
                    b.Id,
                    b.GuestId,
                    b.HomeId,
                    b.Status.ToString(),
                    b.TimeRange.Start,
                    b.TimeRange.End,
                    b.NumberOfGuests,
                    b.Reciept.StartPrice.Amount,
                    b.Reciept.StartPrice.Currency))
                .ToListAsync(cancellationToken);
        }
    }
}
