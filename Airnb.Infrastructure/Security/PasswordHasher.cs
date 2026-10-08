using Airnb.Application.Security.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Airnb.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher 
    { 
        private readonly PasswordHasher<object> _inner = new();  //using Microsoft.AspNetCore.Identity.PasswordHasher new PasswordHasher<object>();
        public string Hash(string password) =>    //dette sker kun ved registrering af en bruger, hvor passwordet bliver hashet og gemt i databasen
            _inner.HashPassword(null!, password); //returns a hashed password laver et salt og hasher passwordet med PBKDF2 algoritmen, det er 16 bytes salt og 32 bytes hash, og returnerer en string som indeholder salt og hash i base64 format
        public bool Verify(string password, string hash) => 
            _inner.VerifyHashedPassword(null!, hash, password) != PasswordVerificationResult.Failed;  //returns true if the password matches the hash, false otherwise
    }
}
