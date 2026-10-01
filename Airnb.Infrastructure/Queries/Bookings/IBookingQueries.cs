using Airnb.Domain.Entities;
using Airnb.Shared.Bookings.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.Queries.Bookings
{
    public interface IBookingQueries
    {
        Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<BookingDto>> GetByHomeIdAsync(Guid homeId, CancellationToken cancellationToken = default);

    }
}
