using Microsoft.AspNetCore.Mvc;
using HaoranServer.Dto.ReviewDto;
using HaoranServer.Models;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            var reviews = await _reviewService.GetAllReviews();
            if (reviews == null || !reviews.Any())
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReview(int reviewId)
        {
            var review = await _reviewService.GetReview(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> PutReview(int reviewId, ReviewPutDto reviewPutDto)
        {
            if (!_reviewService.ReviewExists(reviewId))
            {
                return NotFound();
            }

            await _reviewService.UpdateReview(reviewId, reviewPutDto);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(ReviewPostDto reviewPostDto)
        {
            var review = await _reviewService.CreateReview(reviewPostDto);
            if (review == null)
            {
                return NotFound(new { message = "User not found." });
            }
            return CreatedAtAction("GetReview", new { reviewId = review.ReviewId }, review);
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (!_reviewService.ReviewExists(reviewId))
            {
                return NotFound();
            }

            await _reviewService.DeleteReview(reviewId);
            return NoContent();
        }
    }
}
