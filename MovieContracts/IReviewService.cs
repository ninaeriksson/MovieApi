

using MovieCore.Models.Dtos;
using MovieCore.Models.Paging;

namespace MovieContracts
{
    public interface IReviewService
    {
        Task<PagedResult<ReviewDto>> GetReviewsByMovieIdAsync(int movieId, PagingParameters paging);
    }
}
