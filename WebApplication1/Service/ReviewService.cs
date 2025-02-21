using System;
using System.Collections.Generic;

namespace RestaurantManagement.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;

        public ReviewService(ReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review CreateReview(Review review)
        {
            _reviewRepository.Add(review);
            return review;
        }

        public List<Review> GetReviewsByRestaurant(int restaurantId)
        {
            return _reviewRepository.GetByRestaurantId(restaurantId);
        }

        public List<Review> GetReviewsByUser(int userId)
        {
            return _reviewRepository.GetByUserId(userId);
        }

        public void DeleteReview(int id)
        {
            var review = _reviewRepository.Get(id);
            if (review == null)
                throw new Exception("Review not found.");

            _reviewRepository.Delete(id);
        }
    }
}
