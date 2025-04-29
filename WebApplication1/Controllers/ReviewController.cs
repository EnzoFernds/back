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
    public async Task<ActionResult<Review>> GetReview(int id)
    {
        var review = await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Restaurant)
            .FirstOrDefaultAsync(r => r.ReviewId == id);

        if (review == null)
            return NotFound();

        return review;
    }

    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        review.DatePosted = DateTime.UtcNow;
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, Review review)
    {
        if (id != review.ReviewId)
            return BadRequest();

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reviews.Any(r => r.ReviewId == id))
                return NotFound();
            else
                throw;
        }

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
