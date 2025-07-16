using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;

namespace MovieServices.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewsByMovieIdAsync(int movieId)
        {
            var movie = await unitOfWork.Movies.GetAsync(movieId);

            if (movie == null || movie.Reviews == null)
                return Enumerable.Empty<ReviewDto>();

            var reviews = movie.Reviews.Select(r => new ReviewDto
            {
                ReviewerName = r.ReviewerName,
                Rating = r.Rating,
                Comment = r.Comment
            });

            return reviews;
        }
    }
}
