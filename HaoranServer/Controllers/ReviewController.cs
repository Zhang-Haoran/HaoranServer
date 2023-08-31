using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto;

namespace HaoranServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewContext _context;

        private readonly UserContext _userContext;

        public ReviewController(ReviewContext context, UserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> Getreview()
        {
          if (_context.review == null)
          {
              return NotFound();
          }
            return await _context.review.Include(r => r.User).ToListAsync();
        }

        // GET: api/Review/5
        [HttpGet("{reviewId}")]
        public async Task<ActionResult<Review>> GetReview(int reviewId)
        {
          if (_context.review == null)
          {
              return NotFound();
          }
            var review = await _context.review.Include(r => r.User).FirstOrDefaultAsync(r => r.ReviewId == reviewId);

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

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Review>> PostReview([FromBody] ReviewPostDto reviewPostDto)
        {
            // 根据UserId查找用户
            var user = await _userContext.user.FindAsync(reviewPostDto.UserId);

            // 如果没有找到用户，返回一个错误
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            Review review = new Review
            {
                Rating = reviewPostDto.Rating,
                Comment = reviewPostDto.Comment,
                UserId = reviewPostDto.UserId,
            };
          if (_context.review == null)
          {
              return Problem("Entity set 'ReviewContext.review'  is null.");
          }
            _context.review.Add(review);
            await _context.SaveChangesAsync();

            review.User = user;
            return CreatedAtAction("GetReview", new { reviewId = review.ReviewId }, review);
        }

        // DELETE: api/Review/5
        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (_context.review == null)
            {
                return NotFound();
            }
            var review = await _context.review.FindAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            _context.review.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int reviewId)
        {
            return (_context.review?.Any(e => e.ReviewId == reviewId)).GetValueOrDefault();
        }
    }
}
