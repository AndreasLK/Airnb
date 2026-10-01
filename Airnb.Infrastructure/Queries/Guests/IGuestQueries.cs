using Airnb.Domain.Entities;
using Airnb.Shared.Guests.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Infrastructure.Queries.Guests
{
    public interface IGuestQueries
    {
        Task<IReadOnlyList<GuestDto>> ListGuestsAsync(CancellationToken cancellationToken = default);
    }
}
