using Microsoft.EntityFrameworkCore;
using MovieContracts;
using MovieCore.DomainContracts;
using MovieCore.Models.Dtos;
using MovieCore.Models.Entities;
using MovieCore.Models.Paging;

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
            var genre = await unitOfWork.Genres.GetByIdAsync(dto.GenreId);
            if (genre == null)
            {
                throw new ArgumentException($"Genren med id {dto.GenreId} finns inte.");
            }

            var movie = new Movie
            {
                Title = dto.Title,
                Year = dto.Year,
                GenreId = genre.Id,
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

            var createdMovie = await unitOfWork.Movies
                .GetAll()
                .Include(m => m.Genre)
                .Include(m => m.MovieDetails)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (createdMovie == null)
                throw new Exception("Något gick fel vid skapandet av filmen.");

            return new MovieDto
            {
                Id = createdMovie.Id,
                Title = createdMovie.Title,
                Year = createdMovie.Year,
                Genre = createdMovie.Genre?.Name ?? "",
                Duration = createdMovie.Duration,
                Synopsis = createdMovie.MovieDetails?.Synopsis ?? "",
                Language = createdMovie.MovieDetails?.Language ?? "",
                Budget = createdMovie.MovieDetails?.Budget ?? 0
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


        public async Task<PagedResult<MovieDto>> GetAllAsync(PagingParameters paging)
        {
            var query = unitOfWork.Movies
                .GetAll()
                .Include(m => m.Genre)
                .Include(m => m.MovieDetails);

            int totalItems = await query.CountAsync();

            int pageSize = Math.Min(paging.PageSize, 100);
            int currentPage = paging.Page < 1 ? 1 : paging.Page;

            var items = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    Genre = m.Genre != null ? m.Genre.Name : "Okänd",
                    Duration = m.Duration,
                    Synopsis = m.MovieDetails != null ? m.MovieDetails.Synopsis : "",
                    Language = m.MovieDetails != null ? m.MovieDetails.Language : "",
                    Budget = m.MovieDetails != null ? m.MovieDetails.Budget : 0
                })
                .ToListAsync();

            return new PagedResult<MovieDto>
            {
                Items = items,
                TotalItems = totalItems,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
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
                Genre = movie.Genre?.Name ?? "",
                Duration = movie.Duration,
                Synopsis = movie.MovieDetails?.Synopsis ?? "",
                Language = movie.MovieDetails?.Language ?? "",
                Budget = movie.MovieDetails?.Budget ?? 0
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
                Genre = movie.Genre?.Name ?? "", // <-- ändrad här
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

            var genre = await unitOfWork.Genres.GetByIdAsync(dto.GenreId);
            if (genre == null)
                return false;

            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Genre = genre;
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
