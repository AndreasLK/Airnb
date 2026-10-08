using Airnb.Domain.Common;
using Airnb.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Airnb.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string Email { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        private User() { } // EF Core

        private User(string email, string name, string passwordHash)
        {
            Email = email.Trim().ToLowerInvariant();
            Name = name.Trim();
            PasswordHash = passwordHash;
            Validate();
        }

        public static User Create(string email, string name, string passwordHash)
        {
            return new User(email, name, passwordHash);
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Email))
                throw new DomainException("Email må ikke være tom.");
            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException("Email har ikke et gyldigt format.");
            if (string.IsNullOrWhiteSpace(Name))
                throw new DomainException("Navn må ikke være tomt.");
            if (string.IsNullOrWhiteSpace(PasswordHash))
                throw new DomainException("Password hash mangler.");
        }
    }
}
