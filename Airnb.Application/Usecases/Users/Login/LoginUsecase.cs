using Airnb.Application.Exceptions;
using Airnb.Application.Repository.Interfaces;
using Airnb.Application.Security.Interfaces;
using Airnb.Domain.Entities;
using Airnb.Shared.Auth.DTO;
using Airnb.Shared.Auth.Requests;


namespace Airnb.Application.Usecases.Users.Login
{
    public class LoginUsecase : ILoginUsecase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUsecase(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Find brugeren og tjek passwordet
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null || !_passwordHasher.Verify(request.Password, user.PasswordHash))     // verificér passwordet igennem hashing algoritmen
                throw new InvalidCredentialsException("Forkert email eller adgangskode.");

            // 2. Ryd op i brugerens gamle refresh tokens
            await _refreshTokenRepository.DeleteInactiveForUserAsync(user.Id, cancellationToken); // slet gamle refresh tokens, der ikke længere er aktive

            // 3. Lav access token
            var (accessToken, accessExpiresAt) = _tokenService.CreateAccessToken(user);           // lav access token med brugerens informationer

            // 4. Lav refresh token – kun hashet gemmes
            var (refreshToken, refreshHash, refreshExpiresAt) = _tokenService.CreateRefreshToken(); // lav refresh token, som kun gemmes i hashet form
            await _refreshTokenRepository.AddAsync(
                RefreshToken.Create(user.Id, refreshHash, refreshExpiresAt),
                cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            // 5. Returnér begge tokens
            return new LoginResponse(user.Id, accessToken, accessExpiresAt, refreshToken);
        }
    }
}
