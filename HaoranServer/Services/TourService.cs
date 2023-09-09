using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.TourDto;
using AutoMapper;

public class TourService
{
    private readonly TourContext _context;
    private readonly IMapper _mapper;

    public TourService(TourContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Tour>> GetAllTours()
    {
        return await _context.tour.ToListAsync();
    }

    public async Task<Tour> GetTour(int tourId)
    {
        return await _context.tour.FindAsync(tourId);
    }

    public async Task UpdateTour(int tourId, TourPutDto tourPutDto)
    {
        var tour = await _context.tour.FindAsync(tourId);
        if (tour != null)
        {
            _mapper.Map(tourPutDto, tour);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Tour> CreateTour(TourPostDto tourPostDto)
    {
        Tour tour = new Tour();
        _mapper.Map(tourPostDto, tour);
        _context.tour.Add(tour);
        await _context.SaveChangesAsync();
        return tour;
    }

    public async Task DeleteTour(int tourId)
    {
        var tour = await _context.tour.FindAsync(tourId);
        if (tour != null)
        {
            _context.tour.Remove(tour);
            await _context.SaveChangesAsync();
        }
    }

    public bool TourExists(int tourId)
    {
        return _context.tour?.Any(e => e.tourId == tourId) ?? false;
    }
}