using Shared;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Added this comment to make sure git works fine with this file!
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet(Name = "AllBookings")]
        public async Task<ActionResult<IEnumerable<BookingResponse>>> GetAllBookings(CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllAsync(cancellationToken);
            return Ok(bookings.Select(ToResponse));
        }

        [HttpGet("{id:guid}", Name = "GetBookingById")]
        public async Task<ActionResult<BookingResponse>> GetBookingById(Guid id, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetByIdAsync(id, cancellationToken);
            if (booking == null)
                return NotFound();

            return Ok(ToResponse(booking));
        }

        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult<BookingResponse>> CreateBooking(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            Booking booking;
            try
            {
                var timeRange = new TimeRange(request.Start, request.End); // adjust to your TimeRange's real ctor
                var reciept = new Reciept(); // TODO: replace once Reciept's shape is known

                booking = Booking.Create(
                    request.GuestId,
                    request.HomeId,
                    request.Status,
                    timeRange,
                    DateTime.UtcNow,
                    request.NumberOfGuests,
                    request.Price,
                    reciept);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            await _bookingRepository.AddAsync(booking, cancellationToken);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute("GetBookingById", new { id = booking.Id }, ToResponse(booking));
        }

        [HttpPut("{id:guid}", Name = "UpdateBooking")]
        public async Task<IActionResult> UpdateBooking(Guid id, UpdateBookingRequest request, CancellationToken cancellationToken)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(id, cancellationToken);
            if (existingBooking == null)
                return NotFound();

            try
            {
                var timeRange = new TimeRange(request.Start, request.End);
                existingBooking.UpdateDetails(request.Status, timeRange, request.NumberOfGuests, request.Price, existingBooking.Reciept);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            _bookingRepository.Update(existingBooking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}", Name = "DeleteBooking")]
        public async Task<IActionResult> DeleteBooking(Guid id, CancellationToken cancellationToken)
        {
            var existingBooking = await _bookingRepository.GetByIdAsync(id, cancellationToken);
            if (existingBooking == null)
                return NotFound();

            _bookingRepository.Delete(existingBooking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        private static BookingResponse ToResponse(Booking booking) =>
            new(booking.Id, booking.GuestId, booking.HomeId, booking.Status,
                booking.CreatedTime, booking.NumberOfGuests, booking.Price);
    }
}