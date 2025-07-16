

using MovieCore.Models.Dtos;

namespace MovieContracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsByMovieIdAsync(int movieId);
    }
}
