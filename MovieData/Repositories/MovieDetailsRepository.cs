using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Context;

namespace MovieData.Repositories
{
    internal class MovieDetailsRepository : IMovieDetailsRepository
    {
        private readonly MovieApiContext context;

        public MovieDetailsRepository(MovieApiContext context)
        {
            this.context = context;
        }

        public void Add(MovieDetails movieDetails)
        {
            context.MovieDetails.Add(movieDetails);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.Movies.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MovieDetails>> GetAllAsync()
        {
            return await context.MovieDetails
                .Include(md => md.Movie)
                .ToListAsync();
        }

        public async Task<MovieDetails> GetAsync(int id)
        {
            return await context.MovieDetails
                .Include(md => md.Movie)
                .FirstAsync(m => m.Id == id);
        }

        public void Remove(MovieDetails movieDetails)
        {
            context.MovieDetails.Remove(movieDetails);
        }

        public void Update(MovieDetails movieDetails)
        {
            context.MovieDetails.Update(movieDetails);
        }
    }
}
