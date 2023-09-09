﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.TourDto;
using AutoMapper;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly TourContext _context;

        private readonly IMapper _mapper;

        public TourController(TourContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tour
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> Gettour()
        {
          if (_context.tour == null)
          {
              return NotFound();
          }
            return await _context.tour.ToListAsync();
        }

        // GET: api/Tour/5
        [HttpGet("{tourId}")]
        public async Task<ActionResult<Tour>> GetTour(int tourId)
        {
          if (_context.tour == null)
          {
              return NotFound();
          }
            var tour = await _context.tour.FindAsync(tourId);

            if (tour == null)
            {
                return NotFound();
            }

            return tour;
        }

        // PUT: api/Tour/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{tourId}")]
        public async Task<IActionResult> PutTour(int tourId, TourPutDto tourPutDto)
        {
            var tour = await _context.tour.FindAsync(tourId);
            _context.Entry(tour).State = EntityState.Modified;
            _mapper.Map(tourPutDto, tour);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TourExists(tourId))
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

        // POST: api/Tour
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(TourPostDto tourPostDto)
        {
          if (_context.tour == null)
          {
              return Problem("Entity set 'TourContext.tour'  is null.");
          }
            Tour tour = new Tour();
            _mapper.Map(tourPostDto, tour);
            _context.tour.Add(tour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTour", new { tourId = tour.tourId }, tour);
        }

        // DELETE: api/Tour/5
        [HttpDelete("{tourId}")]
        public async Task<IActionResult> DeleteTour(int tourId)
        {
            if (_context.tour == null)
            {
                return NotFound();
            }
            var tour = await _context.tour.FindAsync(tourId);
            if (tour == null)
            {
                return NotFound();
            }

            _context.tour.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TourExists(int tourId)
        {
            return (_context.tour?.Any(e => e.tourId == tourId)).GetValueOrDefault();
        }
    }
}
