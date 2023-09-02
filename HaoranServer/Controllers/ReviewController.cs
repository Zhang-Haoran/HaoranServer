using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.ReviewDto;

namespace HaoranServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewContext _reviewContext;

        private readonly UserContext _userContext;

        public ReviewController(ReviewContext context, UserContext userContext)
        {
            _reviewContext = context;
            _userContext = userContext;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> Getreview()
        {
          if (_reviewContext.review == null)
          {
              return NotFound();
          }
            return await _reviewContext.review.Include(r => r.User).ToListAsync(); // 用include 带上 User
        }

        // GET: api/Review/5
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReview(int reviewId)
        {
          if (_reviewContext.review == null)
          {
              return NotFound();
          }
            var review = await _reviewContext.review.Include(r => r.User).FirstOrDefaultAsync(r => r.ReviewId == reviewId); // 用include 带上 User

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> PutReview(int reviewId, Review review)
        {
            if (reviewId != review.ReviewId)
            {
                return BadRequest();
            }

            _reviewContext.Entry(review).State = EntityState.Modified;

            try
            {
                await _reviewContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(reviewId))
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

        // POST: api/Review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview([FromBody] ReviewPostDto reviewPostDto) // 通过 dto来控制 要求request里的body
        {
            // 根据UserId查找用户
            var user = await _userContext.user.FindAsync(reviewPostDto.UserId);

            // 如果没有找到用户，返回一个错误
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            // 新建和赋值
            Review review = new Review
            {
                Rating = reviewPostDto.Rating,
                Comment = reviewPostDto.Comment,
                UserId = reviewPostDto.UserId,
            };
          if (_reviewContext.review == null)
          {
              return Problem("Entity set 'ReviewContext.review'  is null.");
          }
            _reviewContext.review.Add(review);
            await _reviewContext.SaveChangesAsync();
            // 返回的body中带上User entity的信息
            review.User = user;
            return CreatedAtAction("GetReview", new { reviewId = review.ReviewId }, review);
        }

        // DELETE: api/Review/5
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (_reviewContext.review == null)
            {
                return NotFound();
            }
            var review = await _reviewContext.review.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _reviewContext.review.Remove(review);
            await _reviewContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int reviewId)
        {
            return (_reviewContext.review?.Any(e => e.ReviewId == reviewId)).GetValueOrDefault();
        }
    }
}
