using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly RestaurantContext _context;

    public ReviewController(RestaurantContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewReadDTO>> GetReview(int id)
    {
        var review = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .FirstOrDefaultAsync(r => r.ReviewId == id);

        if (review == null)
            return NotFound();

        var dto = new ReviewReadDTO
        {
            ReviewId = review.ReviewId,
            Rating = review.Rating,
            Comment = review.Comment,
            DatePosted = review.DatePosted,
            UserName = review.User?.UserName,
            RestaurantName = review.Restaurant?.Name
        };

        return dto;
    }


    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(CreateReviewDTO dto)
    {
        var review = new Review
        {
            UserId = dto.UserId,
            RestaurantId = dto.RestaurantId,
            Rating = dto.Rating,
            Comment = dto.Commentaire,
            DatePosted = DateTime.UtcNow
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, UpdateReviewDTO dto)
    {
        if (id != dto.ReviewId)
            return BadRequest();

        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        // Mise à jour
        review.Rating = dto.Rating;
        review.Comment = dto.Comment;

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
            return NotFound();

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
