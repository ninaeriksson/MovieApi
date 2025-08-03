using Microsoft.AspNetCore.Http;
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

        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto dto)
        {
            try
            {
                var review = await serviceManager.ReviewService.AddReviewAsync(dto);
                return Ok(review);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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
