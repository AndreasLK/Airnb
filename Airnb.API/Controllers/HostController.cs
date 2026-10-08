using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airnb.Application.Usecases.Hosts.CreateHost;
using Airnb.Infrastructure.Queries.Hosts;
using Airnb.Application.Usecases.Hosts.DeleteHost;
using Airnb.Shared.Hosts.Requests;

namespace Airnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private readonly IBecomeHostUsecase _becomeHost;
        private readonly IDeleteHostUsecase _deleteHost;
        private readonly IHostQueries _hostQueries;

        public HostController(
            IBecomeHostUsecase becomeHost,
            IDeleteHostUsecase deleteHost,
            IHostQueries hostQueries)
        {
            _becomeHost = becomeHost;
            _deleteHost = deleteHost;
            _hostQueries = hostQueries;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> BecomeHost(HostRequest request, CancellationToken ct)
        {
            var hostId = await _becomeHost.ExecuteAsync(request.UserId, ct);
            return StatusCode(StatusCodes.Status201Created, new { hostId });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteHost(Guid id, CancellationToken ct)
        {
            await _deleteHost.ExecuteAsync(id, ct);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHost(Guid id, CancellationToken ct)
        {
            var host = await _hostQueries.GetByUserIdAsync(id, ct);
            if (host == null)
            {
                return NotFound();
            }
            return Ok(host);
        }

        



    }
}
