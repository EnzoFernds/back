namespace RestaurantManagement.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepo;
        private readonly RestaurantRepository _restoRepo;
        private readonly UserRepository _userRepo;

        public ReviewService(
            ReviewRepository reviewRepo,
            RestaurantRepository restoRepo,
            UserRepository userRepo)
        {
            _reviewRepo = reviewRepo;
            _restoRepo = restoRepo;
            _userRepo = userRepo;
        }

        public Review CreateReview(CreateReviewDTO dto)
        {
            // 1) Vérifications
            var user = _userRepo.Get(dto.UserId);
            if (user == null)
                throw new KeyNotFoundException($"User {dto.UserId} introuvable.");

            var resto = _restoRepo.Get(dto.RestaurantId);
            if (resto == null)
                throw new KeyNotFoundException($"Restaurant {dto.RestaurantId} introuvable.");

            if (dto.Rating < 1 || dto.Rating > 5)
                throw new ArgumentException("La note doit être entre 1 et 5.");

            // 2) Création de l’entité
            var review = new Review
            {
                UserId = dto.UserId,
                RestaurantId = dto.RestaurantId,
                Rating = dto.Rating,
                Comment = dto.Commentaire,
                DatePosted = DateTime.UtcNow
            };

            // 3) Persistance
            _reviewRepo.Add(review);

            return review;
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepo.Get(id);
        }

        public List<Review> GetByRestaurant(int restaurantId)
        {
            return _reviewRepo.GetByRestaurantId(restaurantId);
        }

        public List<Review> GetByUser(string userId)
        {
            return _reviewRepo.GetByUserId(userId);
        }

        public void UpdateReview(UpdateReviewDTO dto)
        {
            var existing = _reviewRepo.Get(dto.ReviewId);
            if (existing == null)
                throw new KeyNotFoundException($"Review {dto.ReviewId} introuvable.");

            existing.Rating = dto.Rating;
            existing.Comment = dto.Comment;
            _reviewRepo.Update(existing);
        }

        public void DeleteReview(int id)
        {
            var existing = _reviewRepo.Get(id);
            if (existing == null)
                throw new KeyNotFoundException($"Review {id} introuvable.");

            _reviewRepo.Delete(id);
        }
    }
}
