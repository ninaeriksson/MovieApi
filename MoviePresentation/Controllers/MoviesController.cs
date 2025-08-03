using Microsoft.AspNetCore.Mvc;
using MovieContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Paging;

namespace MoviePresentation.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public MoviesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        
        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<PagedResponse<MovieDto>>> GetAll([FromQuery] PagingParameters paging)
        {
            var pagedResult = await serviceManager.MovieService.GetAllAsync(paging);

            var response = new PagedResponse<MovieDto>
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


        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            try
            {
                var movie = await serviceManager.MovieService.GetByIdAsync(id);
                return Ok(movie);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
 

        // GET: api/movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {
            var movieDetailDto = await serviceManager.MovieService.GetMovieDetailsAsync(id);

            if (movieDetailDto is null)
                return NotFound($"Filmen med id {id} hittades inte.");

            return Ok(movieDetailDto);
        }


        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var success = await serviceManager.MovieService.UpdateMovieAsync(id, movieUpdateDto);

            if (!success)
                return NotFound($"Filmen med id {id} hittades inte.");

            return NoContent();
        }

        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto movieCreateDto)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState)
                {
                    Status = 400,
                    Title = "Valideringsfel",
                };
                return BadRequest(details);
            }

            try
            {
                var createdMovie = await serviceManager.MovieService.CreateMovieAsync(movieCreateDto);

                return CreatedAtAction(nameof(GetMovieById), new { id = createdMovie.Id }, createdMovie);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = 400,
                    Title = "Felaktigt värde",
                    Detail = ex.Message
                };
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = "Internt serverfel",
                    Detail = "Något gick fel på servern."
                };
                return StatusCode(500, problemDetails);
            }
        }

        
        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await serviceManager.MovieService.DeleteMovieAsync(id);

            if (!deleted)
                return NotFound($"Filmen med id {id} hittades inte.");

            return NoContent();
        }

        private async Task<bool> MovieExists(int id)
        {
            return await serviceManager.MovieService.AnyAsync(id);
        }

    }
}
