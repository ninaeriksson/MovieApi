using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.Models.Entities;
using MovieData.Context;

namespace MovieData.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieApiContext context;

        public MovieRepository(MovieApiContext context)
        {
            this.context = context;
        }

        public void Add(Movie movie)
        {
            context.Movies.Add(movie);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await context.Movies.AnyAsync(m => m.Id == id);
        }

        public IQueryable<Movie> GetAll()
        {
            return context.Movies
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actors);
        }

        public async Task<Movie?> GetAsync(int id)
        {
            return await context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MovieDetails)
                .Include(m => m.Reviews)
                .Include(m => m.Actors)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Movie movie)
        {
            context.Movies.Remove(movie);
        }

        public void Update(Movie movie)
        {
            context.Movies.Update(movie);
        }
    }
}
