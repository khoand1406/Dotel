using Dotel2.Models;
using Dotel2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Dotel2.Repository.Reviews
{
    public class ReviewRespository : IReviewRepository
    {
        private readonly DotelDBContext _dbContext;
        public ReviewRespository(DotelDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateReview(Review review)
        {
            _dbContext.Add(review);
            _dbContext.SaveChanges();
        }

        public List<ReviewViewModel> GetReviews(int rentalId)
        {
            return _dbContext.Reviews
    .Where(review => review.RentalId == rentalId)
    .Include(review => review.User)
    .OrderByDescending(review => review.Rating)
    .Take(5)
    .Select(review => new ReviewViewModel
    {
        Id = review.Id,
        Rating = review.Rating,
        Comment = review.Comment,
        CreatedAt = review.CreatedAt,
        RentalId = review.RentalId,
        FullName = review.User.Fullname
    }
    )
    .ToList();
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
