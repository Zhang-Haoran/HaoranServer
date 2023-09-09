using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Models;
using HaoranServer.Dto.BookingDto;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;
        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Getbooking()
        {
            var bookings = await _bookingService.Getbookings();
            if (bookings == null || !bookings.Any())
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        [HttpGet("{bookingId}")]
        public async Task<ActionResult<Booking>> GetBooking(int bookingId)
        {
            var booking = await _bookingService.GetBooking(bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> PutBooking(int bookingId, BookingPutDto bookingPutDto)
        {
            try
            {
                if (!_bookingService.BookingExists(bookingId))
                {
                    return NotFound();
                }

                await _bookingService.UpdateBooking(bookingId, bookingPutDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingPostDto bookingPostDto)
        {
            var booking = await _bookingService.CreateBooking(bookingPostDto);
            return CreatedAtAction("GetBooking", new { bookingId = booking.BookingId }, booking);
        }

        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (!_bookingService.BookingExists(bookingId))
            {
                return NotFound();
            }

            await _bookingService.DeleteBooking(bookingId);
            return NoContent();
        }
    }
}
