using System;
using System.Collections.Generic;
using System.Text;
using Airnb.Domain.Entities;

namespace Airnb.Application.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        void Update(User user);
        void Delete(User user);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
