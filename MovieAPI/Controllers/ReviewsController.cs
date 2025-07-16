//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MovieApi.Models.Dtos;
//using MovieCore.DomainContracts;
//using MovieData.Context;

//namespace MovieApi.Controllers
//{

//    [ApiController]
//    [Route("api/reviews")]
//    public class ReviewsController : ControllerBase
//    {
//        private readonly IUnitOfWork unitOfWork;

//        public ReviewsController(IUnitOfWork unitOfWork)
//        {
//            this.unitOfWork = unitOfWork;
//        }

//        // GET: api/reviews/movie/{movieId}
//        [HttpGet("movie/{movieId}")]
//        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
//        {
//            var movie = await unitOfWork.Movies.GetAsync(movieId);

//            if (movie == null)
//                return NotFound($"Filmen med id {movieId} hittades inte.");

//            var reviews = movie.Reviews.Select(r => new ReviewDto
//            {
//                ReviewerName = r.ReviewerName,
//                Rating = r.Rating,
//                Comment = r.Comment
//            }).ToList();

//            return Ok(reviews);
//        }
//    }

//    //[ApiController]
//    //[Route("api/reviews")]
//    //public class ReviewsController : ControllerBase
//    //{
//    //    private readonly MovieApiContext _context;

//    //    public ReviewsController(MovieApiContext context)
//    //    {
//    //        _context = context;
//    //    }

//    //    // GET: api/reviews/movie/{movieId}
//    //    [HttpGet("movie/{movieId}")]
//    //    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
//    //    {
//    //        var movie = await _context.Movies
//    //            .Include(m => m.Reviews)
//    //            .FirstOrDefaultAsync(m => m.Id == movieId);

//    //        if (movie is null)
//    //            return NotFound($"Filmen med id {movieId} hittades inte.");

//    //        var reviews = movie.Reviews.Select(r => new ReviewDto
//    //        {
//    //            ReviewerName = r.ReviewerName,
//    //            Rating = r.Rating,
//    //            Comment = r.Comment
//    //        }).ToList();

//    //        return Ok(reviews);
//    //    }

//    //}
//}
