using Airnb.Application.Exceptions;
using Airnb.Application.Repository.Interfaces;
using Airnb.Application.Security.Interfaces;
using Airnb.Shared.Auth.DTO;
using Airnb.Shared.Auth.Requests;



namespace Airnb.Application.Usecases.Users.RefreshTokens
{
    public class RefreshTokenUsecase : IRefreshTokenUsecase
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenUsecase(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> ExecuteAsync(RefreshRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Find tokenet ud fra dets hash
            var hash = _tokenService.HashRefreshToken(request.RefreshToken);
            var stored = await _refreshTokenRepository.GetByTokenHashAsync(hash, cancellationToken);

            // 2. Det skal findes, ikke være udløbet og ikke være brugt
            if (stored is null || !stored.IsActive)
                throw new InvalidCredentialsException("Refresh token er ugyldigt eller udløbet.");

            // 3. Find brugeren
            var user = await _userRepository.GetByIdAsync(stored.UserId, cancellationToken)
                ?? throw new InvalidCredentialsException("Brugeren findes ikke længere.");

            // 4. Rotation: brug det gamle op og lav et nyt
            stored.Revoke();
            var (newRefreshToken, newHash, newExpiresAt) = _tokenService.CreateRefreshToken();
            await _refreshTokenRepository.AddAsync(
                Domain.Entities.RefreshToken.Create(user.Id, newHash, newExpiresAt),
                cancellationToken);

            // 5. Nyt access token
            var (accessToken, accessExpiresAt) = _tokenService.CreateAccessToken(user);

            // 6. Gem begge ændringer på én gang
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return new LoginResponse(user.Id, accessToken, accessExpiresAt, newRefreshToken);
        }
    }
}
