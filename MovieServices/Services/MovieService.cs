using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Entities;

namespace MovieServices.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await unitOfWork.Movies.AnyAsync(id);
        }

        public async Task<MovieDto> CreateMovieAsync(MovieCreateDto dto)
        {
            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                Genre = dto.Genre,
                Duration = dto.Duration,
                MovieDetails = new MovieDetails
                {
                    Synopsis = dto.Synopsis,
                    Language = dto.Language,
                    Budget = dto.Budget
                },
                Reviews = new List<Review>(),
                Actors = new List<Actor>()
            };

            unitOfWork.Movies.Add(movie);
            await unitOfWork.CompleteAsync();

            return new MovieDto
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
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return false;

            unitOfWork.Movies.Remove(movie);
            await unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            var movies = await unitOfWork.Movies.GetAllAsync();

            var dtos = movies.Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genre = m.Genre,
                Duration = m.Duration,
                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget
            });

            return dtos;
        }


        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return null;

            return new MovieDto
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
        }


        public async Task<MovieDetailDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return null;

            return new MovieDetailDto
            {
                Title = movie.Title,
                Year = movie.Year,
                Genre = movie.Genre,
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails?.Synopsis ?? "",
                Language = movie.MovieDetails?.Language ?? "",
                Budget = movie.MovieDetails?.Budget ?? 0,
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
        }


        public async Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            var movie = await unitOfWork.Movies.GetAsync(id);

            if (movie == null)
                return false;

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = dto.Genre;
            movie.Duration = dto.Duration;

            if (movie.MovieDetails == null)
                movie.MovieDetails = new MovieDetails();

            movie.MovieDetails.Synopsis = dto.Synopsis;
            movie.MovieDetails.Language = dto.Language;
            movie.MovieDetails.Budget = dto.Budget;

            unitOfWork.Movies.Update(movie);
            await unitOfWork.CompleteAsync();

            return true;
        }

    }
}
