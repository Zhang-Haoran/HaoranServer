using Microsoft.EntityFrameworkCore;
using HaoranServer.Context;
using HaoranServer.Models;
using HaoranServer.Dto.ReviewDto;
using AutoMapper;

public class ReviewService
{
    private readonly ReviewContext _reviewContext;
    private readonly UserContext _userContext;
    private readonly IMapper _mapper;

    public ReviewService(ReviewContext reviewContext, UserContext userContext, IMapper mapper)
    {
        _reviewContext = reviewContext;
        _userContext = userContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Review>> GetAllReviews()
    {
        return await _reviewContext.review.Include(r => r.User).ToListAsync();
    }

    public async Task<Review> GetReview(int reviewId)
    {
        return await _reviewContext.review.Include(r => r.User).FirstOrDefaultAsync(r => r.ReviewId == reviewId);
    }

    public async Task UpdateReview(int reviewId, ReviewPutDto reviewPutDto)
    {
        var review = await _reviewContext.review.FindAsync(reviewId);
        if (review != null)
        {
            _mapper.Map(reviewPutDto, review);
            await _reviewContext.SaveChangesAsync();
        }
    }

    public async Task<Review> CreateReview(ReviewPostDto reviewPostDto)
    {
        var user = await _userContext.user.FindAsync(reviewPostDto.UserId);
        if (user != null)
        {
            Review review = new Review
            {
                Rating = reviewPostDto.Rating,
                Comment = reviewPostDto.Comment,
                UserId = reviewPostDto.UserId,
            };
            _reviewContext.review.Add(review);
            await _reviewContext.SaveChangesAsync();
            review.User = user;
            return review;
        }
        return null;
    }

    public async Task DeleteReview(int reviewId)
    {
        var review = await _reviewContext.review.FindAsync(reviewId);
        if (review != null)
        {
            _reviewContext.review.Remove(review);
            await _reviewContext.SaveChangesAsync();
        }
    }

    public bool ReviewExists(int reviewId)
    {
        return _reviewContext.review?.Any(e => e.ReviewId == reviewId) ?? false;
    }
}
