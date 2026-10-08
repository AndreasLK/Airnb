using Airnb.Application.Security.Interfaces;
using Airnb.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;   // For creating and handling JWT tokens, hvis denne ikke er tilføjet, virker SymmetricSecurityKey, SigningCredentials, JwtSecurityToken, JwtSecurityTokenHandler ikke
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Airnb.Infrastructure.Security
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string Token, DateTime ExpiresAt) CreateAccessToken(User user)
        {
            var minutes = int.Parse(_configuration["Jwt:AccessTokenMinutes"] ?? "15");                  // Default to 15 minutes if not specified
            var expiresAt = DateTime.UtcNow.AddMinutes(minutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));     // Ensure the key is not null
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);               // Use HMAC SHA256 algorithm for signing

            var claims = new[] // Define the claims for the JWT
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),       // Subject claim with the user's ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email),             // Email claim with the user's email
                new Claim(ClaimTypes.Name, user.Name)                             // Name claim with the user's name
            };

            var token = new JwtSecurityToken(             // Create the JWT token
                issuer: _configuration["Jwt:Issuer"],     // Ensure the issuer is not null
                audience: _configuration["Jwt:Audience"], // Ensure the audience is not null
                claims: claims,                           // Add the claims to the token
                expires: expiresAt,                       // Set the expiration time for the token
                signingCredentials: credentials);         // Sign the token with the specified credentials

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }

        public (string Token, string TokenHash, DateTime ExpiresAt) CreateRefreshToken()          // Create a new refresh token, hash it, and return the token, its hash, and the expiration date
        {
            var minutes = int.Parse(_configuration["Jwt:RefreshTokenMinutes"] ?? "5");            // Default to 5 minutes if not specified
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));               // Generate a random 64-byte token and convert it to a Base64 string

            return (token, HashRefreshToken(token), DateTime.UtcNow.AddMinutes(minutes));         // Return the token, its hash, and the expiration date
        }

        public string HashRefreshToken(string refreshToken)                     
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)));    // Hash the refresh token using SHA256 and return it as a hexadecimal string
        }
    }
}
