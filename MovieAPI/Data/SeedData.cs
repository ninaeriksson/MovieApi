using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
    public class SeedData
    {
        private static readonly Faker faker = new Faker("sv");

        internal static async Task InitAsync(MovieApiContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            // Rensa databasen
            await ClearDatabaseAsync(context);

            // Fyll på databasen
            List<Actor> actors = GenerateActors(50);
            await context.AddRangeAsync(actors);
            List<Movie> movies = GenerateMovies(10, actors);
            await context.AddRangeAsync(movies);

            // Spara
            await context.SaveChangesAsync();
        }

        private static List<Actor> GenerateActors(int numberOfActors)
        {
            var actors = new List<Actor>(numberOfActors);
            for (int i = 0; i < numberOfActors; i++)
            {
                var actor = new Actor
                {
                    Name = faker.Name.FullName(),
                    BirthYear = faker.Random.Int(1900, 2020)
                };
                actors.Add(actor);
            }
            return actors;
        }

        private static List<Movie> GenerateMovies(int numberOfMovies, List<Actor> actors)
        {
            string[] genres = { "Drama", "Action", "Komedi", "Äventyr", "Dokumentär", "Skräck" };
            string[] languages = { "Svenska", "Engelska", "Tyska", "Franska", "Spanska", "Finska" };

            List<Movie> movies = new List<Movie>(numberOfMovies);

            for (int i = 0; i < numberOfMovies; i++)
            {
                movies.Add(CreateMovie(actors, genres, languages));
            }

            return movies;
        }

        private static Movie CreateMovie(List<Actor> actors, string[] genres, string[] languages)
        {
            int numberOfActors = faker.Random.Int(1, 10);
            List<Actor> randomActors = SelectRandomItems(actors, numberOfActors);

            Movie movie = new Movie
            {
                Title = faker.Commerce.ProductName(),
                Year = faker.Random.Int(1950, 2025),
                Genre = faker.PickRandom(genres),
                Duration = faker.Random.Int(60, 180),
                MovieDetails = new MovieDetails
                {
                    Synopsis = faker.Lorem.Sentence(),
                    Language = faker.PickRandom(languages),
                    Budget = faker.Random.Int(500000, 10000000)
                },
                Actors = randomActors,
                Reviews = GenerateReviews()
            };

            return movie;
        }

        private static List<Review> GenerateReviews()
        {
            int numberOfReviews = faker.Random.Int(1, 5);
            List<Review> reviews = new List<Review>(numberOfReviews);

            for (int i = 0; i < numberOfReviews; i++)
            {
                reviews.Add(new Review
                {
                    ReviewerName = faker.Name.FullName(),
                    Comment = faker.Lorem.Sentence(),
                    Rating = faker.Random.Int(1, 5)
                });
            }

            return reviews;
        }


        private static List<T> SelectRandomItems<T>(List<T> list, int numberOfItems)
        {
            if (list.Count == 0)
                return new List<T>();

            if (numberOfItems > list.Count)
                throw new ArgumentException();

            List<T> selectedItems = new List<T>();
            List<int> usedIndices = new List<int>();

            for (int i = 0; i < numberOfItems; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = faker.Random.Int(0, list.Count - 1);
                }
                while (usedIndices.Contains(randomIndex));

                usedIndices.Add(randomIndex);
                selectedItems.Add(list[randomIndex]);
            }
            return selectedItems;
        }

        public static async Task ClearDatabaseAsync(MovieApiContext context)
        {
            // Rensa tabellerna
            await context.Database.ExecuteSqlRawAsync("DELETE FROM ActorMovie");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Reviews");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM MovieDetails");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Movies");
            await context.Database.ExecuteSqlRawAsync("DELETE FROM Actors");

            // Sätt indexeringen på 0 igen
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Reviews', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('MovieDetails', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Movies', RESEED, 0)");
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Actors', RESEED, 0)");
           
            await context.SaveChangesAsync();
        }

    }
}
