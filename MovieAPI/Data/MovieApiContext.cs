using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
    public class MovieApiContext : DbContext
    {
        public MovieApiContext(DbContextOptions<MovieApiContext> options)
            : base(options)
        {
        }

        public DbSet<MovieDetails> MovieDetails { get; set; } = default!;
        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Actor> Actors { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Movie - MovieDetail
            modelBuilder.Entity<Movie>()
                        .HasOne(m => m.MovieDetails)
                        .WithOne(d => d.Movie)
                        .HasForeignKey<MovieDetails>(d => d.MovieId);

            // Movie - Actor
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Actors)
                        .WithMany(a => a.Movies);

            // Movie - Reviews
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Reviews)
                        .WithOne(r => r.Movie)
                        .HasForeignKey(r => r.MovieId);
        }
    }
}
