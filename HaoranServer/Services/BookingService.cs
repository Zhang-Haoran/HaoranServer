using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.BookingDto;
using AutoMapper;

public class BookingService
{
    private readonly BookingContext _context;
    private readonly IMapper _mapper;

    public BookingService(BookingContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Booking>> Getbookings()
    {
        return await _context.booking.Include(u => u.User).Include(t => t.Tour).ToListAsync();
    }

    public async Task<Booking> GetBooking(int bookingId)
    {
        return await _context.booking.Include(u => u.User).Include(t => t.Tour).FirstOrDefaultAsync(b => b.BookingId == bookingId);
    }

    public async Task UpdateBooking(int bookingId, BookingPutDto bookingPutDto)
    {
        var booking = await _context.booking.FindAsync(bookingId);
        _context.Entry(booking).State = EntityState.Modified;
        _mapper.Map(bookingPutDto, booking);
        await _context.SaveChangesAsync();
    }

    public async Task<Booking> CreateBooking(BookingPostDto bookingPostDto)
    {
        Booking booking = new Booking();
        _mapper.Map(bookingPostDto, booking);
        _context.booking.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task DeleteBooking(int bookingId)
    {
        var booking = await _context.booking.FindAsync(bookingId);
        _context.booking.Remove(booking);
        await _context.SaveChangesAsync();
    }

    public bool BookingExists(int bookingId)
    {
        return _context.booking.Any(e => e.BookingId == bookingId);
    }
}
