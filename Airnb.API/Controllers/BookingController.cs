using Airnb.Application.Usecases.Bookings.CreateBooking;
using Airnb.Application.Usecases.Bookings.DeleteBooking;
using Airnb.Application.Usecases.Bookings.UpdateBooking;
using Airnb.Infrastructure.Queries.Bookings;
using Airnb.Shared.Bookings.DTO;
using Airnb.Shared.Bookings.Request.Bookings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingQueries _queries;
        private readonly ICreateBookingUsecase _create;
        private readonly IUpdateBookingUsecase _update;
        private readonly IDeleteBookingUsecase _delete;

        public BookingsController(
            IBookingQueries queries,
            ICreateBookingUsecase create,
            IUpdateBookingUsecase update,
            IDeleteBookingUsecase delete)
        {
            _queries = queries;
            _create = create;
            _update = update;
            _delete = delete;
        }

        [HttpGet]
        [EndpointSummary("Henter alle bookinger")]      //EndpointSummary attributten giver en kort beskrivelse af, hvad endpointet gør i API-dokumentationen. I dette tilfælde angiver den, at endpointet henter alle bookinger.
        [ProducesResponseType(StatusCodes.Status200OK)] //ProducesResponseType attributten angiver de mulige HTTP-statuskoder, som endpointet kan returnere. I dette tilfælde angiver den, at endpointet kan returnere 200 OK, hvis forespørgslen lykkes.
        public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAll(CancellationToken ct)
        {
            return Ok(await _queries.GetAllAsync(ct));
        }

        [HttpGet("{id:guid}")]
        [EndpointSummary("Henter en booking efter ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto>> GetById(Guid id, CancellationToken ct)
        {
            var booking = await _queries.GetByIdAsync(id, ct);
            return booking is null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        [EndpointSummary("Opretter en ny booking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingRequest request, CancellationToken ct)
        {
            try
            {
                var booking = await _create.ExecuteAsync(request, ct); //her sætter vi gang i oprettelsen af en booking ved at kalde ExecuteAsync metoden på _create objektet,
                                                                       //som er en instans af ICreateBookingUsecase interfacet.
                                                                       //Vi sender request objektet og CancellationToken ct som parametre til metoden.
                return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking); //her returnerer vi en CreatedAtAction respons, som angiver, at en ny booking er blevet oprettet.
                                                                                           //Vi bruger nameof(GetById) for at angive navnet på den metode, der kan bruges til at hente den oprettede booking.
                                                                                           //Vi sender også et anonymt objekt med id'et på den oprettede booking og selve booking objektet som responsindhold.
            }
            catch (ArgumentException ex)
            {
                return Problem(
                    title: "Ugyldig booking",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{id:guid}")]
        [EndpointSummary("Opdaterer en eksisterende booking")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, UpdateBookingRequest request, CancellationToken ct)
        {
            try
            {
                await _update.ExecuteAsync(id, request, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [EndpointSummary("Sletter en eksisterende booking")] //EndPointSummary attributten giver en kort beskrivelse af, hvad endpointet gør i API-dokumentationen. I dette tilfælde angiver den, at endpointet sletter en eksisterende booking.
        [ProducesResponseType(StatusCodes.Status204NoContent)] //ProducesResponseType attributterne angiver de mulige HTTP-statuskoder, som endpointet kan returnere. I dette tilfælde angiver den, at endpointet kan returnere 204 No Content, hvis sletningen lykkes, og 404 Not Found, hvis bookingen ikke findes.
        [ProducesResponseType(StatusCodes.Status404NotFound)]  //Dette er en anden ProducesResponseType attribut, der angiver, at endpointet kan returnere 404 Not Found, hvis bookingen ikke findes.
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                await _delete.ExecuteAsync(id, ct);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
