using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace Airnb.Application.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Booking>> GetByHomeIdAsync(Guid homeId, CancellationToken cancellationToken = default);
        Task AddAsync(Booking booking, CancellationToken cancellationToken = default);
        void Update(Booking booking);
        void Delete(Booking booking);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsForHomeAsync(Guid homeId, CancellationToken cancellationToken = default);
        Task<bool> HasUpcomingForHostAsync(Guid hostId, CancellationToken cancellationToken = default);

    }
}
