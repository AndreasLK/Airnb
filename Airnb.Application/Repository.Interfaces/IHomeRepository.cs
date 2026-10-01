using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Repository.Interfaces
{
    public interface IHomeRepository
    {
        Task AddAsync(Home home, CancellationToken cancellationToken = default);
        Task<Home?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Update(Home home);
        void Delete(Home home);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        

    }
}
