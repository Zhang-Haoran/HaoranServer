using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.BookingDto;
using AutoMapper;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingContext _context;

        private readonly IMapper _mapper;

        public BookingController(BookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> Getbooking()
        {
          if (_context.booking == null)
          {
              return NotFound();
          }
            return await _context.booking.Include(u => u.User).Include(t => t.Tour).ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{bookingId}")]
        public async Task<ActionResult<Booking>> GetBooking(int bookingId)
        {
          if (_context.booking == null)
          {
              return NotFound();
          }
            var booking = await _context.booking.Include(u => u.User).Include(t => t.Tour).FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Booking/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{bookingId}")]
        public async Task<IActionResult> PutBooking(int bookingId, BookingPutDto bookingPutDto)
        {
            var booking = await _context.booking.FindAsync(bookingId);
            _context.Entry(booking).State = EntityState.Modified;

            _mapper.Map(bookingPutDto, booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(bookingId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Booking
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingPostDto bookingPostDto)
        {
          if (_context.booking == null)
          {
              return Problem("Entity set 'BookingContext.booking'  is null.");
          }

            Booking booking = new Booking();
            _mapper.Map(bookingPostDto, booking);
            _context.booking.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { bookingId = booking.BookingId }, booking);
        }

        // DELETE: api/Booking/5
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> DeleteBooking(int bookingId)
        {
            if (_context.booking == null)
            {
                return NotFound();
            }
            var booking = await _context.booking.FindAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            _context.booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int bookingId)
        {
            return (_context.booking?.Any(e => e.BookingId == bookingId)).GetValueOrDefault();
        }
    }
}
