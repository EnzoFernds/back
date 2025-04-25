using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Services;

namespace RestaurantManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;

        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST /api/review
        [HttpPost]
        public ActionResult<Review> CreateReview([FromBody] CreateReviewDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var rev = _reviewService.CreateReview(dto);
                return CreatedAtAction(
                    nameof(GetReviewById),
                    new { id = rev.ReviewId },
                    rev
                );
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
        }

        // GET /api/review/{id}
        [HttpGet("{id}")]
        public ActionResult<Review> GetReviewById(int id)
        {
            var rev = _reviewService.GetReviewById(id);
            if (rev == null) return NotFound();
            return Ok(rev);
        }

        // GET /api/review/by-restaurant/{restoId}
        [HttpGet("by-restaurant/{restoId}")]
        public ActionResult<List<Review>> GetByRestaurant(int restoId)
            => Ok(_reviewService.GetByRestaurant(restoId));

        // GET /api/review/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public ActionResult<List<Review>> GetByUser(int userId)
            => Ok(_reviewService.GetByUser(userId));

        // PUT /api/review/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateReview(int id, [FromBody] UpdateReviewDTO dto)
        {
            if (id != dto.ReviewId) return BadRequest();

            try
            {
                _reviewService.UpdateReview(dto);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
        }

        // DELETE /api/review/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteReview(int id)
        {
            try
            {
                _reviewService.DeleteReview(id);
                return NoContent();
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(knf.Message);
            }
        }
    }
}
