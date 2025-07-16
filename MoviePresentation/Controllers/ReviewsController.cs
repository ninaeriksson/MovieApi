using Microsoft.AspNetCore.Mvc;
using MovieCore.Models.Dtos;
using MovieContracts;

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
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
        {
            var reviews = await serviceManager.ReviewService.GetReviewsByMovieIdAsync(movieId);

            if (reviews == null)
                return NotFound($"Filmen med id {movieId} hittades inte.");

            return Ok(reviews);
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
