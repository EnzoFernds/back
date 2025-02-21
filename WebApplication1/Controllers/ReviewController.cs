using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewController(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public ActionResult<List<Review>> GetAllReviews()
        {
            return Ok(_reviewRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Review> GetReviewById(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public ActionResult CreateReview(Review review)
        {
            _reviewRepository.Add(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, review);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateReview(int id, Review review)
        {
            if (id != review.ReviewId)
                return BadRequest();
            _reviewRepository.Update(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteReview(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null)
                return NotFound();
            _reviewRepository.Delete(id);
            return NoContent();
        }
    }
}
