using Airnb.Domain.Common;
using Airnb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Domain.Entities
{
    public class RefreshToken : AggregateRoot
    {
        public Guid UserId { get; private set; }                // Foreign key to the User entity
        public string TokenHash { get; private set; } = null!;  // Hashed version of the refresh token for security
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public bool IsActive => RevokedAt is null && DateTime.UtcNow < ExpiresAt;

        private RefreshToken() { } // EF Core

        private RefreshToken(Guid userId, string tokenHash, DateTime expiresAt)
        {
            UserId = userId;
            TokenHash = tokenHash;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
            Validate();
        }

        public static RefreshToken Create(Guid userId, string tokenHash, DateTime expiresAt)
        {
            return new RefreshToken(userId, tokenHash, expiresAt);
        }

        public void Revoke()
        {
            RevokedAt ??= DateTime.UtcNow;  // Set RevokedAt to current time if it hasn't been set already
        }

        private void Validate()
        {
            if (UserId == Guid.Empty)
                throw new DomainException("UserId mangler.");
            if (string.IsNullOrWhiteSpace(TokenHash))
                throw new DomainException("Token hash mangler.");
            if (ExpiresAt <= CreatedAt)
                throw new DomainException("Udløbstiden skal ligge i fremtiden.");
        }
    }
}
