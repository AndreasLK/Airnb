using Airnb.Application.Usecases.Users.Login;
using Airnb.Application.Usecases.Users.RefreshTokens;
using Airnb.Application.Usecases.Users.RegisterUser;
using Airnb.Shared.Auth.DTO;
using Airnb.Shared.Auth.Requests;
using Airnb.Shared.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUsecase _login;
        private readonly IRefreshTokenUsecase _refresh;
        private readonly IRegisterUserUsecase _register;

        public AuthController(ILoginUsecase login, IRefreshTokenUsecase refresh, IRegisterUserUsecase register)
        {
            _login = login;
            _refresh = refresh;
            _register = register;
        }

        [HttpPost("register")]
        [EndpointSummary("Registrerer en ny bruger")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken ct)
        {
            var userId = await _register.ExecuteAsync(request, ct);
            return StatusCode(StatusCodes.Status201Created, new { userId });
        }

        [HttpPost("login")]
        [EndpointSummary("Logger ind og returnerer et access token og et refresh token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken ct)
        {
            return Ok(await _login.ExecuteAsync(request, ct));
        }

        [HttpPost("refresh")]
        [EndpointSummary("Bytter et refresh token til et nyt par tokens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> Refresh(RefreshRequest request, CancellationToken ct)
        {
            return Ok(await _refresh.ExecuteAsync(request, ct));
        }

        [Authorize]
        [HttpGet("me")]
        [EndpointSummary("Viser indholdet af dit access token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Me()
        {
            return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }
}
