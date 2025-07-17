using Microsoft.AspNetCore.Mvc;
using MovieContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Paging;

namespace MoviePresentation.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public ReviewsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/reviews/movie/{movieId}
        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<PagedResponse<ReviewDto>>> GetReviews(int movieId, [FromQuery] PagingParameters paging)
        {
            var pagedResult = await serviceManager.ReviewService.GetReviewsByMovieIdAsync(movieId, paging);

            if (pagedResult.Items == null || !pagedResult.Items.Any())
                return NotFound($"Filmen med id {movieId} hittades inte eller har inga recensioner.");

            var response = new PagedResponse<ReviewDto>
            {
                Data = pagedResult.Items,
                Meta = new PaginationMeta
                {
                    TotalItems = pagedResult.TotalItems,
                    CurrentPage = pagedResult.CurrentPage,
                    TotalPages = pagedResult.TotalPages,
                    PageSize = pagedResult.PageSize
                }
            };

            return Ok(response);
        }

    }
}

//[ApiController]
//[Route("api/reviews")]
//public class ReviewsController : ControllerBase
//{
//    private readonly MovieApiContext _context;

//    public ReviewsController(MovieApiContext context)
//    {
//        _context = context;
//    }

//    // GET: api/reviews/movie/{movieId}
//    [HttpGet("movie/{movieId}")]
//    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
//    {
//        var movie = await _context.Movies
//            .Include(m => m.Reviews)
//            .FirstOrDefaultAsync(m => m.Id == movieId);

//        if (movie is null)
//            return NotFound($"Filmen med id {movieId} hittades inte.");

//        var reviews = movie.Reviews.Select(r => new ReviewDto
//        {
//            ReviewerName = r.ReviewerName,
//            Rating = r.Rating,
//            Comment = r.Comment
//        }).ToList();

//        return Ok(reviews);
//    }

//}
//}
