using Microsoft.AspNetCore.Mvc;
using Airnb.Application.Interfaces;

namespace Airnb.Application.Controllers
{
    //Added this comment to make sure git works fine with this file!
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _repository;

        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var bookings = await _repository.GetAllAsync(ct);
            return Ok(bookings);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var booking = await _repository.GetByIdAsync(id, ct);
            return booking is null ? NotFound() : Ok(booking);
        }
    }
}
