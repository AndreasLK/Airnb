using Airnb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Security.Interfaces
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiresAt) CreateAccessToken(User user); //Lave et access token (JWT) til en bruger

        (string Token, string TokenHash, DateTime ExpiresAt) CreateRefreshToken(); //Lave et random refresh token (JWT) til en bruger

        string HashRefreshToken(string refreshToken); //Hash refresh tokenet, så det kan gemmes i databasen
    }
}
