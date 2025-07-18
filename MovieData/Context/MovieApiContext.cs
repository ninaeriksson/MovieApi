using Microsoft.EntityFrameworkCore;
using MovieCore.Models.Entities;

namespace MovieData.Context
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
        public DbSet<Genre> Genres { get; set; } = default!;



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

            // Movie - Genre
            modelBuilder.Entity<Movie>()
                        .HasOne(m => m.Genre)
                        .WithMany(g => g.Movies)
                        .HasForeignKey(m => m.GenreId)
                        .OnDelete(DeleteBehavior.Restrict); // Förhindrar att man råkar ta bort alla filmer om en genre tas bort

            modelBuilder.Entity<Genre>()
                        .HasIndex(g => g.Name)
                        .IsUnique();
        }
    }
}
