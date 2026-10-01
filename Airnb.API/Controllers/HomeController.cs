using Airnb.Application.Usecases.Homes.CreateHome;
using Airnb.Application.Usecases.Homes.DeleteHome;
using Airnb.Application.Usecases.Homes.UpdateHome;
using Airnb.Infrastructure.Queries.Homes;
using Airnb.Shared.Homes.DTO;
using Airnb.Shared.Homes.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly IHomeQueries _queries;
        private readonly ICreateHomeUsecase _create;
        private readonly IUpdateHomeUsecase _update;
        private readonly IDeleteHomeUsecase _delete;

        public HomesController(
            IHomeQueries queries,
            ICreateHomeUsecase create,
            IUpdateHomeUsecase update,
            IDeleteHomeUsecase delete)
        {
            _queries = queries;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        [EndpointSummary("Henter alle boliger")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<HomeDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await _queries.GetAllAsync(ct));
        }

        [HttpGet("{id:guid}")]
        [EndpointSummary("Henter en bolig efter ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HomeDto>> GetById(Guid id, CancellationToken ct)
        {
            var home = await _queries.GetByIdAsync(id, ct);
            return home is null ? NotFound() : Ok(home);
        }

        [HttpPost]
        [EndpointSummary("Opretter en ny bolig")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HomeDto>> Create(CreateHomeRequest request, CancellationToken ct)
        {
            var home = await _create.ExecuteAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = home.Id }, home);
        }

        [HttpPut("{id:guid}")]
        [EndpointSummary("Opdaterer en eksisterende bolig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, Guid hostId, UpdateHomeRequest request, CancellationToken ct)
        {
            await _update.ExecuteAsync(id, hostId, request, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [EndpointSummary("Sletter en bolig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]   // huset har aktive bookinger
        public async Task<IActionResult> Delete(Guid id, Guid hostId, CancellationToken ct)
        {
            await _delete.ExecuteAsync(id, hostId, ct);
            return NoContent();
        }
    }


}
