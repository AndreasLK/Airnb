using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Airnb.Infrastructure.Queries.Guests;
using Airnb.Shared.Guests.DTO;
using Airnb.Application.Usecases.Guests.UpdateGuest;
using Airnb.Shared.Guests.Request;

namespace Airnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestQueries _queries;
        private readonly IUpdateGuestUsecase _update;
        
        public GuestController(
            IGuestQueries queries,
            IUpdateGuestUsecase update)
        {
            _queries = queries;
            _update = update;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<IReadOnlyList<GuestDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await _queries.ListGuestsAsync(ct));
        }

        

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult> Update(Guid id, UpdateGuestRequest request, CancellationToken ct)
        {
            await _update.ExecuteAsync(id, request, ct);
            return NoContent();
        }
    }
}
