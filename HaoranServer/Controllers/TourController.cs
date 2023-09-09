using Microsoft.AspNetCore.Mvc;
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
        private readonly TourService _tourService;

        public TourController(TourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tour>>> Gettour()
        {
            var tours = await _tourService.GetAllTours();
            if (tours == null || !tours.Any())
            {
                return NotFound();
            }
            return Ok(tours);
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<Tour>> GetTour(int tourId)
        {
            var tour = await _tourService.GetTour(tourId);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpPut("{tourId}")]
        public async Task<IActionResult> PutTour(int tourId, TourPutDto tourPutDto)
        {
            if (!_tourService.TourExists(tourId))
            {
                return NotFound();
            }

            await _tourService.UpdateTour(tourId, tourPutDto);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(TourPostDto tourPostDto)
        {
            var tour = await _tourService.CreateTour(tourPostDto);
            return CreatedAtAction("GetTour", new { tourId = tour.tourId }, tour);
        }

        [HttpDelete("{tourId}")]
        public async Task<IActionResult> DeleteTour(int tourId)
        {
            if (!_tourService.TourExists(tourId))
            {
                return NotFound();
            }

            await _tourService.DeleteTour(tourId);
            return NoContent();
        }
    }
}
