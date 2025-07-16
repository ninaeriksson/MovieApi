using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;

namespace MovieServices.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<MovieDto> CreateMovieAsync(MovieCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailDto?> GetMovieDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
