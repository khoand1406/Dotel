using Dotel2.Models;
using Dotel2.ViewModels;

namespace Dotel2.Repository.Reviews
{
    public interface IReviewRepository
    {
        public List<ReviewViewModel> GetReviews(int rentalId);

        public void CreateReview(Review review);

        public void UpdateReview(Review review);
    }
}
