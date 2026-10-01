using Airnb.Infrastructure.DataAccessLayer.DbContexts;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Airnb.Domain.Entities;
using Airnb.Application.Repository.Interfaces;

namespace Airnb.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AirnbDbContext _dbContext;

        public UserRepository(AirnbDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.FindAsync(new object[] { userId }, cancellationToken);
        }
        public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await GetByIdAsync(userId, cancellationToken);
            return user != null;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }


    }
}
