using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Entities;

namespace MoviePresentation.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public MoviesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //private readonly MovieApiContext _context;

        //public MoviesController(MovieApiContext context)
        //{
        //    _context = context;
        //}


        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            var movies = await unitOfWork.Movies.GetAllAsync();

            var dto = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration,
                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget

            }).ToList();
            return Ok(dto);
        }
        //// GET: api/movies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        //{
        //    var dto = await _context.Movies.Select(m => new MovieDto
        //    {
        //        Id = m.Id,
        //        Title = m.Title,
        //        Year = m.Year,
        //        Genre = m.Genre,
        //        Duration = m.Duration,
        //        Synopsis = m.MovieDetails.Synopsis,
        //        Language = m.MovieDetails.Language,
        //        Budget = m.MovieDetails.Budget

        //    }).ToListAsync();
        //    return Ok(dto);
        //}



        // GET: api/movies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            Movie movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return NotFound($"Filmen med id {id} hittades inte.");

            if (movie.MovieDetails == null)
                return NotFound($"Filmen med id {id} har inga detaljer registrerade.");

            MovieDto movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails.Synopsis,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget
            };

            return Ok(movieDto);
        }
        //// GET: api/movies/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        //{
        //    //var movie = await _context.Movies.FindAsync(id);
        //    var movie = await _context.Movies
        //        .Include(m => m.MovieDetails)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (movie == null)
        //        return NotFound($"Filmen med id {id} hittades inte.");
        //    //if (movie.MovieDetails == null)
        //    //return NotFound($"Filmen med id {id} har inga detaljer registrerade.");

        //    var movieDto = new MovieDto
        //    {
        //        Id = movie.Id,
        //        Title = movie.Title,
        //        Year = movie.Year,
        //        Genre = movie.Genre,
        //        Duration = movie.Duration,
        //        Synopsis = movie.MovieDetails.Synopsis,
        //        Language = movie.MovieDetails.Language,
        //        Budget = movie.MovieDetails.Budget
        //    };

        //    return Ok(movieDto);
        //}



        // GET: api/movies/{id}/details
        [HttpGet("{id}/details")]
        public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        {

            Movie movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return NotFound($"Filmen med id {id} hittades inte.");

            if (movie.MovieDetails == null)
                return NotFound($"Filmen med id {id} har inga detaljer registrerade.");

            MovieDetailDto movieDetailDto = new MovieDetailDto
            {
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails.Synopsis,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget,
                Comments = movie.Reviews.Select(r => new ReviewDto
                {
                    ReviewerName = r.ReviewerName,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList(),
                Actors = movie.Actors.Select(a => new ActorDto
                {
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList()
            };

            return Ok(movieDetailDto);
        }
        //// GET: api/movies/{id}/details
        //[HttpGet("{id}/details")]
        //public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
        //{
        //    var movie = await _context.Movies
        //                .Include(m => m.MovieDetails)
        //                .Include(m => m.Reviews)
        //                .Include(m => m.Actors)
        //                .FirstOrDefaultAsync(m => m.Id == id);

        //    if (movie == null)
        //        return NotFound($"Filmen med id {id} hittades inte.");

        //    var movieDetailDto = new MovieDetailDto
        //    {
        //        Title = movie.Title,
        //        Year = movie.Year,
        //        Genre = movie.Genre,
        //        Duration = movie.Duration,
        //        Synopsis = movie.MovieDetails.Synopsis,
        //        Language = movie.MovieDetails.Language,
        //        Budget = movie.MovieDetails.Budget,
        //        Comments = movie.Reviews.Select(r => new ReviewDto
        //        {
        //            ReviewerName = r.ReviewerName,
        //            Rating = r.Rating,
        //            Comment = r.Comment
        //        }).ToList(),
        //        Actors = movie.Actors.Select(a => new ActorDto
        //        {
        //            Name = a.Name,
        //            BirthYear = a.BirthYear
        //        }).ToList()
        //    };
        //    return Ok(movieDetailDto);
        //}



        // PUT: api/movies/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        {
            var movie = await unitOfWork.Movies.GetAsync(id);

            if (movie is null)
                return NotFound($"Filmen med id {id} hittades inte.");

            if (movie.MovieDetails is null)
                return NotFound($"Filmen med id {id} har inga detaljer registrerade.");

            movie.Title = movieUpdateDto.Title;
            movie.Year = movieUpdateDto.Year;
            movie.Genre = movieUpdateDto.Genre;
            movie.Duration = movieUpdateDto.Duration;
            movie.MovieDetails.Synopsis = movieUpdateDto.Synopsis;
            movie.MovieDetails.Language = movieUpdateDto.Language;
            movie.MovieDetails.Budget = movieUpdateDto.Budget;

            try
            {
                await unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await unitOfWork.Movies.AnyAsync(id))
                    return NotFound($"Filmen med id {id} hittades inte.");
                else
                    throw;
            }

            return NoContent(); // 204
        }
        //// PUT: api/movies/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMovie(int id, MovieUpdateDto movieUpdateDto)
        //{
        //    //var movie = await _context.Movies.FindAsync(id);
        //    var movie = await _context.Movies
        //        .Include(m => m.MovieDetails)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (movie is null)
        //        return NotFound($"Filmen med id {id} hittades inte.");

        //    movie.Title = movieUpdateDto.Title;
        //    movie.Year = movieUpdateDto.Year;
        //    movie.Genre = movieUpdateDto.Genre;
        //    movie.Duration = movieUpdateDto.Duration;
        //    movie.MovieDetails.Synopsis = movieUpdateDto.Synopsis;
        //    movie.MovieDetails.Language = movieUpdateDto.Language;
        //    movie.MovieDetails.Budget = movieUpdateDto.Budget;

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //            return NotFound($"Filmen med id {id} hittades inte.");
        //        else
        //            throw;
        //    }
        //    return NoContent(); //status 204
        //}



        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto movieCreateDto)
        {
            Movie movie = new Movie
            {
                Title = movieCreateDto.Title,
                Year = movieCreateDto.Year,
                Genre = movieCreateDto.Genre,
                Duration = movieCreateDto.Duration,
                MovieDetails = new MovieDetails
                {
                    Synopsis = movieCreateDto.Synopsis,
                    Language = movieCreateDto.Language,
                    Budget = movieCreateDto.Budget
                },
                Reviews = new List<Review>(),
                Actors = new List<Actor>()
            };

            unitOfWork.Movies.Add(movie);
            await unitOfWork.CompleteAsync();

            var movieDto = new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails.Synopsis,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget
            };

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movieDto);
        }
        //// POST: api/movies
        //[HttpPost]
        //public async Task<ActionResult<MovieDto>> CreateMovie(MovieCreateDto movieCreateDto)
        //{
        //    var movie = new Movie
        //    {
        //        Title = movieCreateDto.Title,
        //        Year = movieCreateDto.Year,
        //        Genre = movieCreateDto.Genre,
        //        Duration = movieCreateDto.Duration,
        //        MovieDetails = new MovieDetails
        //        {
        //            Synopsis = movieCreateDto.Synopsis,
        //            Language = movieCreateDto.Language,
        //            Budget = movieCreateDto.Budget
        //        },
        //        Reviews = new List<Review>(),
        //        Actors = new List<Actor>()
        //    };

        //    _context.Movies.Add(movie);
        //    await _context.SaveChangesAsync();

        //    var movieDto = new MovieDto
        //    {
        //        Id = movie.Id,
        //        Title = movie.Title,
        //        Year = movie.Year,
        //        Genre = movie.Genre,
        //        Duration = movie.Duration,
        //        Synopsis = movie.MovieDetails.Synopsis,
        //        Language = movie.MovieDetails.Language,
        //        Budget = movie.MovieDetails.Budget
        //    };

        //    return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movieDto);
        //}



        // DELETE: api/movies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            Movie movie = await unitOfWork.Movies.GetAsync(id);
            if (movie == null)
                return NotFound($"Filmen med id {id} hittades inte.");

            unitOfWork.Movies.Remove(movie);
            await unitOfWork.CompleteAsync();

            return NoContent(); // 204
        }
        //// DELETE: api/movies/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie(int id)
        //{
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie == null)
        //        return NotFound();

        //    _context.Movies.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        private async Task<bool> MovieExists(int id)
        {
            return await unitOfWork.Movies.AnyAsync(id);
        }
        //private bool MovieExists(int id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}
