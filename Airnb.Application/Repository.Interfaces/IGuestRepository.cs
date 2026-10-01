using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Repository.Interfaces
{
    public interface IGuestRepository
    {
        Task<GuestProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(GuestProfile guest, CancellationToken cancellationToken = default);
        void Update(GuestProfile guest);
        void Delete(GuestProfile guest);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
